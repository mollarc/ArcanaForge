using UnityEngine;

public class ModifierComponent : WandComponent
{
    public ModifierComponentSO modifierComponentData;
    //Debuff Type
    //Buff Type
    //Debuff Duration
    //Buff Duration
    //Write percentages as decimals
    public float modifierValue;
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        componentName = modifierComponentData.name;
        componentType = modifierComponentData.componentType;
        manaCost = modifierComponentData.manaCost;
        cooldown = modifierComponentData.cooldown;
        modifierValue = modifierComponentData.modifierValue;
        numberOfCasts = modifierComponentData.numberOfCasts;
        targets = modifierComponentData.targets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
