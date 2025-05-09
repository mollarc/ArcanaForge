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
    public string moveInfo; // Format should be " X Damage" and X is replaced by value in calculation
    public Sprite moveImage;
    public List<StackableEffectSO> statusEffects;

}
