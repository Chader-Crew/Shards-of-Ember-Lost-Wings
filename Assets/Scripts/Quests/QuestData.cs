using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestData")]
public class QuestData : MonoBehaviour
{
    public string questName;          
    public string description;        
    public string objective; 
    public bool _isCompleted = false;  

    public void CompleteQuest() // adicionar uma animação pop up que pode aparacer na tela do player quando a quest é completada
    {
        _isCompleted = true;
    }
}
