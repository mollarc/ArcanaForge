using FMOD.Studio;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int currentShield;
    public BattleHUD battleHUD;

    public float dmgIncomingPercent;
    public float dmgOutgoingPercent;
    public int dmgIncomingFlat;
    public int dmgOutgoingFlat;

    public bool isDead = false;

    public string FmodHitSoundPath;

    public List<StackableEffectSO> stackableEffects = new List<StackableEffectSO>();
    public List<StackableEffectSO> effectsThatAffectOthers = new List<StackableEffectSO>();

    public void Start()
    {
        dmgIncomingPercent = 1f;
    }
    public void SetHUD()
    {
        battleHUD.nameText.text = unitName;
        battleHUD.hpSlider.maxValue = maxHP;
        battleHUD.hpSlider.value = currentHP;
        battleHUD.shieldText.text = currentShield.ToString();

        battleHUD.hpText.text = battleHUD.hpSlider.value + " / " + battleHUD.hpSlider.maxValue;
    }

    public virtual void TakeDamage(int dmg, bool isHit)
    {
        if(dmg != 0)
        {
            EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(FmodHitSoundPath);
            if (eventInstance.isValid())
            {
                eventInstance.start();
            }
        }
        dmgIncomingFlat = 0;
        GetModifiers();
        if (stackableEffects != null)
        {
            List<StackableEffectSO> effectsToRemove = new List<StackableEffectSO>();
            foreach (StackableEffectSO s in stackableEffects)
            {
                if (isHit)
                {
                    switch (s.effectName)
                    {
                        case "Frigid":
                            if(dmg <= 0)
                            {
                                break;
                            }
                            //dmgIncomingFlat += s.amount;
                            s.TickEffect();
                            if (s.amount <= 0)
                            {
                                effectsToRemove.Add(s);
                                battleHUD.RemoveStatus(stackableEffects.IndexOf(s));
                            }
                            break;
                    }
                }
            }
            foreach(StackableEffectSO s in effectsToRemove)
            {
                stackableEffects.Remove(s);
            }
        }
        if (isHit)
        {
            //dmg = (int)Mathf.Round(dmg * dmgIncomingPercent);
            dmg += dmgIncomingFlat;
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

    public void TakeHPLoss(int hpLoss)
    {
        currentHP -= hpLoss;

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
        bool hasStatus = false;
        List<StackableEffectSO> effectsToAdd = new List<StackableEffectSO>();
        if (stackableEffects.Count != 0)
        {
            foreach (StackableEffectSO effect in stackableEffects)
            {
                if (effect.effectName == _statusEffect.effectName)
                {
                    hasStatus = true;
                    effect.amount += _statusEffect.amount;
                }
            }
            if (!hasStatus)
            {
                effectsToAdd.Add(_statusEffect);
            }
        }
        else
        {
            effectsToAdd.Add(_statusEffect);
        }
        stackableEffects.AddRange(effectsToAdd);
    }

    public void StartTurnEffects()
    {
        List<StackableEffectSO> effectsToRemove = new List<StackableEffectSO>();
        if (stackableEffects != null)
        {
            foreach (StackableEffectSO effect in stackableEffects)
            {
                if (effect.turnStartTick)
                {
                    string effectName = effect.effectName;
                    switch (effectName)
                    {
                        default:
                            effect.TickEffect();
                            if (effect.amount <= 0)
                            {
                                effectsToRemove.Add(effect);
                                battleHUD.RemoveStatus(stackableEffects.IndexOf(effect));
                            }
                            break;
                    }
                }
            }
        }
        foreach (StackableEffectSO effect in effectsToRemove)
        {
            stackableEffects.Remove(effect);
        }
        SetHUD();
    }

    public void EndTurnEffects()
    {
        effectsThatAffectOthers.Clear();
        List<StackableEffectSO> effectsToRemove = new List<StackableEffectSO>();
        if (stackableEffects != null)
        {
            foreach (StackableEffectSO effect in stackableEffects)
            {
                if (!effect.turnStartTick)
                {
                    string effectName = effect.effectName;
                    switch (effectName)
                    {
                        case "Poison":
                            TakeHPLoss(effect.amount);
                            effect.TickEffect();
                            if (effect.amount <= 0)
                            {
                                effectsToRemove.Add(effect);
                                battleHUD.RemoveStatus(stackableEffects.IndexOf(effect));
                            }
                            break;
                        case "Burn":
                            TakeDamage(effect.amount, false);
                            effect.TickEffect();
                            if (effect.amount <= 0)
                            {
                                effectsToRemove.Add(effect);
                                battleHUD.RemoveStatus(stackableEffects.IndexOf(effect));
                            }
                            break;
                        case "Hailstorm":
                            effectsThatAffectOthers.Add(effect);
                            break;
                        case "Frigid":
                            break;
                        default:
                            effect.TickEffect();
                            if (effect.amount <= 0)
                            {
                                effectsToRemove.Add(effect);
                                battleHUD.RemoveStatus(stackableEffects.IndexOf(effect));
                            }
                            break;
                    }
                }
            }
        }
        foreach (StackableEffectSO effect in effectsToRemove)
        {
            stackableEffects.Remove(effect);
        }
        SetHUD();
    }

    public void GetModifiers()
    {
        dmgIncomingPercent = 1f;
        dmgOutgoingPercent = 0f;
        dmgIncomingFlat = 0;
        dmgOutgoingFlat = 0;
        foreach (StackableEffectSO stackableEffect in stackableEffects)
        {
            switch (stackableEffect.effectName)
            {
                case "Weak":
                    dmgOutgoingPercent = -0.25f;
                    break;
                case "Fragile":
                    dmgIncomingPercent = 1.5f;
                    break;
                case "Power":
                    dmgOutgoingFlat = stackableEffect.amount;
                    break;
                default:
                    print("Defaulted");
                    break;
            }
        }
    }
}

