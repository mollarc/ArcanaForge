using UnityEngine;
using UnityEngine.UI;
using ModifierComponentNS;
using System.Collections.Generic;
public class ModifierComponent : WandComponent
{
    public ModifierComponentSO modifierComponentData;
    public float modifierValue;//Write percentages as decimals
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    public List<moves> moveInfos;
    public List<StackableEffectSO> statusEffects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public new void LoadComponentData(ModifierComponentSO componentData)
    {
        base.LoadComponentData(componentData);
        modifierValue = componentData.modifierValue;
        numberOfCasts = componentData.numberOfCasts;
        targets = componentData.targets;
        foreach (moves move in componentData.moveInfos)
        {
            moveInfos.Add(move);
        }
        statusEffects = componentData.statusEffects;
        UpdateTooltip();
    }

    public override void UpdateTooltip()
    {
        base.UpdateTooltip();
        if(statusEffects != null)
        {
            foreach(StackableEffectSO s in statusEffects)
            {
                tooltip.GetEffectTooltip(s.effectName +": " +s.effectTooltip);
            }
        }
    }
}
