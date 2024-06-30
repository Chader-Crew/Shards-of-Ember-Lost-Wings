using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillReseter : MonoBehaviour
{
    [SerializeField]SkillTreeHolder player;
    void Start()
    {
        player=this.gameObject.GetComponent<PlayerController>().state;
        for(int i=0;i<player.skills.Length;i++)
        {
            player.skills[i].canCast=false;
        }
    }
}
