using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardSO", menuName = "Scriptable Objects/RewardSO")]
public class RewardSO : ScriptableObject
{
    public string rewardName;
    public List<WandComponentSO> rewards;
}
