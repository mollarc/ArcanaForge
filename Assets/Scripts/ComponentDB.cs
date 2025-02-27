using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentDB : MonoBehaviour
{
    public GameObject typeComponent;
    public GameObject modifierComponent;
    public List<WandComponentSO> components = new List<WandComponentSO>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateComponent()
    {
        int randomIndex = Random.Range(0,components.Count);
        WandComponentSO component = components[randomIndex];
        if (component.componentType == "TYPE")
        {
            var tempList1 = new List<WandComponentSO>();
            tempList1.Add(component);
            var templist2 = tempList1.Cast<TypeComponentSO>().ToArray();
            var newTypeComponent = Instantiate(typeComponent);
            newTypeComponent.GetComponent<TypeComponent>().LoadComponentData(templist2[0]);
            return newTypeComponent;
        }
        else if(component.componentType == "MODIFIER")
        {
            var tempList1 = new List<WandComponentSO>();
            tempList1.Add(component);
            var templist2 = tempList1.Cast<ModifierComponentSO>().ToArray();
            var newModComponent = Instantiate(modifierComponent);
            newModComponent.GetComponent<ModifierComponent>().LoadComponentData(templist2[0]);
            return newModComponent;
        }
        return null;
    }
}
