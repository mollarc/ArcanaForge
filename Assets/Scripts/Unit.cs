using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int currentShield;

    public bool isDead = false;

    public List<StackableEffect> stackableEffects = new List<StackableEffect>();

    public bool TakeDamage(int dmg)
    {
        if(currentShield - dmg < 0)
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
            return true;
        }
        else
        {
            return false;
        }
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

    public void AddStatus(StackableEffect _statusEffect)
    {
        print(_statusEffect._status.GetEffectName());
        if(stackableEffects != null)
        {
            foreach(StackableEffect effect in stackableEffects)
            {
                if(effect._status == _statusEffect._status)
                {
                    print("Adding to Status");
                    effect._status.AddAmount(_statusEffect._status.GetAmount());
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
        if(stackableEffects != null)
        {
            foreach (StackableEffect effect in stackableEffects)
            {
                string effectName = effect._status.GetEffectName();
                print(effectName);
                print(effect._status.GetAmount());
                switch (effectName)
                {
                    case "Poison":
                        TakeDamage(effect._status.GetAmount());
                        effect._status.ActivateEffect();
                        print(effect._status.GetAmount().ToString());
                        break;
                    case "Burn":
                        TakeDamage(effect._status.GetAmount());
                        effect._status.ActivateEffect();
                        print(effect._status.GetAmount().ToString());
                        break;
                    default:
                        print("Defaulted");
                        break;
                }
            }
        }
    }
}
