using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipDescription;
    public string tooltipName;
    public string tooltipMana = "";
    public string tooltipCooldown = "";
    public string tooltipUses = "";
    public Sprite tooltipSprite;
    private bool tooltipDisplay;

    public ItemInspector itemInspector;
    void Awake()
    {
        itemInspector = GameObject.FindGameObjectWithTag("Inspector").GetComponent<ItemInspector>();
        tooltipDisplay = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipDisplay)
        {
            return;
        }
        tooltipDisplay = true;
        itemInspector.LoadTooltipData(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipDisplay)
        {
            tooltipDisplay = false;
            itemInspector.ClearInspector();
        }
    }
}
