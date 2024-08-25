using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    //public bool completedTutorial = false; //isso podia estar num game manager, quando a gente tiver
    //tem que estar no save também

    public GameObject tutorialPanel;
    public TMP_Text tutorialText;
    //public GameObject enemyPrefab; no tutorial oficial spawnar inimigo

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Animator tutorialAnim;

    void Start(){
        inputReader.OnMoveEvent += HandleMove;
        tutorialPanel.SetActive(true);
        UpdateTutorial("Use WASD ou Analógico Esquerdo para mover");
    }

    public void UpdateTutorial(string newText){
        tutorialText.text = newText;
    }

    private void HandleMove(Vector2 vector2){

        if(vector2.magnitude != 0){
            UpdateTutorial("Pressione Espaço ou B na manete para o dash");
        }
        inputReader.OnMoveEvent -= HandleMove;
        inputReader.OnDashEvent += HandleDash;
    }

    private void HandleDash(){
        UpdateTutorial("Pressione F ou A na manete para interagir");
        inputReader.OnDashEvent -= HandleDash;
        inputReader.OnItemInteractEvent += HandleInteract;
    }

    private void HandleInteract(){
        UpdateTutorial("Esquerdo do mouse ou X na manete: Ataque simples");
        inputReader.OnItemInteractEvent -= HandleInteract;
        inputReader.OnAttackEvent += HandleAttack;
    }

    private void HandleAttack(){
        UpdateTutorial("Esc ou Start para abrir a Skill Tree");
        StartCoroutine("CleanTutorial");
    }

    IEnumerator CleanTutorial(){
        yield return new WaitForSeconds(5f);
        inputReader.OnAttackEvent -= HandleAttack;
        tutorialAnim.Play("TutorialPopOut");
        Destroy(this, 2f); //por enqunato apagar o codigo funciona
    }
}
