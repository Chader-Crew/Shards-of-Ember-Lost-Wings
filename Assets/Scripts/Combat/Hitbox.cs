using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Hitbox para ativacao de ataques baseados em animacao
public class Hitbox : MonoBehaviour
{
    [SerializeField] private Character owner;   //Dono da hitbox, usado para atribuir propriedade a skill
    [SerializeField] private string[] tags;     //Tags de personagem que a hitbox acerta.

    [Header("Hitbox Attributes")]               //Atributos do OverlapBox
    [SerializeField] private Transform[] pivotArray;    //lista de pivos para animar as hitboxes
    [Header("For previewing")]
    [SerializeField] private Transform pivot;
    [SerializeField] private Vector3 offsets;
    [SerializeField] private Vector3 dimensions;

    
    private Color gizmoColor = Color.yellow;    //para debug


    //mostra a hitbox quando selecionada no modo editor
    private void OnDrawGizmosSelected() 
    {
        //gizmos
        #if UNITY_EDITOR
        Gizmos.color = gizmoColor;
        
        //posicao
        Vector3 boxPosition = pivot.position + pivot.forward * offsets.z + pivot.right * offsets.x + pivot.up * offsets.y;
        boxPosition = pivot.InverseTransformPoint(boxPosition); //nao entendi direito o pq mas essa rotacao faz ter que tirar o inverso da posicao local

        Matrix4x4 prevMatrix = Gizmos.matrix;                   //cache da rotacao pra n dar ruim
        Gizmos.matrix = pivot.localToWorldMatrix;               //rotacao do gizmo
        Gizmos.DrawWireCube(boxPosition, dimensions);           //draw
        Gizmos.matrix = prevMatrix;                             //restaura a rotacao
        #endif
    }

    //Atribuicao de pivo para evento de animacao
    public void SetPivot(int index)
    {
        pivot = pivotArray[index];
    }
    //Atribuicoes de offset para evento de animacao (sim so da pra passar 1 por ves)
    public void SetOffsetX(float offset)
    {
        offsets.x = offset;
    }
    public void SetOffsetY(float offset)
    {
        offsets.y = offset;
    }
    public void SetOffsetZ(float offset)
    {
        offsets.z = offset;
    }
    //Atribuicoes de escala para evento de animacao (sim so da pra passar 1 por ves)
    public void SetDimensionX(float dimension)
    {
        dimensions.x = dimension;
    }
    public void SetDimensionY(float dimension)
    {
        dimensions.y = dimension;
    }
    public void SetDimensionZ(float dimension)
    {
        dimensions.z = dimension;
    }

    //Ativa a hitbox, se ja estiver ativada com alguma skill, adiciona na lista de chamada, mas não reinicia a hitbox.
    public void Activate(SkillBase skill) 
    {
        //criacao da SkillData e inicializacao de variaveis
        SkillData context = new SkillData();
        context.owner = owner;
        StartCoroutine(ActiveCollision(skill, context));

        gizmoColor = Color.red;     //para debug
    }

    public void Deactivate()
    {
        StopAllCoroutines();

        gizmoColor = Color.yellow;  //para debug
    }

    //checa acerto em cada frame de física por um tempo designado. Coloca os Characters acertados na lista targets da SkillData.
    private IEnumerator ActiveCollision(SkillBase skill, SkillData context)
    {
        List<Collider> hitColliders = new List<Collider>(); //lista de colisores ja acertados, para evitar multihit indesejado.

        context.targets = new List<Character>();            //inicializa a lista de alvos da SkillData.

        while (true)
        {

            context.targets.Clear();                        //limpa a lista de target antes de toda checagem para nao ativar duplicado.

            //OverlapBox
            List<Collider> colliders = Physics.OverlapBox(
            pivot.position +    //posicao do pivot
            pivot.forward * offsets.z + pivot.right * offsets.x + pivot.up * offsets.y,    //offset rodado pra local position
            dimensions/2,       //tamanho do quadrado
            pivot.rotation)     //rotacao do pivot
            .ToList();      
            
            //remocao de colisores ja acertados por essa hitbox.
            foreach (Collider hitCollider in hitColliders)  
            {
                colliders.Remove(hitCollider);
            }

            //atribuicao a lista de alvos e ativacao de skill.
            if (colliders.Count > 0)
            {
                foreach(Collider collider in colliders)
                {
                    hitColliders.Add(collider);                                 //adiciona o colisor na lista de colisores acertados,
                    if(tags.Contains(collider.tag))                             //e se o colisor for da tag correta, da GetComponent<Character> e adiciona na lista de targets.
                    { context.targets.Add(collider.GetComponent<Character>()); }
                }

                skill.Activate(context);
            }

            yield return new WaitForFixedUpdate();  //espera pelo proximo update de fisica
        }
    }
}
