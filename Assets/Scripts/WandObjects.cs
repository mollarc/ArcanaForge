using System.Collections.Generic;
using System.Linq;
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
    public int targets;
    public TMP_Text manaCostText;
    public List <WandComponents> components;
    public TMP_Dropdown.OptionData tempData;
    public WandComponents tempComponent2;
    public WandComponents tempComponent3;
    public TMP_Dropdown typeDropdown1;
    public TMP_Dropdown typeDropdown2;
    public TMP_Dropdown modifierDropdown3;
    public WandComponents[] wandTypeComponents;
    public WandComponents[] wandModifierComponents;
    public WandComponents[] wandShapeComponents;


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
        tempData = modifierDropdown3.options[0];
    }

    public void ChangeType1Component()
    {
        if (!(wandTypeComponents[0] == null))
        {
            typeDropdown2.options.Add(new TMP_Dropdown.OptionData(wandTypeComponents[0].componentName));
        }
        for (int i = 0; i < typeDropdown2.options.Count; i++)
        {
            if(typeDropdown1.options[typeDropdown1.value].text == null)
            {

            }
            else if (typeDropdown1.options[typeDropdown1.value].text == typeDropdown2.options[i].text)
            {
                print("Drop2 "+ typeDropdown2.value + " i: " +i);
                
                typeDropdown2.options.RemoveAt(i);
                if (i == 0 && typeDropdown2.value > 0)
                {
                    typeDropdown2.value = 0;
                }

            }
        }
        
        foreach(var wandComponent in components)
        {
            if(wandComponent.componentName == typeDropdown1.options[typeDropdown1.value].text)
            {
                wandTypeComponents[0] = wandComponent;
            }
        }
        
        typeDropdown1.RefreshShownValue();
    }
    public void ChangeType2Component()
    {
        if (!(wandTypeComponents[1] == null))
        {
            typeDropdown1.options.Add(new TMP_Dropdown.OptionData(wandTypeComponents[1].componentName));
        }
        for (int i = 0; i < typeDropdown1.options.Count; i++)
        {
            if (typeDropdown2.options[typeDropdown2.value].text == typeDropdown1.options[i].text)
            {
                print("Drop1 " + typeDropdown1.value + " i: " + i);
                
                typeDropdown1.options.RemoveAt(i);
                if (i == 0 && typeDropdown1.value > 0)
                {
                    typeDropdown1.value = 0;
                }

            }
        }
        
        foreach (var wandComponent in components)
        {
            if (wandComponent.componentName == typeDropdown2.options[typeDropdown2.value].text)
            {
                wandTypeComponents[1] = wandComponent;
            }
        }
        
        typeDropdown2.RefreshShownValue();
    }

    public void ChangeModifierComponent()
    {
        foreach (var wandComponent in components)
        {
            print(modifierDropdown3.value);
            if (wandComponent.componentName == modifierDropdown3.options[modifierDropdown3.value].text)
            {
                wandModifierComponents[0] = wandComponent;
            }
        }
        modifierDropdown3.RefreshShownValue();
    }

    public void CalculateValues()
    {
        ResetValues();

        foreach (var wandComponent in wandTypeComponents)
        {
            if(wandComponent != null)
            {
                manaCost += wandComponent.manaCost;
                healValue += wandComponent.healValue;
                damageValue += wandComponent.damageValue;
                shieldValue += wandComponent.shieldValue;
                modifierValue += wandComponent.modifierValue;
            } 
        }
        foreach (var wandComponent in wandModifierComponents)
        {
            if (wandComponent != null)
            {
                manaCost += wandComponent.manaCost;
                healValue += wandComponent.healValue;
                damageValue += wandComponent.damageValue;
                shieldValue += wandComponent.shieldValue;
                modifierValue += wandComponent.modifierValue;
            }
        }
        if(wandShapeComponents.Count() == 0)
        {
            targets = 0;
        }
        else
        {
            foreach (var wandComponent in wandShapeComponents)
            {
                manaCost += wandComponent.manaCost;

                if(wandComponent.targets > targets)
                {
                    targets = wandComponent.targets;
                }
            }
        }

        Debug.Log(healValue);
        Debug.Log(damageValue);
        Debug.Log(shieldValue);
        //Debug.Log(modifierValue);
        manaCostText.text = manaCost.ToString();
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
