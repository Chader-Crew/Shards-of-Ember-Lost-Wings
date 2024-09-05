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

    [SerializeField] private Animator tutorialAnim;

    void Start(){
        InputReader.OnMoveEvent += HandleMove;
        tutorialPanel.SetActive(true);
        UpdateTutorial("Use WASD para mover");
    }

    public void UpdateTutorial(string newText){
        tutorialText.text = newText;
    }

    private void HandleMove(Vector2 vector2){

        if(vector2.magnitude != 0){
            UpdateTutorial("Pressione Espaço para o dash");
        }
        InputReader.OnMoveEvent -= HandleMove;
        InputReader.OnDashEvent += HandleDash;
    }

    private void HandleDash(){
        UpdateTutorial("Pressione F para interagir");
        InputReader.OnDashEvent -= HandleDash;
        InputReader.OnItemInteractEvent += HandleInteract;
    }

    private void HandleInteract(){
        UpdateTutorial("Esquerdo do mouse: Ataque simples");
        InputReader.OnItemInteractEvent -= HandleInteract;
        InputReader.OnAttackEvent += HandleAttack;
    }

    private void HandleAttack(){
        UpdateTutorial("Esc para abrir a Skill Tree");
        StartCoroutine("CleanTutorial");
    }

    IEnumerator CleanTutorial(){
        yield return new WaitForSeconds(5f);
        InputReader.OnAttackEvent -= HandleAttack;
        tutorialAnim.Play("TutorialPopOut");
        Destroy(this, 2f); //por enqunato apagar o codigo funciona
    }
}
