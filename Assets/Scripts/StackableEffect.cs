using NUnit.Framework;
using UnityEngine;

public abstract class StackableEffect : MonoBehaviour
{
    public bool permanent;
    public int amount;
    public int tickAmount;
    public string effectName;
    public StackableEffectSO effectData;

}
