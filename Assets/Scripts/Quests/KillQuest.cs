using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    public int OncaToKill { get; private set; }
    public int OncaKilled { get; private set; }

    public KillQuest(string name, string description, int oncaToKill) : base(name, description)
    {
        OncaToKill = oncaToKill;
        OncaKilled = 0;
    }

    public void KilledOnca()
    {  
        OncaKilled++;

        if (OncaKilled >= OncaToKill)
        {
            CompleteQuest();
        }
    }
}
