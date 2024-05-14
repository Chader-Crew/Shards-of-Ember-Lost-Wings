using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class skilltreeManager : MonoBehaviour
{
    public static bool skill1=false,skill2=false; 
    [SerializeField]int skillpoints;
    public Text SP;
    public GameObject skilltree;

    void Start()
    {
        skillpoints = 2;
    }
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(skilltree.activeSelf)
            {
            }
            else
            {
                skilltree.SetActive(true);
            }
        }
    }
    public void Resume()
    {
        skilltree.SetActive(false);
        Time.timeScale=1;
    }
    public void ComprarSkill1()
    {
        if(skillpoints > 0||skill1==false)
        {
            skill1=true;
            skillpoints--;
        }
    }
    public void ComprarSkill2()
    {
        if(skill1==true||skill2==false)
        {
            skill2=true;
            skillpoints--;
        }
    }
}
