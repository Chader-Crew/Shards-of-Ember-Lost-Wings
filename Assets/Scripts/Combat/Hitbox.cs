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
    [SerializeField] private Transform pivot;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 dimensions;


    //mostra a hitbox quando selecionada no modo editor
    private void OnDrawGizmosSelected() 
    {
        //gizmos
        #if UNITY_EDITOR
        Gizmos.color = Color.red;
        
        //posicao
        Vector3 boxPosition = pivot.position + pivot.forward * offset.z + pivot.right * offset.x + pivot.up * offset.y;
        boxPosition = pivot.InverseTransformPoint(boxPosition); //nao entendi direito o pq mas essa rotacao faz ter que tirar o inverso da posicao local

        Matrix4x4 prevMatrix = Gizmos.matrix;                   //cache da rotacao pra n dar ruim
        Gizmos.matrix = pivot.localToWorldMatrix;               //rotacao do gizmo
        Gizmos.DrawWireCube(boxPosition, dimensions);           //draw
        Gizmos.matrix = prevMatrix;                             //restaura a rotacao
        #endif
    }

    //Ativa a hitbox, iniciando a checagem pelo tempo designado.
    private void Activate(SkillBase skill)  //(SkillBase skill, float seconds) NAO TA FUNCIONANDO POR QUE O ANIMATOR SO CHAMA METODO COM 1 PARAMETRO MAX
    {
        //criacao da SkillData e inicializacao de variaveis
        SkillData context = new SkillData();
        context.owner = owner;
        StartCoroutine(ActiveCollision(skill, context, 0.5f));
    }

    //checa acerto em cada frame de física por um tempo designado. Coloca os Characters acertados na lista targets da SkillData.
    private IEnumerator ActiveCollision(SkillBase skill, SkillData context, float seconds)
    {
        float timer = 0;                                    //timer pra saber quando parar de ativar essa skill
        List<Collider> hitColliders = new List<Collider>(); //lista de colisores ja acertados, para evitar multihit indesejado.

        context.targets = new List<Character>();            //inicializa a lista de alvos da SkillData.

        while (timer <= seconds){

            context.targets.Clear();                        //limpa a lista de target antes de toda checagem para nao ativar duplicado.

            //OverlapBox
            List<Collider> colliders = Physics.OverlapBox(
            pivot.position +    //posicao do pivot
            pivot.forward * offset.z + pivot.right * offset.x + pivot.up * offset.y,    //offset rodado pra local position
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

            yield return new WaitForFixedUpdate();  //espera pelo proximo update de fisica e atualiza o timer
            timer += Time.fixedDeltaTime;
        }
    }
}
