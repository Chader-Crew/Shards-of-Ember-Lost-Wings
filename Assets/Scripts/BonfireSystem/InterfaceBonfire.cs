using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InterfaceBonfire : MonoBehaviour
{
    public GameObject bonfireMenu;
    public GameObject grid;
    public Button bonfirePrefab;

    public void OpenBonfireMenu()
    {
        bonfireMenu.SetActive(true);

        foreach (Transform child in grid.transform) //funciona e por enquanto vai ficar assim 
        {
            Destroy(child.gameObject);
        }

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
        PlayerController.Instance.transform.position = localTP.position;
        PlayerController.Instance.playerMovement.characterController.enabled = false;
        bonfireMenu.SetActive(false);
    }
}

