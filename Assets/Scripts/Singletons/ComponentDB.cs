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
    //List of starting components for each character as  a Scriptable Object
    public List<WandComponentSO> inventoryComponents = new List<WandComponentSO>();
    public List<WandComponentSO> nonInventoryComponents;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nonInventoryComponents = components;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(List<WandComponentSO> _inventoryComponents)
    {
        inventoryComponents.AddRange(_inventoryComponents);
    }
}
