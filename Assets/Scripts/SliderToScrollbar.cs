using UnityEngine;
using UnityEngine.UI;

public class SliderToScrollbar : MonoBehaviour
{
    public Slider slider;
    public ScrollRect scrollRect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScrollPosition()
    {
        scrollRect.horizontalNormalizedPosition = slider.value;
    }
}
