using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    public Animator playerAnimator;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();
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
        originalParent = transform.parent; //Save OG parent
        transform.SetParent(transform.root); //Above other Canvas'
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; //Semi-transparent during drag
        Slot originalSlot = originalParent.GetComponent<Slot>();
        print(originalSlot.currentItem.GetComponent<WandComponent>().componentType);
        playerAnimator.SetBool("isSelecting", true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //Enable Raycasts
        canvasGroup.alpha = 1f; //No longer transparent

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); //Slot where item dropped
        if(dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null )
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
            playerAnimator.SetBool("isSelecting", false);
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if(dropSlot != null)
        {
            if (dropSlot.slotType != originalSlot.currentItem.GetComponent<WandComponent>().componentType && dropSlot.slotType != "ANY")
            {
                transform.SetParent(originalParent);
                playerAnimator.SetBool("isSelecting", false);
            }
            else
            {
                if (dropSlot.currentItem != null)
                {
                    //Slot has an item - swap items
                    dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                    originalSlot.currentItem = dropSlot.currentItem;
                    dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    originalSlot.currentItem = null;
                }

                //Move Item Drop Slot
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = gameObject;
                if(dropSlot.slotType != "ANY")
                {
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
    }
}
