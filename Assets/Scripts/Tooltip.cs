using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    
    public Sprite tooltipImage;
    private GameObject tooltipObj;
    private bool tooltipDisplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tooltipObj = new GameObject("Tooltip");
        SpriteRenderer spriteRenderer = tooltipObj.AddComponent<SpriteRenderer>();
        tooltipObj.GetComponent<SpriteRenderer>().sprite = tooltipImage;
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
        tooltipObj.transform.position = gameObject.transform.position + new Vector3(0,5f,0);
    }

    public void OnMouseExit()
    {
        if (tooltipDisplay)
        {
            tooltipDisplay = false;
            tooltipObj.SetActive(false);
        }
    }


}
