using UnityEngine;

[CreateAssetMenu(fileName = "WandComponent", menuName = "Scriptable Objects/WandComponent")]
public class TypeComponent : ScriptableObject
{
    public string componentName;
    public int manaCost;
    public int cooldown;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
}
