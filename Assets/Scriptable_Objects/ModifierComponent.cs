using UnityEngine;

[CreateAssetMenu(fileName = "ModifierComponent", menuName = "Scriptable Objects/ModifierComponent")]
public class ModifierComponent : ScriptableObject
{
    public string componentName;
    //Debuff Type
    //Buff Type
    //Debuff Duration
    //Buff Duration
    //Write percentages as decimals
    public float modifierValue;
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
}
