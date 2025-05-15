using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemTooltip : Tooltip
{
    public WandComponent wandComponent;
    public List<string> effectTooltips;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        effectTooltips.Clear();
        wandComponent.UpdateTooltip();
        if (effectTooltips != null)
        {
            foreach (string effect in effectTooltips)
            {
                itemInspector.effectDescriptions.Add(effect);
            }
        }
        itemInspector.LoadEffectTooltips();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        
        if (tooltipDisplay)
        {
            tooltipDisplay = false;
            itemInspector.ClearInspector();
            itemInspector.ClearEffectTooltips();
        }
    }

    public void GetEffectTooltip(string _effectTooltips)
    {
        effectTooltips.Add(_effectTooltips);
    }
}
