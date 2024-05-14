using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ativador de skill incorporeo.
[RequireComponent(typeof(Character))]
public class SkillCaster : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 30f)] private float interval;     //intervalo entre chamadas
    [SerializeField] private SkillBase skill;                       //skill pra ser chamada
    [SerializeField] private Character character;                   //personagem de origem (stats)
    [Space(15)] [SerializeField] private bool _isHitBoxActivated;   //liga se for usar hitbox
    [SerializeField] private Hitbox hitbox;                         //referencia pra hitbox

    private void Start() 
    {
        StartCoroutine(CastLoop());
    }

    //loop de ativacao.
    private IEnumerator CastLoop()
    {  
        while (true)
        {
            SkillData data = new SkillData();
            data.owner = character;
            
            //ativa a hitbox se tiver, se nao so ativa a skill
            if(_isHitBoxActivated)
            {
                hitbox.Activate(skill);
                yield return new WaitForSeconds(interval);
                hitbox.Deactivate();
            }else
            {
                skill.Activate(data);
                yield return new WaitForSeconds(interval);
            }
        } 
    }
}
