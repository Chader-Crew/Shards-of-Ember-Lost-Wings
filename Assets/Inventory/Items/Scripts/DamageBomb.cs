using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Bomb", menuName = "Inventory System/Item/Damage Bomb")]
public class DamageBomb : UsableItem
{
    public GameObject bombPrefab; // Prefab da bomba
    //public float throwForce = 10f;

    public override void Use()
    {
        ThrowBomb();
    }

    private void ThrowBomb()
    {
        GameObject player = PlayerController.Instance.gameObject;
        GameObject bomb = Instantiate(bombPrefab, player.transform.position + player.transform.forward, Quaternion.identity);
        //Rigidbody rb = bomb.GetComponent<Rigidbody>();
        //rb.AddForce(player.transform.forward * throwForce, ForceMode.VelocityChange);
    }
}