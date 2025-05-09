using UnityEngine;
using UnityEngine.UI;

public class PortalEnlarge : MonoBehaviour
{
    RectTransform rect;
    public float increment = 0.004f;
    public float targetScale = 0f;
    float value = 0f;
    public GameObject portalOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portalOverlay.GetComponent<Image>().enabled = false;
        rect = GetComponent<RectTransform>();
        gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale != new Vector3(targetScale, targetScale, 1))
        {
            gameObject.transform.localScale = new Vector3(value,value,1);
        }
        else
        {
            portalOverlay.GetComponent<Image>().enabled = true;
        }
        value += increment;
    }
}
