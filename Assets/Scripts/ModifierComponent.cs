using UnityEngine;
using UnityEngine.UI;

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
        if(modifierComponentData != null)
        {
            LoadComponentData(modifierComponentData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadComponentData(ModifierComponentSO componentData)
    {
        componentName = componentData.name;
        componentType = componentData.componentType;
        manaCost = componentData.manaCost;
        cooldown = componentData.cooldown;
        gameObject.GetComponent<Image>().sprite = componentData.gemImage;
        modifierValue = componentData.modifierValue;
        numberOfCasts = componentData.numberOfCasts;
        targets = componentData.targets;
    }
}
