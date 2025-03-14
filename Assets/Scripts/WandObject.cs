using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class WandObject : MonoBehaviour
{
    public WandObjectSO wandData;
    public string wandName;
    public int typeSlots;
    public int modifierSlots;
    public int shapeSlots;
    private int manaCost;
    private int healValue;
    private int damageValue;
    private int shieldValue;
    private float modifierValue;
    public int targets;
    public TMP_Text wandTypeText;
    public GameObject typePanel;
    public GameObject modifierPanel;
    public GameObject slotTypePrefab;
    public GameObject slotModPrefab;
    public GameObject slotShapePrefab;
    public Slot[] wandTypeSlots;
    public Slot[] wandModifierSlots;
    public Slot[] wandShapeSlots;


    public int HealValue { get => healValue; set => healValue = value; }
    public int DamageValue { get => damageValue; set => damageValue = value; }
    public int ShieldValue { get => shieldValue; set => shieldValue = value; }
    public int ManaCost { get => manaCost; set => manaCost = value; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetValues();
        SetUP();
    }

    public void SetUP()
    {
        wandName = wandData.name;
        typeSlots = wandData.typeSlots;
        modifierSlots = wandData.modifierSlots;
        shapeSlots = wandData.shapeSlots;
        wandTypeSlots = new Slot[typeSlots];
        wandModifierSlots = new Slot[modifierSlots];
        typePanel = gameObject.transform.GetChild(0).gameObject;
        modifierPanel = gameObject.transform.GetChild(1).gameObject;
        for (int i = 0; i < typeSlots; i++)
        {
            Slot slot = Instantiate(slotTypePrefab, typePanel.transform).GetComponent<Slot>();
            wandTypeSlots[i] = slot;
        }
        for (int i = 0; i < modifierSlots; i++)
        {
            Slot slot = Instantiate(slotModPrefab, modifierPanel.transform).GetComponent<Slot>();
            wandModifierSlots[i] = slot;
        }
    }

    public void CalculateValues()
    {
        ResetValues();

        if(wandTypeSlots.Count() != 0)
        {
            foreach (Slot typeSlot in wandTypeSlots)
            {
                if (typeSlot.currentItem != null)
                {
                    manaCost += typeSlot.currentItem.GetComponent<TypeComponent>().manaCost;
                    healValue += typeSlot.currentItem.GetComponent<TypeComponent>().healValue;
                    damageValue += typeSlot.currentItem.GetComponent<TypeComponent>().damageValue;
                    shieldValue += typeSlot.currentItem.GetComponent<TypeComponent>().shieldValue;
                }
            }
        }
        if(wandModifierSlots.Count() != 0)
        {
            //Buffs/Debuffs that affect values should adjust the modifier value
            foreach (Slot modifierSlot in wandModifierSlots)
            {
                if (modifierSlot.currentItem != null)
                {
                    manaCost += modifierSlot.currentItem.GetComponent<ModifierComponent>().manaCost;
                    modifierValue += modifierSlot.currentItem.GetComponent<ModifierComponent>().modifierValue;
                }
            }
        }
        
        if (wandShapeSlots.Count() != 0)
        {
            foreach (Slot shapeSlot in wandShapeSlots)
            {
                manaCost += shapeSlot.currentItem.GetComponent<ShapeComponent>().manaCost;

                if (shapeSlot.currentItem.GetComponent<ShapeComponent>().targets > targets)
                {
                    targets = shapeSlot.currentItem.GetComponent<ShapeComponent>().targets;
                }
            }
        }
        else
        {
            targets = 0;
        }
        //manaCostText.text = manaCost.ToString();
        manaCost *= -1;
        healValue = (int)Mathf.Round(healValue * modifierValue);
        damageValue = (int)Mathf.Round(damageValue * modifierValue);
        shieldValue = (int)Mathf.Round(shieldValue * modifierValue);

        //DisplayTypeValues();
    }

    public void MarkUsed()
    {
        if (wandTypeSlots.Count() != 0)
        {
            foreach (Slot typeSlot in wandTypeSlots)
            {
                if (typeSlot.currentItem != null)
                {
                    typeSlot.currentItem.GetComponent<TypeComponent>().wasUsed = true;
                }
            }
        }
        if (wandModifierSlots.Count() != 0)
        {
            //Buffs/Debuffs that affect values should adjust the modifier value
            foreach (Slot modifierSlot in wandModifierSlots)
            {
                if (modifierSlot.currentItem != null)
                {
                    modifierSlot.currentItem.GetComponent<ModifierComponent>().wasUsed = true;
                }
            }
        }

        if (wandShapeSlots.Count() != 0)
        {
            foreach (Slot shapeSlot in wandShapeSlots)
            {
                if (shapeSlot.currentItem.GetComponent<ShapeComponent>().targets > targets)
                {
                    shapeSlot.currentItem.GetComponent<ShapeComponent>().wasUsed = true;
                }
            }
        }
    }

    public void EndTurn()
    {
        if (wandTypeSlots.Count() != 0)
        {
            foreach (Slot typeSlot in wandTypeSlots)
            {
                if (typeSlot.currentItem != null)
                {
                    typeSlot.currentItem.GetComponent<TypeComponent>().EndTurn();
                }
            }
        }
        if (wandModifierSlots.Count() != 0)
        {
            //Buffs/Debuffs that affect values should adjust the modifier value
            foreach (Slot modifierSlot in wandModifierSlots)
            {
                if (modifierSlot.currentItem != null)
                {
                    modifierSlot.currentItem.GetComponent<ModifierComponent>().EndTurn();
                }
            }
        }

        if (wandShapeSlots.Count() != 0)
        {
            foreach (Slot shapeSlot in wandShapeSlots)
            {
                if (shapeSlot.currentItem.GetComponent<ShapeComponent>().targets > targets)
                {
                    shapeSlot.currentItem.GetComponent<ShapeComponent>().EndTurn();
                }
            }
        }
    }

    public void ResetValues()
    {
        manaCost = 0;
        healValue = 0;
        damageValue = 0;
        shieldValue = 0;
        modifierValue = 1;
    }

    public void DisplayTypeValues()
    {
        wandTypeText.text = "Mana Cost: " + manaCost;

        if(damageValue > 0)
        {
            wandTypeText.text += " Damage:" + damageValue;
        }
        if (shieldValue > 0)
        {
            wandTypeText.text += " Shield: " + shieldValue;
        }
        if (healValue > 0)
        {
            wandTypeText.text += " Heal: " + healValue;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

