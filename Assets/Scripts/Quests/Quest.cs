using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string QuestName { get; private set; } 
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }

    public Quest(string name, string description)
    {
        QuestName = name;        
        Description = description;
        IsCompleted = false;     
    }

    public void CompleteQuest()
    {
        IsCompleted = true;      
    }
}
