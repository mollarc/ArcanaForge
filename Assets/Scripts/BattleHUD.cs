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
    public Slider manaSlider;


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
        manaSlider.maxValue = player.maxMana;
        manaSlider.value = player.currentMana;

        manaText.text = manaSlider.value + "\n/\n" + manaSlider.maxValue;
    }

    public void SetHP(int hp)
    {
        hpSlider.value=hp;
        hpText.text = hpSlider.value + "/" + hpSlider.maxValue;
    }

    public void SetMana(Player player)
    {
        //Things that cost mana should be inputted as a negative number
        manaSlider.value = player.currentMana;
        manaText.text = manaText.text = manaSlider.value + "\n/\n" + manaSlider.maxValue;
    }

    public void SetShield(Unit unit)
    {
        shieldText.text = unit.currentShield.ToString();
    }

    public void setMove(string move)
    {
        moveText.text = move;
    }
}
