using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using WandComponentNS;
public class ComponentDB : MonoBehaviour
{
    public static ComponentDB Instance;

    public GameObject typeComponent;
    public GameObject modifierComponent;
    public List<WandComponentSO> components = new List<WandComponentSO>();

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(gameObject);
        }
    }
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
        if (component.componentType == componentType.Type)
        {
            var tempList1 = new List<WandComponentSO>();
            tempList1.Add(component);
            var templist2 = tempList1.Cast<TypeComponentSO>().ToArray();
            var newTypeComponent = Instantiate(typeComponent);
            newTypeComponent.GetComponent<TypeComponent>().LoadComponentData(templist2[0]);
            return newTypeComponent;
        }
        else if(component.componentType == componentType.Modifier)
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

    public List<GameObject> CreateAllComponents()
    {
        List<GameObject> list = new List<GameObject>();
        foreach(WandComponentSO component in components)
        {
            if (component.componentType == componentType.Type)
            {
                var tempList1 = new List<WandComponentSO>();
                tempList1.Add(component);
                var templist2 = tempList1.Cast<TypeComponentSO>().ToArray();
                var newTypeComponent = Instantiate(typeComponent);
                newTypeComponent.GetComponent<TypeComponent>().LoadComponentData(templist2[0]);
                list.Add(newTypeComponent);
            }
            else if (component.componentType == componentType.Modifier)
            {
                var tempList1 = new List<WandComponentSO>();
                tempList1.Add(component);
                var templist2 = tempList1.Cast<ModifierComponentSO>().ToArray();
                var newModComponent = Instantiate(modifierComponent);
                newModComponent.GetComponent<ModifierComponent>().LoadComponentData(templist2[0]);
                list.Add(newModComponent);
            }
        }
        return list;
    }
}
