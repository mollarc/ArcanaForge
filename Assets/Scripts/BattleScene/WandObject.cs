using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    public float outsideDMGModifierPercent = 1f;
    public int outsideDMGModifierFlat;
    public int outsideEffectDMG;
    public int targets;
    public List<string> moveInfos = new List<string>();
    public List<StackableEffectSO> cloneDebuffEffects = new List<StackableEffectSO>();
    public List<StackableEffectSO> cloneBuffEffects = new List<StackableEffectSO>();
    public TMP_Text wandMoveText;
    public TMP_Text manaCostText;
    public Image castButton;
    public GameObject typePanel;
    public GameObject modifierPanel;
    public GameObject slotTypePrefab;
    public GameObject slotModPrefab;
    public GameObject slotShapePrefab;
    public Slot[] wandTypeSlots;
    public Slot[] wandModifierSlots;
    public Slot[] wandShapeSlots;
    public List<string> FmodSoundPaths;



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

    void Update()
    {

    }

    public void SetUP()
    {
        wandName = wandData.name;
        typeSlots = wandData.typeSlots;
        modifierSlots = wandData.modifierSlots;
        shapeSlots = wandData.shapeSlots;
        wandTypeSlots = new Slot[typeSlots];
        wandModifierSlots = new Slot[modifierSlots];
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
        wandMoveText.text = "";
        moveInfos.Clear();
        targets = 0;
        if (wandTypeSlots.Count() != 0)
        {
            foreach (Slot typeSlot in wandTypeSlots)
            {
                if (typeSlot.currentItem != null)
                {
                    TypeComponent typeComponent = typeSlot.currentItem.GetComponent<TypeComponent>();
                    manaCost += typeComponent.manaCost;
                    healValue += typeComponent.healValue;
                    damageValue += typeComponent.damageValue;
                    shieldValue += typeComponent.shieldValue;
                    FmodSoundPaths.Add(typeComponent.FmodEventPath);
                    if (typeComponent.targets > targets)
                    {
                        targets = typeComponent.targets;
                    }
                    foreach (var move in typeComponent.moveInfos)
                    {
                        List<string> tempList = new List<string>();
                        if (moveInfos.Count != 0)
                        {
                            bool isInList = false;
                            foreach (string inList in moveInfos)
                            {
                                if (inList == move.ToString())
                                {
                                    isInList = true;
                                    break;
                                }
                            }
                            if (!isInList)
                            {
                                tempList.Add(move.ToString());
                            }
                            if (tempList.Count != 0)
                            {
                                foreach (string inList2 in tempList)
                                {
                                    moveInfos.Add(inList2.ToString());
                                }
                            }
                        }
                        else
                        {
                            moveInfos.Add(move.ToString());
                        }
                    }
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
                    ModifierComponent modifierComponent = modifierSlot.currentItem.GetComponent<ModifierComponent>();
                    manaCost += modifierSlot.currentItem.GetComponent<ModifierComponent>().manaCost;
                    modifierValue += modifierSlot.currentItem.GetComponent<ModifierComponent>().modifierValue;
                    FmodSoundPaths.Add(modifierComponent.FmodEventPath);
                    if (modifierComponent.targets > targets)
                    {
                        targets = modifierComponent.targets;
                    }
                    foreach (var move in modifierComponent.moveInfos)
                    {
                        List<string> tempList = new List<string>();
                        if (moveInfos.Count != 0)
                        {
                            bool isInList = false;
                            foreach (string inList in moveInfos)
                            {
                                if (inList == move.ToString())
                                {
                                    isInList = true;
                                    break;
                                }
                            }
                            if (!isInList)
                            {
                                tempList.Add(move.ToString());
                            }
                            if (tempList.Count != 0)
                            {
                                foreach (string inList2 in tempList)
                                {
                                    moveInfos.Add(inList2.ToString());
                                }
                            }
                        }
                        else
                        {
                            moveInfos.Add(move.ToString());
                        }
                    }
                    if (modifierComponent.statusEffects != null)
                    {
                        foreach (StackableEffectSO statusEffectData in modifierComponent.statusEffects)
                        {
                            StackableEffectSO stackableClone = Instantiate(statusEffectData);
                            if (statusEffectData.type == 0)
                            {
                                cloneDebuffEffects.Add(stackableClone);
                            }
                            else if (statusEffectData.type == 1)
                            {
                                cloneBuffEffects.Add(stackableClone);
                            }

                        }
                    }
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
                    string moveText = shapeSlot.currentItem.GetComponent<TypeComponent>().moveInfo;
                    if (!wandMoveText.text.Contains(moveText))
                    {
                        wandMoveText.text += moveText + " ";
                    }
                }
            }
        }
        manaCostText.text = manaCost.ToString();
        manaCost *= -1;
        foreach (StackableEffectSO playerEffect in Player.playerInstance.stackableEffects)
        {
            switch (playerEffect.effectData.effectName)
            {
                case "Power":
                    damageValue += playerEffect.amount;
                    break;
                case "Weak":
                    outsideDMGModifierPercent = 0.75f;
                    break;
            }
        }
        healValue = (int)Mathf.Round(healValue * modifierValue);
        if(damageValue > 0)
        {
            damageValue = (int)Mathf.Round(((damageValue + outsideDMGModifierFlat) * modifierValue * outsideDMGModifierPercent) + outsideEffectDMG);
        }
        shieldValue = (int)Mathf.Round(shieldValue * modifierValue);
        if (moveInfos != null)
        {
            foreach (string inList in moveInfos)
            {
                switch (inList)
                {
                    case "Damage":
                        wandMoveText.text += damageValue.ToString() + " Damage ";
                        break;
                    case "Shield":
                        wandMoveText.text += shieldValue.ToString() + " Shield ";
                        break;
                    case "Heal":
                        wandMoveText.text += healValue.ToString() + " Heal ";
                        break;
                }
            }
        }
        if (cloneDebuffEffects != null)
        {
            foreach (StackableEffectSO stackableEffectSO in cloneDebuffEffects)
            {
                switch (stackableEffectSO.effectData.effectName)
                {
                    case "Poison":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Poison ";
                        break;
                    case "Burn":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Burn ";
                        break;
                    case "Frigid":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Frigid ";
                        break;
                    case "Weak":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Weak ";
                        break;
                    case "Fragile":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Fragile ";
                        break;
                }
            }
        }
        if (cloneBuffEffects != null)
        {
            foreach (StackableEffectSO stackableEffectSO in cloneBuffEffects)
            {
                switch (stackableEffectSO.effectData.effectName)
                {
                    case "Power":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Power ";
                        break;
                    case "Hailstorm":
                        wandMoveText.text += stackableEffectSO.amount.ToString() + " Hailstorm ";
                        break;
                }
            }
        }
    }

    public void CheckMana()
    {
        if (manaCost * -1 > Player.playerInstance.currentMana)
        {
            castButton.color = Color.red;
        }
        else
        {
            castButton.color = Color.white;
        }
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
                    if (typeSlot.currentItem.GetComponent<TypeComponent>().EndTurn())
                    {
                        print("Moving Component");
                        typeSlot.currentItem.GetComponent<TypeComponent>().MoveComponent();
                    }
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
                    if (modifierSlot.currentItem.GetComponent<ModifierComponent>().EndTurn())
                    {
                        modifierSlot.currentItem.GetComponent<ModifierComponent>().MoveComponent();
                    }
                }
            }
        }

        if (wandShapeSlots.Count() != 0)
        {
            foreach (Slot shapeSlot in wandShapeSlots)
            {
                if (shapeSlot.currentItem.GetComponent<ShapeComponent>().targets > targets)
                {
                    if (shapeSlot.currentItem.GetComponent<ShapeComponent>().EndTurn())
                    {
                        shapeSlot.currentItem.GetComponent<ShapeComponent>().MoveComponent();
                    }

                }
            }
        }
        CalculateValues();
    }

    public void ResetValues()
    {
        manaCost = 0;
        healValue = 0;
        damageValue = 0;
        shieldValue = 0;
        modifierValue = 1;
        cloneDebuffEffects.Clear();
        cloneBuffEffects.Clear();
        FmodSoundPaths.Clear();
    }
}

