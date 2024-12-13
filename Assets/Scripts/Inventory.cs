using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public WandObject Wand1;
    public List<WandComponents> WandComponents = new List<WandComponents>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var wandComponent in WandComponents)
        {
            if (wandComponent.componentType == "TYPE")
            {
                Wand1.typeDropdown1.options.Add(new TMP_Dropdown.OptionData(wandComponent.componentName));
                Wand1.typeDropdown2.options.Add(new TMP_Dropdown.OptionData(wandComponent.componentName));
                Debug.Log("ComponentAdded");
            }
            else if (wandComponent.componentType == "MODIFIER")
            {
                Wand1.modifierDropdown3.options.Add(new TMP_Dropdown.OptionData(wandComponent.componentName));
                Debug.Log("ComponentAdded");
            }
        }
        Wand1.typeDropdown1.RefreshShownValue();
        Wand1.typeDropdown2.RefreshShownValue();
        Wand1.modifierDropdown3.RefreshShownValue();
        Wand1.SetUP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
