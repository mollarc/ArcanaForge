using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectSO", menuName = "Scriptable Objects/StatusEffectSO")]
public class StackableEffectSO : ScriptableObject
{
    public bool permanent;
    public bool turnStartTick;
    public bool scalesWithAmount;
    public int amount;
    public int tickAmount;
    public int type; //Type of effect. 0 Apply to Enemy, 1 Apply to Self
    public string effectName;
    public Sprite effectIcon;
    public string effectTooltip;
    public string effectColor;
    public RuntimeAnimatorController anim;

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