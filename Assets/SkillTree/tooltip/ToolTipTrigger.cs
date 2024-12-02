using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public string header;
    [Multiline()]
    public string body;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipSystem.Show(body,header);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Hide();
    }

    
}
