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
    public int uses;
    public int currentUses;
    public bool wasUsed;
    public bool wasDisabled;
    public Sprite gemImage;
    public Image image;
    public CanvasGroup canvasGroup;
    public InventoryController inventoryController;
    public ImageFlipAnimation flipAnimation;
    public string moveInfo;
    public GemTooltip tooltip;
    public TMP_Text cooldownText;

    void Awake()
    {
        SetUP();
    }
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
        uses = componentData.uses;
        currentUses = uses;
        gemImage = componentData.gemImage;
        flipAnimation.sprites.AddRange(componentData.sprites);
        moveInfo = componentData.moveInfo;
    }

    public void LoadComponentData(ModifierComponentSO componentData)
    {
        componentName = componentData.componentName;
        componentType = componentData.componentType;
        manaCost = componentData.manaCost;
        cooldown = componentData.cooldown;
        uses = componentData.uses;
        currentUses = uses;
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
        wasDisabled = true;
        inventoryController.AddDisabledGems();
    }

    public void AdjustCooldown(int _adjustment)
    {
        Color color = image.color;
        currentCooldown += _adjustment;
        if (currentCooldown <= 0)
        {
            print("cooldown0");
            if (wasDisabled)
            {
                print("wasDisabled");
                color.r *= 2f;
                color.g *= 2f;
                color.b *= 2f;
                image.color = color;
                wasDisabled = false;
            }
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

    public void AdjustUses(int _adjustment)
    {
        if (uses < 0)
        {
            return;
        }
        Color color = image.color;
        currentUses += _adjustment;
        if (currentUses == 0)
        {
            canvasGroup.alpha = 0.8f;
            color.r *= .5f;
            color.g *= .5f;
            color.b *= .5f;
            image.color = color;
            MoveComponent();
        }
        else if (currentUses > 0)
        {
            if (wasDisabled)
            {
                color.r *= 2f;
                color.g *= 2f;
                color.b *= 2f;
                image.color = color;
                wasDisabled = false;
            }
        }
        UpdateTooltip();
    }

    public bool EndTurn()
    {
        Color color = image.color;
        if (wasUsed)
        {
            wasUsed = false;
            currentCooldown += cooldown;
            cooldownText.text = currentCooldown.ToString();
            currentUses--;
            UpdateTooltip();
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
                cooldownText.text = "";
                if (currentUses !=0)
                {
                    canvasGroup.alpha = 1f;
                    color.r *= 2f;
                    color.g *= 2f;
                    color.b *= 2f;
                    image.color = color;
                }
            }
        }
        else if (!wasUsed)
        {
            return false;
        }
        wasUsed = false;
        return true;
    }

    public virtual void UpdateTooltip()
    {
        tooltip.tooltipName = componentName;
        tooltip.tooltipDescription = moveInfo;
        tooltip.tooltipMana = manaCost.ToString();
        tooltip.tooltipCooldown = cooldown.ToString();
        if (currentUses > 0)
        {
            tooltip.tooltipUses = currentUses.ToString();
        }
        else if(currentUses == 0)
        {
            tooltip.tooltipUses = "<color=#FF0005>" + currentUses.ToString() + "</color>";
        }
        tooltip.tooltipSprite = gemImage;
    }
}