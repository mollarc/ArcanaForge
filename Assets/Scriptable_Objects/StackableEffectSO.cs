using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectSO", menuName = "Scriptable Objects/StatusEffectSO")]
public class StackableEffectSO : ScriptableObject
{
    public bool permanent;
    public int amount;
    public int tickAmount;
    public string effectName;
}
