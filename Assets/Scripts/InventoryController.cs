using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject wandPanel;
    public int slotCount;
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public Slot[] slotArray;
    public ComponentDB componentDB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotArray = new Slot[slotCount];
        componentDB = gameObject.GetComponent<ComponentDB>();
        itemPrefabs = componentDB.CreateAllComponents();
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            slotArray[i] = slot;
            if (i < itemPrefabs.Count)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item)
    {
        itemPrefabs.Add(item);
    }
}
