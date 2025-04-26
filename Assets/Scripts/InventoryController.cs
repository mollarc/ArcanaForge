using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateInventory()
    {
        GameObject inventory = Instantiate(inventoryPanel,GameObject.FindWithTag("Canvas").transform);
        slotArray = new Slot[slotCount];
        componentDB = gameObject.GetComponent<ComponentDB>();
        itemPrefabs = componentDB.CreateAllComponents();
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventory.GetComponentInChildren<GridLayoutGroup>().gameObject.transform).GetComponent<Slot>();
            slotArray[i] = slot;
            if (i < itemPrefabs.Count)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);

                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

    public void AddItem(GameObject item)
    {
        itemPrefabs.Add(item);
    }

    public void EndTurn()
    {
        foreach(Slot slot in slotArray)
        {
            if (slot.currentItem != null)
            {
                slot.currentItem.gameObject.GetComponent<WandComponent>().EndTurn();
            }
        }
    }
}
