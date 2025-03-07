using NUnit.Framework;
using System;
using UnityEngine;

public class StackableEffect : MonoBehaviour
{
    public bool permanent;
    public int amount;
    public int tickAmount;
    public string effectName;

    public Sprite effectIcon;

    public StackableEffect(StackableEffectSO _effectData)
    {
        permanent = _effectData.permanent;
        amount = _effectData.amount;
        tickAmount = _effectData.tickAmount;
        effectName = _effectData.effectName;
    }

    [SerializeReference, SubclassSelector]
    public IStackableEffect _status;

    public void ActivateSkill() => _status?.ActivateEffect();
}

public interface IStackableEffect
{
    void ActivateEffect();
    int GetAmount();
    string GetEffectName();
    void AddAmount(int add);
}

[Serializable]
public sealed class DecreaseByTick : IStackableEffect
{
    [HideInInspector]
    public string effectName;

    public int amount;
    public int tick;

    public void ActivateEffect()
    {
        amount -= tick;
    }

    public void AddAmount(int add)
    {
        amount += add;
    }

    public int GetAmount()
    {
        return amount;
    }

    public string GetEffectName()
    {
        return effectName;
    }
}
[Serializable]
public sealed class DecreaseByHalf : IStackableEffect
{
    [HideInInspector]
    public string effectName;

    public int amount;


    public void ActivateEffect()
    {
        amount /= 2;
    }

    public void AddAmount(int add)
    {
        amount += add;
    }

    public int GetAmount()
    {
        return amount;
    }

    public string GetEffectName()
    {
        return effectName;
    }
}

