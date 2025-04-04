using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Canvas canvas;
    public GameObject tooltipObj;
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

    public void OnMouseOver()
    {
        if(tooltipDisplay)
        {
            return;
        }
        print("Displaying image");
        tooltipObj.SetActive(true);
        tooltipDisplay = true;
    }

    public void OnMouseExit()
    {
        if (tooltipDisplay)
        {
            tooltipDisplay = false;
            tooltipObj.SetActive(false);
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
