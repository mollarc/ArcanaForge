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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.SetUP();
        if (modifierComponentData != null)
        {
            LoadComponentData(modifierComponentData);
        }
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
        UpdateTooltip();
    }
}
