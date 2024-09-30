using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveGame
{
    public static void Save()
    {
        PlayerData playerData = new PlayerData();   //inicializa o struct

        //atribui os stats do player (TODO: substituir isso por um registro de desbloqueios de nodos das skillTrees, quando estiverem implementados)
        playerData.atk = PlayerController.Instance.character.Stats.atk;
        playerData.def = PlayerController.Instance.character.Stats.def;
        playerData.spd = PlayerController.Instance.character.Stats.spd;
        playerData.maxHp = PlayerController.Instance.character.Stats.maxHp;

        //atribui a lista de bonfires desbloqueadas
        playerData.unlockedBonfires = new string[BonfireRegister.bonfireDictionary.Keys.Count];
        BonfireRegister.bonfireDictionary.Keys.CopyTo(playerData.unlockedBonfires, 0);  // copia as keys como string pro array, tem que ou ser assim ou usar linq
                                                                                        // eu não vou usar linq pq ele me traiu -Alu
        
        //atribui o spawnpoint  (TODO: substituir isso pela posicao da ultima bonfire interagida quando for implementado)
        playerData.spawnPosition = PlayerController.Instance.transform.position;

        //escreve o arquivo em JSON
        string jsonString = JsonUtility.ToJson(playerData);
        Directory.CreateDirectory(Application.dataPath + "/Save");
        File.WriteAllText(Application.dataPath + "/Save/saveFile.json", jsonString);
    }

    public static void Load(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Game")
        {
            Debug.LogError("TENTOU LOADAR UMA CENA QUE NAO E A GAME");
            return;
        }

        //carrega o arquivo para o struct
        string jsonString = File.ReadAllText(Application.dataPath + "/Save/saveFile.json");
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonString);

        //atribui os stats do player
        PlayerController.Instance.character.Stats.atk = loadedData.atk;
        PlayerController.Instance.character.Stats.def = loadedData.def;
        PlayerController.Instance.character.Stats.spd = loadedData.spd;
        PlayerController.Instance.character.Stats.maxHp = loadedData.maxHp;
        PlayerController.Instance.character.Stats.hp = loadedData.maxHp;

        //teleporta o player pro lugar certo
        PlayerController.Instance.transform.position = loadedData.spawnPosition;

        //atribui a lista de bonfires desbloqueadas
        BonfireRegister.bonfireActive.Clear();
        foreach (string bonfireName in loadedData.unlockedBonfires)
        {
            BonfireRegister.bonfireActive.Add(BonfireRegister.bonfireDictionary[bonfireName]);
        }

        SceneManager.sceneLoaded -= Load;
    }
}
