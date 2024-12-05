using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestEnemyKill")]
public class EnemyKiller : QuestObjective
{
    public List<Character> enemies; // Lista de inimigos para matar

    public override void Objective()
    {
        CheckQuestProgress();
    }
    public void Initialize()
    {
        // Aqui você pode adicionar a lógica para associar os inimigos dinamicamente, se necessário
        Debug.Log("Inicializando quest: " + questName);
        
        // Se os inimigos já estiverem configurados, você só precisa se inscrever nos eventos
        foreach (var enemy in enemies)
        {
            enemy.OnDiedEvent += OnEnemyDeath;
        }
    }

    private void OnDisable()
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
    }

    public void CheckQuestProgress()
    {
        // Checa se ainda há inimigos na lista (por segurança)
        enemies.RemoveAll(enemy => enemy == null || enemy.HP <= 0);

        if (enemies.Count == 0)
        {
            CompleteQuest();
        }
    }
}
