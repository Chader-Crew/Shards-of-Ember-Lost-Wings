using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//estrutura de dados relacionada ao progresso do player, para funcionar com SaveGame e ser escrita em JSON
[Serializable]
public struct PlayerData
{
    public Vector3 spawnPosition;
    public int atk, def, spd, maxHp;
    public string[] unlockedBonfires;
    public string activeTree;
    public string activeZone;
    public int _altarIsActive;
    public int[] unlockedSkills;
}
