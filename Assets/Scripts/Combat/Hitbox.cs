using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Hitbox para ativacao de ataques baseados em animacao
public class Hitbox : MonoBehaviour
{
    [SerializeField] private Character owner;   //Dono da hitbox, usado para atribuir propriedade a skill
    [SerializeField] private string[] tags;     //Tags de personagem que a hitbox acerta.

    [Header("Hitbox Attributes")]   //Atributos do OverlapBox
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 dimensions;


    //mostra a hitbox quando selecionada no modo editor
    private void OnDrawGizmosSelected() 
    {
        //gizmos
        #if UNITY_EDITOR
        Gizmos.color = Color.red;
        
        //posicao
        Vector3 boxPosition = transform.position + transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y;
        boxPosition = transform.InverseTransformPoint(boxPosition); //nao entendi direito o pq mas essa rotacao faz ter que tirar o inverso da posicao local

        Matrix4x4 prevMatrix = Gizmos.matrix;           //cache da rotacao pra n dar ruim
        Gizmos.matrix = transform.localToWorldMatrix;   //rotacao do gizmo
        Gizmos.DrawWireCube(boxPosition, dimensions);   //draw
        Gizmos.matrix = prevMatrix;                     //restaura a rotacao
        #endif
    }

    //Ativa a hitbox, colocando todos os personagens acertados numa SkillData e enviando para a SkillBase designada.
    private void Activate(SkillBase skill) 
    {
        //criacao da SkillData e inicializacao de variaveis
        SkillData context = new SkillData();
        context.owner = owner;
        context.targets = new List<Character>();

        //overlapbox e atribuicao de lista de alvos
        Collider[] colliders = Physics.OverlapBox(transform.position +
        transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y,    //offset rodado pra local position
        dimensions/2,
        transform.rotation);

        foreach(Collider collider in colliders)
        {
                if(tags.Contains(collider.tag))
                { context.targets.Add(collider.GetComponent<Character>()); }
        }
        
        skill.Activate(context);
    }
}
