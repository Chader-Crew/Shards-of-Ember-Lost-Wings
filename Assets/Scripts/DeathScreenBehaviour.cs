using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    private void Start() 
    {
        screen.SetActive(false);
    }
    public void OnPlayerDeath()
    {
        screen.SetActive(true);
        StartCoroutine("timeZero");
    }

    IEnumerator timeZero(){
        yield return new WaitForSeconds(3.5f);
        Time.timeScale = 0;
    }
}
