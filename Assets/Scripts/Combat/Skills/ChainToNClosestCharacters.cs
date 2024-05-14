using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ativa uma skill um maximo de N vezes em personagens proximos.
[CreateAssetMenu(menuName = "ScriptableObjects/Skills/ChainToNClosest")]
public class ChainToNClosestCharacters : SkillBase
{  
    [SerializeField] private string tag;            //tag para filtro de personagens
    [SerializeField] private float maxDistance;     //distancia maxima do dono da skill
    [SerializeField] private int maxChainCount;     //quantidade maxima de chars afetados
    [SerializeField] private SkillBase nextSkill;   //skill pra ser ativada

    public override void Activate(SkillData context)
    {   
        //determinacao de origem da distancia.
        //Se houver target, origem e a posicao do alvo. Se nao, origem e a posicao do dono.
        Vector3 origin;
        if(context.targets == null)
        {
            origin = context.owner.transform.position;
        }else
        {
            origin = context.targets[0].transform.position;
            if(context.targets.Count != 1)
            {
                Debug.LogWarning("===CHAIN EFFECT ATIVADO COM CONTEXTO DE MULTIPLOS ALVOS===");
            }
        }
        
        //overlap sphere
        Collider[] colliders = Physics.OverlapSphere(origin,maxDistance);
        float charactersChained = 0;
        colliders.OrderBy(collider => Vector3.Distance(origin, collider.transform.position));   //ordena a lista de acordo com a distancia da origem

        foreach (Collider collider in colliders)    
        {
            if(collider.tag == tag)                 //filtro de tag
            {
                SkillData nextContext = context;    //clone de contexto

                nextContext.targets.Clear();        //reatribuicao de alvo
                nextContext.targets.Add(collider.gameObject.GetComponent<Character>());

                nextSkill.Activate(nextContext);    //ativacao de skill
                charactersChained++;                //cheque de numero de ativacoes
                if(charactersChained >= maxChainCount)
                {
                    break;
                }
            }
        }
    }
}
