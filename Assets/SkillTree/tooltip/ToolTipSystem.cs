using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    private static ToolTipSystem instance;
    public tooltip toooltip;
    void Awake()
    {
        instance=this;
    }

    public static void Show(string body, string header="")
    {
        instance.toooltip.SetText(body,header);
        instance.toooltip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        instance.toooltip.gameObject.SetActive(false);

    }
}
