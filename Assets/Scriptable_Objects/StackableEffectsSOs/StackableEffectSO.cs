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
    public EffectDataSO effectData;
    public Sprite effectIcon;
    public RuntimeAnimatorController anim;

    [SerializeReference, SubclassSelector]
    public IStackableEffect tick;

    public int TickLogic(int amount, int tick) => (int)this.tick?.TickLogic(amount, tick);

    void Awake()
    {
        effectIcon = effectData.effectIcon;
    }
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