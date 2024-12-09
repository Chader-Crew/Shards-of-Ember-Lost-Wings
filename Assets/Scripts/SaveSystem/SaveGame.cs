using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveGame
{
    public static void Save()
    {
        PlayerData playerData = new PlayerData();   //inicializa o struct
        
        
        //atribui o estado elemental atual
        playerData.activeTree = PlayerController.Instance.state.nameState;

        playerData.activeZone = GameObject.FindObjectOfType<GameZone>().name;
        
        //atribui as listas de skills desbloqueadas
        //lista temporaria dos botoes da skilltree pra achar todas as skills
        List<currentSkill> tempList = new List<currentSkill>();
        tempList = GameObject.FindObjectsByType<currentSkill>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        //Sort por nome para a indexação ser sempre igual
        tempList.Sort((s1, s2) => s1.gameObject.name.CompareTo(s2.gameObject.name));    //eu escrevi que nao ia usar linq ali embaixo mas
                                                                                                                    //to eu aqui usando. -Alu
        List<int> skillIndexes = new List<int>();
        foreach (currentSkill skillButton in tempList.Where(s => s.Skill.canCast))
        {
            skillIndexes.Add(tempList.IndexOf(skillButton));
        }
        playerData.unlockedSkills = skillIndexes.ToArray();

        //atribui a lista de bonfires desbloqueadas
        playerData.unlockedBonfires = new string[BonfireRegister.bonfireDictionary.Keys.Count];
        BonfireRegister.bonfireDictionary.Keys.CopyTo(playerData.unlockedBonfires, 0);  // copia as keys como string pro array, tem que ou ser assim ou usar linq
                                                                                        // eu não vou usar linq pq ele me traiu -Alu
        
        //atribui o spawnpoint  (TODO: substituir isso pela posicao da ultima bonfire interagida)
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
            Debug.LogError("TENTOU LOADAR EM UMA CENA QUE NAO E A GAME");
            return;
        }
        
        //carrega o arquivo para o struct
        string jsonString = File.ReadAllText(Application.dataPath + "/Save/saveFile.json");
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonString);

        GameObject newZone = (GameObject)GameObject.Instantiate(Resources.Load(loadedData.activeZone));
        GameZone.currentZone = newZone.GetComponent<GameZone>();
        
        //load do estado elemental
        //muda de estado pra frente e para quando acha o certo
        for (int i = 0; i < 3; i++)
        {
            if (loadedData.activeTree == PlayerController.Instance.state.nameState){ break; }
            
            PlayerController.Instance.ChangeState(1);
        }
        
        //load das compras de skill
        //lista temporaria dos botoes da skilltree pra achar todas as skills
        List<currentSkill> tempList = new List<currentSkill>();
        tempList = GameObject.FindObjectsByType<currentSkill>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        //Sort por nome para a indexação ser sempre igual
        tempList.Sort((s1, s2) => s1.gameObject.name.CompareTo(s2.gameObject.name));

        foreach (currentSkill skillButton in tempList.Where(s => loadedData.unlockedSkills.Contains(tempList.IndexOf(s))))  //cruzes -Alu
        {
            //Debug.Log(skillButton.gameObject.name);
            skillButton.LoadSkill();
        }

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

    public static void LoadBlank(Scene scene, LoadSceneMode mode)
    {
        List<currentSkill> tempList = new List<currentSkill>();
        tempList = GameObject.FindObjectsByType<currentSkill>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

        foreach (currentSkill skillButton in tempList)
        {
            skillButton.Skill.canCast = false;
            skillButton.Skill._onCooldown = false;
        }
    }
    
}
