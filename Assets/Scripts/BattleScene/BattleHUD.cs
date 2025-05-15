using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text hpText;
    public TMP_Text currentManaText;
    public TMP_Text maxManaText;
    public TMP_Text shieldText;
    public TMP_Text moveText;
    public Slider hpSlider;

    public GameObject statusPanel;

    public GameObject statusEffect;

    public List<GameObject> statusEffectList;

    public void SetHUDPlayer(Player player)
    {
        currentManaText.text = player.currentMana.ToString();
        maxManaText.text = player.maxMana.ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.value=hp;
        hpText.text = hpSlider.value + " / " + hpSlider.maxValue;
    }

    public void SetMana(Player player)
    {
        //Things that cost mana should be inputted as a negative number
        currentManaText.text = player.currentMana.ToString();
        maxManaText.text = player.maxMana.ToString();
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
        ClearNulls();
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
                        statusEffectList.Remove(status);
                    }
                }
                GameObject item = Instantiate(statusEffect, statusPanel.transform);
                item.GetComponentInChildren<TMP_Text>().text = effect.amount.ToString();
                item.GetComponentInChildren<Image>().sprite = effect.effectIcon;
                string tooltipText;
                if(effect.scalesWithAmount)
                {
                    tooltipText = effect.effectTooltip.Replace("X", "<color=" + effect.effectColor + ">" + effect.amount.ToString() + "</color>");
                }
                else
                {
                    tooltipText = effect.effectTooltip;
                }
                Tooltip tooltip = item.GetComponentInChildren<Tooltip>();
                tooltip.tooltipDescription = tooltipText;
                tooltip.tooltipName = "<color=" + effect.effectColor + ">" + effect.effectName.ToUpper() + "</color>";
                tooltip.tooltipSprite = effect.effectIcon;
                statusEffectList.Add(item);
            }
        }
    }

    public void RemoveStatus(int index)
    {
        Destroy(statusEffectList[index]);
    }

    public void ClearNulls()
    {
        for(int i = statusEffectList.Count-1; i >= 0; i--)
        {
            if (statusEffectList[i] == null)
            {
                statusEffectList.RemoveAt(i);
            }
        }
    }
}
