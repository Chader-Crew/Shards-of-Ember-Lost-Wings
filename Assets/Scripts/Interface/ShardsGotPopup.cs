using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShardsGotPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text totalText;
    [SerializeField] private TMP_Text increaseText;
    [SerializeField] private Animator anim;
    public void Popup(int increase)
    {
        totalText.text = PlayerController.Instance.StatShards.ToString();
        increaseText.text = "+" +increase.ToString();
        anim.Play("POPUP");
    }
}
