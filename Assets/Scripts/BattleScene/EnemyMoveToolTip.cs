using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyMoveTooltip : Tooltip
{
    public Enemy enemy;
    public List<string> effectTooltips;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        effectTooltips.Clear();
        enemy.RefreshMove();
        if (effectTooltips != null)
        {
            print("TipNotNull");
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
