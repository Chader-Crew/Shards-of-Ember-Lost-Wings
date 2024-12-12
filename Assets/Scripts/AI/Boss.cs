using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCController))]
public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private NPCController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<NPCController>();
        
        controller.Character.OnDiedEvent += () => { winScreen.SetActive(true);};
    }
}
