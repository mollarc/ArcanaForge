using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectSO", menuName = "Scriptable Objects/StatusEffectSO")]
public class StackableEffectSO : ScriptableObject
{
    public bool permanent;
    public int amount;
    public int tickAmount;
    public int type; //Type of effect. 0: DOT effect 1:Passive Buffs/Debuffs 2:Buffs/Debuffs that Add buffs/debuffs per turn
    public string effectName;
    public Sprite effectIcon;
    public string effectTooltip;
    public string effectColor;

    [SerializeReference, SubclassSelector]
    public IStackableEffect _status;

    public int TickLogic(int amount, int tick) => (int)_status?.TickLogic(amount, tick);

    public void TickEffect()
    {
        if(!permanent)
        {
            amount = TickLogic(amount, tickAmount);
        }
    }
}
public interface IStackableEffect
{
    int TickLogic(int _amount, int _tick);
}

[Serializable]
public sealed class DecreaseByTick : IStackableEffect
{
    public int TickLogic(int _amount, int _tick)
    {
        _amount -= _tick;
        return _amount;
    }
}
[Serializable]
public sealed class DecreaseByHalf : IStackableEffect
{
    public int TickLogic(int _amount, int _tick)
    {
        _amount /= 2;
        return _amount;
    }
}