using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestEnemyKill")]
public class EnemyKiller : QuestObjective
{
    public List<Character> enemies; // Lista de inimigos para matar
    public QuestParent[] enemiesParents; // Lista de inimigos para matar
    public EnemyKiller enemyKiller;

    public override void Objective()
    {
    }
    public void Initialize()
    {
        // Aqui você pode adicionar a lógica para associar os inimigos dinamicamente, se necessário
        Debug.Log("Inicializando quest: " + questName);
        // //parents = FindObjectsOfType<QuestParent>();
        // enemiesParents = FindObjectsOfType<QuestParent>();
        
        // // Se os inimigos já estiverem configurados, você só precisa se inscrever nos eventos
        // foreach (var enemy in enemies)
        // {
        //     enemy.OnDiedEvent += OnEnemyDeath;
        // }
    }

    /*private void OnDisable()
    {
        // Desinscreve dos eventos quando a quest for desativada
        foreach (var enemy in enemies)
        {
            enemy.OnDiedEvent -= OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        // Remove o inimigo da lista quando ele morre
        enemies.RemoveAll(enemy => enemy == null || enemy.HP <= 0);
        Debug.Log($"Inimigo eliminado! Restantes: {enemies.Count}");

        // Verifica se todos os inimigos foram eliminados
        if (enemies.Count == 0)
        {
            CompleteQuest(); // Marca a quest como completa
            Debug.Log($"Quest '{questName}' concluída!");
        }
    }*/
}
