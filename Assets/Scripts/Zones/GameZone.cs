using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Zonas de jogo conectadas por pontos de warp.
public class GameZone : MonoBehaviour
{
    private static GameZone currentZone;

    [SerializeField] private Transform[] warpDestinations;

    private void Start()
    {
        if (currentZone == null)
        {
            currentZone = gameObject.GetComponent<GameZone>();
        }
        else if (currentZone!= this)
        {
            Destroy(gameObject);
        }
    }

    public static void GoToZone(GameZone zone, int warpIndex)
    {
        //currentZone.transform.position = Vector3.down*10000;
        PlayerController.Instance.playerMovement.characterController.enabled = false;
        PlayerController.Instance.transform.position = zone.warpDestinations[warpIndex].position;
        PlayerController.Instance.playerMovement.characterController.enabled = true;

        Destroy(currentZone.gameObject);
        currentZone = Instantiate(zone, Vector3.zero, Quaternion.identity);
        SaveGame.Save();
    }
}
