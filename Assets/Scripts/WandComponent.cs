using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WandComponentNS;

public class WandComponent : MonoBehaviour
{
    public string componentName;
    public componentType componentType;
    public int manaCost;
    public int cooldown;
    public int currentCooldown;
    public bool wasUsed;
    public Sprite gemImage;
    public Image image;
    public CanvasGroup canvasGroup;
    public InventoryController inventoryController;
    public ImageFlipAnimation flipAnimation;
    public string moveInfo;
    public Tooltip tooltip;
    public TMP_Text cooldownText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUP()
    {
        currentCooldown = 0;
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        inventoryController = FindAnyObjectByType<InventoryController>();
    }

    public void LoadComponentData(TypeComponentSO componentData)
    {
        componentName = componentData.componentName;
        componentType = componentData.componentType;
        manaCost = componentData.manaCost;
        cooldown = componentData.cooldown;
        gemImage = componentData.gemImage;
        flipAnimation.sprites = componentData.sprites;
        moveInfo = componentData.moveInfo;
    }

    public void LoadComponentData(ModifierComponentSO componentData)
    {
        componentName = componentData.componentName;
        componentType = componentData.componentType;
        manaCost = componentData.manaCost;
        cooldown = componentData.cooldown;
        gemImage = componentData.gemImage;
        flipAnimation.sprites.AddRange(componentData.sprites);
        moveInfo = componentData.moveInfo;
    }

    public void MoveComponent()
    {
        Slot dropSlot;
        Slot originalSlot = transform.parent.GetComponent<Slot>();
        for (int i = 0; i < inventoryController.slotArray.Length; i++)
        {
            dropSlot = inventoryController.slotArray[i];
            if (dropSlot.currentItem == null)
            {
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = gameObject;
                originalSlot.currentItem = null;
                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                break;
            }
        }
        inventoryController.AddDisabledGems();
    }

    public void AdjustCooldown(int _adjustment)
    {
        Color color = image.color;
        currentCooldown += _adjustment;
        if (currentCooldown < 0)
        {
            color.r *= 2f;
            color.g *= 2f;
            color.b *= 2f;
            image.color = color;
            currentCooldown = 0;
            cooldownText.text = "";
        }
        else if (currentCooldown > 0)
        {
            canvasGroup.alpha = 0.8f;
            color.r *= .5f;
            color.g *= .5f;
            color.b *= .5f;
            image.color = color;
            cooldownText.text = currentCooldown.ToString();
            MoveComponent();
        }
    }

    public bool EndTurn()
    {
        Color color = image.color;
        if (wasUsed)
        {
            wasUsed = false;
            currentCooldown += cooldown;
            cooldownText.text = currentCooldown.ToString();
            if (currentCooldown == 0)
            {
                cooldownText.text = "";
                return false;
            }
            canvasGroup.alpha = 0.8f;
            color.r *= .5f;
            color.g *= .5f;
            color.b *= .5f;
            image.color = color;
        }
        else if (currentCooldown > 0)
        {
            wasUsed = false;
            currentCooldown -= 1;
            cooldownText.text = currentCooldown.ToString();
            if (currentCooldown == 0)
            {
                canvasGroup.alpha = 1f;
                color.r *= 2f;
                color.g *= 2f;
                color.b *= 2f;
                image.color = color;
                cooldownText.text = "";
            }
        }
        else if(!wasUsed)
        {
            return false;
        }
        wasUsed = false;
        return true;
    }

    public void UpdateTooltip()
    {
        tooltip.tooltipName = componentName;
        tooltip.tooltipDescription = moveInfo;
        tooltip.tooltipMana = manaCost.ToString();
        tooltip.tooltipCooldown = cooldown.ToString();
        tooltip.tooltipSprite = gemImage;
    }
}