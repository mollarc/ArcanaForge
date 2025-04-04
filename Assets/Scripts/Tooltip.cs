using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipObj;
    public TMP_Text tooltipText;
    private bool tooltipDisplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tooltipDisplay = false;
        tooltipObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceTooltipText(string text)
    {
        if (tooltipText != null)
        {
            tooltipText.text = text;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipDisplay)
        {
            return;
        }
        print("Displaying image");
        tooltipObj.SetActive(true);
        tooltipDisplay = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipDisplay)
        {
            tooltipDisplay = false;
            tooltipObj.SetActive(false);
        }
    }
}
