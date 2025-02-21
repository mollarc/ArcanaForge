using UnityEngine;
using UnityEngine.UI;

public class TypeComponent : WandComponent
{
    public TypeComponentSO typeComponentData;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(typeComponentData != null)
        {
            LoadComponentData(typeComponentData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadComponentData(TypeComponentSO componentData)
    {
        componentName = componentData.name;
        componentType = componentData.componentType;
        manaCost = componentData.manaCost;
        cooldown = componentData.cooldown;
        gameObject.GetComponent<Image>().sprite = componentData.gemImage;
        healValue = componentData.healValue;
        damageValue = componentData.damageValue;    
        shieldValue = componentData.shieldValue;
        targets = componentData.targets;
    }
}
