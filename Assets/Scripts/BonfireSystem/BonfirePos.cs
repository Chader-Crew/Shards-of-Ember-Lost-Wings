using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonfirePos
{
    public string bonfireName;
    public Vector3 position;

    public BonfirePos(string name, Vector3 pos)
    {
        bonfireName = name;
        position = pos;
    }
}
