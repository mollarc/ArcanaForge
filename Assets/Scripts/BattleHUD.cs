using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text hpText;
    public TMP_Text manaText;
    public TMP_Text shieldText;
    public TMP_Text moveText;
    public Slider hpSlider;

    public GameObject statusPanel;

    public GameObject statusEffect;

    public List<GameObject> statusEffectList;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        shieldText.text = unit.currentShield.ToString();
    
        hpText.text = hpSlider.value + "/" + hpSlider.maxValue;
    }

    public void SetHUDPlayer(Player player)
    {
        manaText.text = player.currentMana + "\n/\n" + player.maxMana;
    }

    public void SetHP(int hp)
    {
        hpSlider.value=hp;
        hpText.text = hpSlider.value + "/" + hpSlider.maxValue;
    }

    public void SetMana(Player player)
    {
        //Things that cost mana should be inputted as a negative number
        manaText.text = manaText.text = player.currentMana + "\n/\n" + player.maxMana;
    }

    public void SetShield(Unit unit)
    {
        shieldText.text = unit.currentShield.ToString();
    }

    public void SetMove(string move)
    {
        moveText.text = move;
    }

    public void DisplayStatus(List<StackableEffectSO> list)
    {
        if (list.Count != 0)
        {
            foreach (StackableEffectSO effect in list)
            {
                if(statusEffectList.Count != 0)
                {
                    List<GameObject> statusesToRemove = new List<GameObject>();
                    foreach (GameObject status in statusEffectList)
                    {
                        if(status.GetComponentInChildren<Image>().sprite == effect.effectIcon)
                        {
                            Destroy(status);
                            statusesToRemove.Add(status);
                        }
                    }
                    foreach(GameObject status in statusesToRemove)
                    {
                        print("Removing Dupe Status");
                        statusEffectList.Remove(status);
                    }
                }
                GameObject item = Instantiate(statusEffect, statusPanel.transform);
                item.GetComponentInChildren<TMP_Text>().text = effect.amount.ToString();
                item.GetComponentInChildren<Image>().sprite = effect.effectIcon;
                string tooltipText = "<color=" +effect.effectColor + ">" + effect.effectName.ToUpper() + "</color>: " + effect.effectTooltip.Replace("X", "<color=" + effect.effectColor +">" + effect.amount.ToString() + "</color>");
                item.GetComponentInChildren<Tooltip>().ReplaceTooltipText(tooltipText);
                statusEffectList.Add(item);
            }
        }
    }

}
