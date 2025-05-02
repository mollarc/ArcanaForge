using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WandComponentNS;
public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject wandPanel;
    public int slotCount;
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public Slot[] slotArray;
    public ComponentDB componentDB;
    public Inventory inventoryScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotArray = new Slot[slotCount];
        inventoryScript = gameObject.GetComponent<Inventory>();
        CreateInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateInventory()
    {
        //GameObject inventory = Instantiate(inventoryPanel,GameObject.FindWithTag("Canvas").transform);
        //componentDB = gameObject.GetComponent<ComponentDB>();
        //itemPrefabs = componentDB.CreateAllComponents(); 
        itemPrefabs = inventoryScript.CreateAllComponents();
        for (int i = 0; i < itemPrefabs.Count || i <= 4; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.GetComponentInChildren<GridLayoutGroup>().gameObject.transform).GetComponent<Slot>();
            slotArray[i] = slot;
            slot.slotIndex = i;
            if (i < itemPrefabs.Count)
            {
                Vector3 originalScale = itemPrefabs[i].transform.localScale;
                itemPrefabs[i].transform.SetParent(slot.transform);
                itemPrefabs[i].transform.localScale = originalScale;                
                itemPrefabs[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                inventoryScript.displayedGems.Add(itemPrefabs[i]);
                slot.currentItem = itemPrefabs[i];
            }
        }
    }
    public void SortForAllGems()
    {
        foreach (Slot slot in slotArray)
        {
            if (slot != null)
            {
                slot.currentItem = null;
            }
        }
        for (int i = inventoryScript.displayedGems.Count - 1; i >= 0; i--)
        {
            inventoryScript.RemoveDisplayedGem(i);
        }
        for (int i = inventoryScript.undisplayedGems.Count - 1; i >= 0; i--)
        {
            foreach (Slot slot in slotArray)
            {
                if (slot != null && slot.currentItem == null)
                {
                    inventoryScript.undisplayedGems[i].transform.SetParent(slot.transform);
                    inventoryScript.undisplayedGems[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    inventoryScript.displayedGems.Add(inventoryScript.undisplayedGems[i]);
                    slot.currentItem = inventoryScript.undisplayedGems[i];
                    inventoryScript.undisplayedGems.RemoveAt(i);
                    break;
                }
            }
        }
    }
    public void SortForTypeGems()
    {
        //Removes gems from inventory that arent type gems
        foreach (Slot slot in slotArray)
        {
            if (slot != null && slot.currentItem != null)
            {
                if (slot.currentItem.GetComponent<WandComponent>().componentType != componentType.Type)
                {
                    for (int i = inventoryScript.displayedGems.Count - 1; i >= 0; i--)
                    {
                        if (inventoryScript.displayedGems[i] == slot.currentItem)
                        {
                            inventoryScript.RemoveDisplayedGem(i);
                            break;
                        }
                    }
                    slot.currentItem = null;
                }
            }
        }
        //Adds gems moved out of scene back into scene
        for (int i = inventoryScript.undisplayedGems.Count - 1; i >= 0; i--)//Cycles through gems moved out of scene
        {
            foreach (Slot slot in slotArray)//Cycles Through slots in inventory
            {
                if (slot != null && slot.currentItem == null)//Checks if slot exists and is empty
                {
                    if (inventoryScript.undisplayedGems[i].GetComponent<WandComponent>().componentType == componentType.Type)//Checks if the gem should be displayed
                    {
                        inventoryScript.undisplayedGems[i].transform.SetParent(slot.transform);
                        inventoryScript.undisplayedGems[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        slot.currentItem = inventoryScript.undisplayedGems[i];
                        inventoryScript.RemoveUndisplayedGem(i, slot.slotIndex);
                        break;
                    }
                }
            }
        }
    }

    public void SortForModifierGems()
    {
        foreach (Slot slot in slotArray)
        {
            if (slot != null && slot.currentItem != null)
            {
                if (slot.currentItem.GetComponent<WandComponent>().componentType != componentType.Modifier)
                {
                    for (int i = inventoryScript.displayedGems.Count - 1; i >= 0; i--)
                    {
                        if (inventoryScript.displayedGems[i] == slot.currentItem)
                        {
                            inventoryScript.RemoveDisplayedGem(i);
                            break;
                        }
                    }
                    slot.currentItem = null;
                }
            }
        }
        //Adds gems moved out of scene back into scene
        for (int i = inventoryScript.undisplayedGems.Count - 1; i >= 0; i--)//Cycles through gems moved out of scene
        {
            foreach (Slot slot in slotArray)//Cycles Through slots in inventory
            {
                if (slot != null && slot.currentItem == null)//Checks if slot exists and is empty
                {
                    if (inventoryScript.undisplayedGems[i].GetComponent<WandComponent>().componentType == componentType.Modifier)//Checks if the gem should be displayed
                    {
                        inventoryScript.undisplayedGems[i].transform.SetParent(slot.transform);
                        inventoryScript.undisplayedGems[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        slot.currentItem = inventoryScript.undisplayedGems[i];
                        inventoryScript.RemoveUndisplayedGem(i, slot.slotIndex);
                        break;
                    }
                }
            }
        }
    }

    public void AddItem(GameObject item)
    {
        itemPrefabs.Add(item);
    }

    public void EndTurn()
    {
        foreach (Slot slot in slotArray)
        {
            if (slot != null && slot.currentItem != null)
            {
                slot.currentItem.gameObject.GetComponent<WandComponent>().EndTurn();
            }
        }
    }
}
