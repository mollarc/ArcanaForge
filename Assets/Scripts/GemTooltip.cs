using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemTooltip : Tooltip
{
    public WandComponent WandComponent;
    public new void OnPointerEnter(PointerEventData eventData)
    {
        WandComponent.UpdateTooltip();
        base.OnPointerEnter(eventData);
    }
}
