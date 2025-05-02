using UnityEngine;

public class PortalEnlarge : MonoBehaviour
{
    RectTransform rect;
    public float increment = 0.004f;
    float value = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localScale != Vector3.one)
        {
            gameObject.transform.localScale = new Vector3(value,value,1);
        }
        value += increment;
    }
}
