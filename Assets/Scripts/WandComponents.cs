using UnityEngine;

public class WandComponents : MonoBehaviour
{
    public string componentName;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    //Write percentages as decimals
    public float modifierValue;
    public int targets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public WandComponents(string _componentName, int _healValue, int _damageValue, int _shieldValue, float _modifierValue, int _targets)
    {
        componentName = _componentName;
        healValue = _healValue;
        damageValue = _damageValue;
        shieldValue = _shieldValue;
        modifierValue = _modifierValue;
        targets = _targets;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
