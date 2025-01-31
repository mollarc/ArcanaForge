using UnityEngine;

public class WandComponents : MonoBehaviour
{
    public string componentName;
    public string componentType;
    public int manaCost;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    //Write percentages as decimals
    public float modifierValue;
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Random,Blast,All" in order starting from 0. Highest value takes priority.
    public WandComponents(string _componentName, int _healValue, int _damageValue, int _shieldValue, float _modifierValue, int _numberOfCasts, int _targets)
    {
        componentName = _componentName;
        healValue = _healValue;
        damageValue = _damageValue;
        shieldValue = _shieldValue;
        modifierValue = _modifierValue;
        numberOfCasts = _numberOfCasts;
        targets = _targets;
    }
}
