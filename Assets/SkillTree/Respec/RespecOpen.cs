using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespecOpen : MonoBehaviour //colisao com o objeto de respec ou player
{
    //dialog "would you like to respec your skills?"
    //if yes == open skill tree ui, lock all skills, give back the points
    //else == leave dialogue, unlock player movement

    //or

    //open skill tree ui, with respec button somewhere
    //respec click, give all points back, lock all skills
    //update skill ui
    //save new bought or any bought
    //close button

    public GameObject skillTreeUI, respecButton, interactButton;

    public void ToggleSkillTree(bool t){
        skillTreeUI.SetActive(t);
        respecButton.SetActive(t);
    }

    public void Respec(){
        //points back
        //lock skills
    }

    public void CloseRespec(){
        //update save
    }
}
