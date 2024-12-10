using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Character>().OnDiedEvent += () => { winScreen.SetActive(true);};
    }
}
