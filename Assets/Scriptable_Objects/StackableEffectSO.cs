using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectSO", menuName = "Scriptable Objects/StatusEffectSO")]
public class StackableEffectSO : ScriptableObject
{
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
public sealed class Poison : IStackableEffect
{
    [HideInInspector]
    public string _name = "Poison";

    public int amount;


    public void ActivateEffect()
    {
        amount -= 1;
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
        return _name;
    }
}
[Serializable]
public sealed class Burn : IStackableEffect
{
    [HideInInspector]
    public string _name = "Burn";

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
        return _name;
    }
}
[Serializable]
public sealed class Frigid : IStackableEffect
{
    [HideInInspector]
    public string _name = "Frigid";

    public int amount;


    public void ActivateEffect()
    {
        amount -= 1;
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
        return _name;
    }
}
