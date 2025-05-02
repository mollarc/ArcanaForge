using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using WandComponentNS;
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    public Animator playerAnimator;
    public GameObject player;
    public WandComponent wandComponent;
    public UnityEvent componentMoved;
    public Inventory inventory;
    public InventoryController inventoryController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();
        inventory = GameObject.FindWithTag("GameController").GetComponent<Inventory>();
        inventoryController = GameObject.FindWithTag("GameController").GetComponent<InventoryController>();
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Wand"))
        {
            componentMoved.AddListener(gameObject.GetComponent<WandObject>().CalculateValues);
        }
    }

    public bool CheckCooldown()
    {
        if (wandComponent.currentCooldown > 0)
        {
            return true;
        }
        return false;
    }

    public void ComponentChange()
    {
        playerAnimator.SetBool("Swapped", true);
        playerAnimator.SetBool("isSelecting", false);
        StartCoroutine(ChangeSwapped());

    }

    public IEnumerator ChangeSwapped()
    {
        yield return new WaitForSeconds(0.25f);
        playerAnimator.SetBool("Swapped", false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckCooldown())
        {
            eventData.pointerDrag = null;
            return;
        }
        originalParent = transform.parent; //Save OG parent
        transform.SetParent(transform.root); //Above other Canvas'
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; //Semi-transparent during drag
        Slot originalSlot = originalParent.GetComponent<Slot>();
        playerAnimator.SetBool("isSelecting", true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CheckCooldown())
        {
            return;
        }
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //Enable Raycasts
        canvasGroup.alpha = 1f; //No longer transparent
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); //Slot where item dropped
        Slot originalSlot = originalParent.GetComponent<Slot>();
        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
            playerAnimator.SetBool("isSelecting", false);
            if (originalSlot.slotType != componentType.Any)
            {
                foreach (Slot slot in inventoryController.slotArray)
                {
                    if (slot != null && slot.currentItem == null)
                    {
                        inventory.RemoveGemFromWand(gameObject, slot.slotIndex);
                        transform.SetParent(slot.transform);
                        slot.currentItem = gameObject;
                        slot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        originalSlot.currentItem = null;
                        return;
                    }
                }
            }
        }

        if (dropSlot != null)
        {
            if (dropSlot.slotType != originalSlot.currentItem.GetComponent<WandComponent>().componentType && dropSlot.slotType != componentType.Any)
            {
                transform.SetParent(originalParent);
                playerAnimator.SetBool("isSelecting", false);
            }
            else
            {
                if (dropSlot.currentItem != null)
                {
                    if (originalSlot.slotType == dropSlot.currentItem.GetComponent<WandComponent>().componentType || originalSlot.slotType == componentType.Any)
                    {
                        //Slot has an item - swap items
                        dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                        originalSlot.currentItem = dropSlot.currentItem;
                        dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        if (dropSlot.slotType != componentType.Any || originalSlot.slotType != componentType.Any)
                        {
                            playerAnimator.SetBool("Swapped", true);
                            playerAnimator.SetBool("isSelecting", false);
                            inventory.AddGemToWand(originalSlot.currentItem);
                            StartCoroutine(ChangeSwapped());
                        }
                    }
                    else
                    {
                        transform.SetParent(originalParent);
                        playerAnimator.SetBool("isSelecting", false);
                        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //Centers Item
                        componentMoved.Invoke();
                        return;
                    }
                }
                else
                {
                    originalSlot.currentItem = null;
                }

                //Move Item Drop Slot
                if (originalSlot.slotType != componentType.Any)
                {
                    inventory.RemoveGemFromWand(gameObject, dropSlot.slotIndex);
                }
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = gameObject;
                if (dropSlot.slotType != componentType.Any)
                {
                    inventory.AddGemToWand(gameObject);
                    ComponentChange();
                }
                else
                {
                    playerAnimator.SetBool("isSelecting", false);
                }
            }
        }
        else
        {
            //No slot under drop point
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //Centers Item
        componentMoved.Invoke();
    }
}
