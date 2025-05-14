using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text manaValue;
    public TMP_Text cooldownValue;
    public TMP_Text usesValue;
    public Image itemImage;
    public Sprite emptyImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTooltipData(Tooltip tooltip)
    {
        itemName.text = tooltip.tooltipName;
        itemDescription.text = tooltip.tooltipDescription;
        manaValue.text = tooltip.tooltipMana;
        cooldownValue.text = tooltip.tooltipCooldown;
        usesValue.text = tooltip.tooltipUses;
        itemImage.sprite = tooltip.tooltipSprite;
    }

    public void ClearInspector()
    {
        itemName.text = "";
        itemDescription.text = "";
        manaValue.text = "";
        cooldownValue.text = "";
        usesValue.text = "";
        itemImage.sprite = emptyImage;
    }
}
