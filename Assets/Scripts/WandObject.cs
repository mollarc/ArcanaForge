using System.Collections.Generic;
using UnityEngine;

public class WandObject : MonoBehaviour
{
    public string wandName;
    public int typeSlots;
    public int modifierSlots;
    public int shapeSlots;
    public int healValue = 0;
    public int damageValue = 0;
    public int shieldValue = 0;
    public float modifierValue = 1;
    public List<WandComponents> wandComponents= new List<WandComponents>();

    public WandObject(string _wandName, int _typeSlots, int _modifierSlots, int _shapeSlots)
    {
        wandName = _wandName;
        typeSlots = _typeSlots;
        modifierSlots = _modifierSlots;
        shapeSlots = _shapeSlots;
    }

    public void AddWandComponent(WandComponents _wandComponent)
    {
        wandComponents.Add(_wandComponent);
    }
    
    public void RemoveWandComponent(string wandName)
    {
        //Search through list and remove item
        //Maybe components should be an array? may have future implications if relics get added
    }

    public void CalculateValues()
    {
        foreach(var wandComponent in wandComponents)
        {
            healValue += wandComponent.healValue;
            damageValue += wandComponent.damageValue;
            shieldValue += wandComponent.shieldValue;
            modifierValue += wandComponent.modifierValue;
        }

        healValue = (int)Mathf.Round(healValue * modifierValue);
        damageValue = (int)Mathf.Round(damageValue * modifierValue);
        shieldValue = (int)Mathf.Round(shieldValue * modifierValue);
    }

    public void ResetValues()
    {
        healValue = 0;
        damageValue = 0;
        shieldValue = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
