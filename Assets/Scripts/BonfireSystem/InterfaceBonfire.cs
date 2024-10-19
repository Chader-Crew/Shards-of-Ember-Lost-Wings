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

        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Bonfire bonfire in BonfireRegister.bonfireActive)
        {
            Button button = Instantiate(bonfirePrefab, grid.transform);
            button.GetComponentInChildren<Text>().text = bonfire.bonfireName;

            button.onClick.AddListener(() => TPcheck(bonfire));
        }
    }

    public void TPcheck(Bonfire bonfire)
    {
        Teleport(bonfire.bonfirePosition);
    }

    public void CloseBonfireMenu()
    {
        bonfireMenu.SetActive(false);
    }

    public void Teleport(Vector3 position)
    {
        PlayerController.Instance.playerMovement.characterController.enabled = false;
        PlayerController.Instance.transform.position = new Vector3(position.x, position.y + 1, position.z);
        PlayerController.Instance.playerMovement.characterController.enabled = true;

        bonfireMenu.SetActive(false);
    }
}
