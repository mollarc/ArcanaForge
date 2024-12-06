using UnityEngine;

public class WandComponents : MonoBehaviour
{
    public string componentName;
    public int manaCost;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    //Write percentages as decimals
    public float modifierValue;
    public int numberOfCasts;
    public string targets; //Strings should only be "Single,Random,Blast,All"
    public WandComponents(string _componentName, int _healValue, int _damageValue, int _shieldValue, float _modifierValue, int _numberOfCasts, string _targets)
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
