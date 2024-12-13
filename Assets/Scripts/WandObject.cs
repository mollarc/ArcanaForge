using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

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
    //public List<WandComponents> wandTypeComponents= new List<WandComponents>();
    //public List<WandComponents> wandModifierComponents= new List<WandComponents>();
    public List <WandComponents> components;
    public WandComponents tempComponent1;
    public WandComponents tempComponent2;
    public WandComponents tempComponent3;
    public TMP_Dropdown typeDropdown1;
    public TMP_Dropdown typeDropdown2;
    public TMP_Dropdown modifierDropdown3;
    public WandComponents[] wandTypeComponents;
    public WandComponents[] wandModifierComponents;


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

    public void SetUP()
    {
        wandTypeComponents = new WandComponents[typeSlots];
        wandModifierComponents = new WandComponents[modifierSlots];
    }

    public void ChangeType1Component()
    {
        if (!(wandTypeComponents[0] == null))
        {
            typeDropdown2.options.Add(new TMP_Dropdown.OptionData(wandTypeComponents[0].componentName));
        }
        else
        {
            typeDropdown1.options.RemoveAt(0);
        }
        foreach(var wandComponent in components)
        {
            if(wandComponent.componentName == typeDropdown1.options[typeDropdown1.value].text)
            {
                wandTypeComponents[0] = wandComponent;
            }
        }
        typeDropdown2.options.Remove(typeDropdown1.options[typeDropdown1.value]);
        typeDropdown1.RefreshShownValue();
    }
    public void ChangeType2Component()
    {
        if (!(wandTypeComponents[1] == null))
        {
            typeDropdown1.options.Add(new TMP_Dropdown.OptionData(wandTypeComponents[1].componentName));
        }
        else
        {
            typeDropdown2.options.RemoveAt(0);
        }
        foreach (var wandComponent in components)
        {
            if (wandComponent.componentName == typeDropdown2.options[typeDropdown2.value].text)
            {
                wandTypeComponents[1] = wandComponent;
            }
        }
        typeDropdown1.options.Remove(typeDropdown1.options[typeDropdown2.value]);
        typeDropdown2.RefreshShownValue();
    }

    public void ChangeModifierComponent()
    {
        if (!(wandModifierComponents[0] == null))
        {
            modifierDropdown3.options.Add(new TMP_Dropdown.OptionData(wandModifierComponents[0].componentName));
        }
        else
        {
            //modifierDropdown3.options.RemoveAt(0);
        }
        Debug.Log(modifierDropdown3.value);
        foreach (var wandComponent in components)
        {
            if (wandComponent.componentName == modifierDropdown3.options[modifierDropdown3.value].text)
            {
                wandModifierComponents[0] = wandComponent;
            }
        }
        modifierDropdown3.options.RemoveAt(modifierDropdown3.value);
        modifierDropdown3.RefreshShownValue();
    }

    public void ChangeComponent()
    {

    }

    public void CalculateValues()
    {
        ResetValues();
        
        foreach (var wandComponent in wandTypeComponents)
        {
            manaCost += wandComponent.manaCost;
            healValue += wandComponent.healValue;
            damageValue += wandComponent.damageValue;
            shieldValue += wandComponent.shieldValue;
            modifierValue += wandComponent.modifierValue;
        }
        foreach (var wandComponent in wandModifierComponents)
        {
            manaCost += wandComponent.manaCost;
            healValue += wandComponent.healValue;
            damageValue += wandComponent.damageValue;
            shieldValue += wandComponent.shieldValue;
            modifierValue += wandComponent.modifierValue;
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
