using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TempEnemyMove", menuName = "Scriptable Objects/TempEnemyMove")]

public class TempEnemyMove : ScriptableObject
{
    public string moveName;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public int type; //0 = Attack 1 = Shield 2 = Heal
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<StackableEffectSO> statusEffects;

}
