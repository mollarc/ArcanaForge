using System.Collections.Generic;
using UnityEngine;

public class WandObject : MonoBehaviour
{
    public string wandName;
    public int typeSlots;
    public int modifierSlots;
    public int shapeSlots;
    private int manaCost;
    private int healValue;
    private int damageValue;
    private int shieldValue;
    private float modifierValue;
    public List<WandComponents> wandComponents= new List<WandComponents>();

    public int HealValue { get => healValue; set => healValue = value; }
    public int DamageValue { get => damageValue; set => damageValue = value; }
    public int ShieldValue { get => shieldValue; set => shieldValue = value; }
    public int ManaCost { get => manaCost; set => manaCost = value; }

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
    }

    public void CalculateValues()
    {
        ResetValues();
        
        foreach (var wandComponent in wandComponents)
        {
            manaCost += wandComponent.manaCost;
            healValue += wandComponent.healValue;
            damageValue += wandComponent.damageValue;
            shieldValue += wandComponent.shieldValue;
            modifierValue += wandComponent.modifierValue;

            //Debug.Log(wandComponent.healValue);
            //Debug.Log(wandComponent.damageValue);
            //Debug.Log(wandComponent.shieldValue);
            //Debug.Log(wandComponent.modifierValue);
        }
        Debug.Log(healValue);
        Debug.Log(damageValue);
        Debug.Log(shieldValue);
        //Debug.Log(modifierValue);
        manaCost *= -1;
        healValue = (int)Mathf.Round(healValue * modifierValue);
        damageValue = (int)Mathf.Round(damageValue * modifierValue);
        shieldValue = (int)Mathf.Round(shieldValue * modifierValue);
        Debug.Log(healValue);
        Debug.Log(damageValue);
        Debug.Log(shieldValue);
    }

    public void ResetValues()
    {
        manaCost = 0;
        healValue = 0;
        damageValue = 0;
        shieldValue = 0;
        modifierValue = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
