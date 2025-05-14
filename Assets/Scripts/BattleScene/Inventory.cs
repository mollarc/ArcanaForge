using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using System.Linq;
using WandComponentNS;
public class Inventory : MonoBehaviour
{
    public WandObject Wand1;
    public GameObject typeComponent;
    public GameObject modifierComponent;
    public List<WandComponentSO> components = new List<WandComponentSO>();
    public List<GameObject> displayedGems = new List<GameObject>();
    public List<GameObject> undisplayedGems = new List<GameObject>();
    public List<GameObject> wandGems = new List<GameObject>();
    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        components.AddRange(GlobalController.Instance.gemInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateComponent()
    {
        int randomIndex = Random.Range(0, components.Count);
        WandComponentSO component = components[randomIndex];
        if (component.componentType == componentType.Type)
        {
            var tempList1 = new List<WandComponentSO>
            {
                component
            };
            var templist2 = tempList1.Cast<TypeComponentSO>().ToArray();
            var newTypeComponent = Instantiate(typeComponent);
            newTypeComponent.GetComponent<TypeComponent>().LoadComponentData(templist2[0]);
            newTypeComponent.GetComponent<TypeComponent>().UpdateTooltip();
            return newTypeComponent;
        }
        else if (component.componentType == componentType.Modifier)
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
        foreach (WandComponentSO component in components)
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

    public void RemoveDisplayedGem(int index)
    {
        displayedGems[index].transform.SetParent(gameObject.transform);
        undisplayedGems.Add(displayedGems[index]);
        displayedGems.RemoveAt(index);
    }

    public void RemoveUndisplayedGem(int index, int _slotIndex)
    {
        displayedGems.Insert(_slotIndex, undisplayedGems[index]);
        undisplayedGems.RemoveAt(index);
    }

    public void AddGemToWand(GameObject gem)
    {
        for(int i = displayedGems.Count - 1; i >= 0; i--)
        {
            if (displayedGems[i] == gem)
            {
                wandGems.Add(gem);
                displayedGems.RemoveAt(i);
            }
        }
    }

    public void RemoveGemFromWand(GameObject gem, int index)
    {
        for (int i = wandGems.Count - 1; i >= 0; i--)
        {
            if (wandGems[i] == gem)
            {
                if(displayedGems.Count <= index)
                {
                    displayedGems.Add(gem);
                }
                else
                {
                    displayedGems.Insert(index, gem);
                }
                wandGems.RemoveAt(i);
            }
        }
    }
}
