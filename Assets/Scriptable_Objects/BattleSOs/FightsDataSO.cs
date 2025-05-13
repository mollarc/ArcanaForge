using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FightsDataSO", menuName = "Scriptable Objects/FightsDataSO")]
public class FightsDataSO : ScriptableObject
{
    public List<GameObject> enemyDataList;
}
