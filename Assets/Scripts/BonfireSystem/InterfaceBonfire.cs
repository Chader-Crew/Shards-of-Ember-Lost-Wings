using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InterfaceBonfire : MonoBehaviour
{
    public GameObject bonfireMenu;
    public GameObject grid;
    public Button bonfirePrefab;
    public PlayerMovement playerRef;

    void Awake()
    {
        playerRef = GetComponent<PlayerMovement>();
    }
    public void OpenBonfireMenu()
    {
        bonfireMenu.SetActive(true);

        foreach (Bonfire bonfire in BonfireRegister.bonfireActive)
        {
            Button button = Instantiate(bonfirePrefab, grid.transform);
            button.GetComponentInChildren<Text>().text = bonfire.bonfireName;
            button.onClick.AddListener(() => Teleport(bonfire.bonfireLocation));
        }
    }

    public void CloseBonfireMenu()
    {
        bonfireMenu.SetActive(false);
    }

    public void Teleport(Transform localTP)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = localTP.position;
        playerRef.characterController.enabled = false;
        bonfireMenu.SetActive(false);
    }
}

