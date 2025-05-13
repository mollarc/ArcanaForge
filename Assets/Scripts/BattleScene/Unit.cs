using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int currentShield;
    public BattleHUD battleHUD;

    public bool isDead = false;

    public List<StackableEffectSO> stackableEffects = new List<StackableEffectSO>();

    public void SetHUD()
    {
        battleHUD.nameText.text = unitName;
        battleHUD.hpSlider.maxValue = maxHP;
        battleHUD.hpSlider.value = currentHP;
        battleHUD.shieldText.text = currentShield.ToString();

        battleHUD.hpText.text = battleHUD.hpSlider.value + " / " + battleHUD.hpSlider.maxValue;
    }

    public void TakeDamage(int dmg)
    {
        if (stackableEffects != null)
        {
            foreach(StackableEffectSO s in stackableEffects)
            {
                if (s.effectName == "Frigid")
                {
                    dmg += s.amount;
                    s.amount -= s.tickAmount;
                }
            }
        }
        {
            
        }
        if (currentShield - dmg < 0)
        {
            dmg -= currentShield;
            currentShield = 0;
        }
        else
        {
            currentShield -= dmg;
            dmg = 0;
        }
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            isDead = true;
        }

        SetHUD();
    }

    public void HealDamage(int healing)
    {
        if (maxHP - currentHP <= healing)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += healing;
        }
    }

    public void GainShield(int shield)
    {
        currentShield += shield;
    }

    public void ResetShield()
    {
        currentShield = 0;
    }

    public void AddStatus(StackableEffectSO _statusEffect)
    {
        if (stackableEffects.Count != 0)
        {
            foreach (StackableEffectSO effect in stackableEffects)
            {
                if (effect.effectName == _statusEffect.effectName)
                {
                    print("Adding to Status");
                    effect.amount += _statusEffect.amount;
                }
                else
                {
                    print("Adding Status");
                    stackableEffects.Add(_statusEffect);
                }
            }
        }
        else
        {
            print("Adding Status");
            stackableEffects.Add(_statusEffect);
        }
        print("End of Add Status");
    }

    public void EndTurnEffects()
    {
        if (stackableEffects != null)
        {
            foreach (StackableEffectSO effect in stackableEffects)
            {
                string effectName = effect.effectName;
                switch (effectName)
                {
                    case "Poison":
                        TakeDamage(effect.amount);
                        effect.TickEffect();
                        break;
                    case "Burn":
                        TakeDamage(effect.amount);
                        effect.TickEffect();
                        break;
                    default:
                        print("Defaulted");
                        break;
                }
            }
        }
        SetHUD();
    }
}

