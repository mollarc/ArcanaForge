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
    public TMP_Text manaCostText;
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
        }
        for (int i = 0; i < modifierSlots; i++)
        {
            Slot slot = Instantiate(slotModPrefab, modifierPanel.transform).GetComponent<Slot>();
        }
    }

    public void CalculateValues()
    {
        ResetValues();

        foreach (Slot typeSlot in wandTypeSlots)
        {
            if (typeSlot.currentItem != null)
            {
                manaCost += typeSlot.currentItem.manaCost;
                healValue += typeSlot.currentItem.healValue;
                damageValue += typeSlot.currentItem.damageValue;
                shieldValue += typeSlot.currentItem.shieldValue;
            }
        }
        foreach (ModifierComponent wandComponent in wandModifierComponents)
        {
            if (wandComponent != null)
            {
                manaCost += wandComponent.manaCost;
                modifierValue += wandComponent.modifierValue;
            }
        }
        if (wandShapeComponents.Count() == 0)
        {
            targets = 0;
        }
        else
        {
            foreach (ShapeComponent wandComponent in wandShapeComponents)
            {
                manaCost += wandComponent.manaCost;

                if (wandComponent.targets > targets)
                {
                    targets = wandComponent.targets;
                }
            }
        }
        manaCostText.text = manaCost.ToString();
        manaCost *= -1;
        healValue = (int)Mathf.Round(healValue * modifierValue);
        damageValue = (int)Mathf.Round(damageValue * modifierValue);
        shieldValue = (int)Mathf.Round(shieldValue * modifierValue);
    }

    public void ResetValues()
    {
        manaCost = 0;
        healValue = 0;
        damageValue = 0;
        shieldValue = 0;
        modifierValue = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

