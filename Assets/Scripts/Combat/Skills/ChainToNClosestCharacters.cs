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
    //[SerializeField] private bool _LOS;              //ativa a checagem de line of sight

    [Header("Line Renderer Properties")]
    [SerializeField] private bool _renderLine;
    [SerializeField] private Material lineRendererMaterial;
    [SerializeField] private float lineWidth;
    [SerializeField] private float lineDuration;

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
        colliders.OrderBy(c => (c.transform.position - origin).sqrMagnitude);   //ordena a lista de acordo com a distancia da origem (NÃO TA FUNCIONANDO REEEEEEEEE)
        
        for(int i = 0; i<colliders.Length; i++)
        {
            Debug.Log(i + ": " + (colliders[i].transform.position - origin).sqrMagnitude);
        }

        foreach (Collider collider in colliders)    
        {
            if(collider.tag == tag)                 //filtro de tag
            {
            /* if(_LOS)                             //checagem de LOS
                {                                   //tem que mexer em layer de colisao, depois faz
                    
                } */

                SkillData nextContext = context;    //clone de contexto

                nextContext.targets = new List<Character>{collider.gameObject.GetComponent<Character>()} ;   //reatribuicao de alvo

                //Debug.Log(nextContext.targets[0]);
                nextSkill.Activate(nextContext);                //ativacao de skill

                //Line Renderer
                LineRenderer line = new GameObject().AddComponent<LineRenderer>();
                line.material = lineRendererMaterial;
                line.useWorldSpace = true;
                Vector3[] linePos = new Vector3[2]{origin, collider.transform.position};
                line.SetPositions(linePos);
                line.startWidth = lineWidth;
                Destroy(line.gameObject, lineDuration);

                charactersChained++;                            //cheque de numero de ativacoes
                if(charactersChained >= maxChainCount)
                {
                    break;
                }
            }
        }
    }
}
