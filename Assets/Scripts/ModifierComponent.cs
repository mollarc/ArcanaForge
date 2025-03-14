using UnityEngine;
using UnityEngine.UI;

public class ModifierComponent : WandComponent
{
    public ModifierComponentSO modifierComponentData;
    public float modifierValue;//Write percentages as decimals
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
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
