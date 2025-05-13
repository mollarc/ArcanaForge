
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class FIX_Scrollbar : MonoBehaviour
{
    public Slider slider;
    public Scrollbar scrollbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSliderPosition()
    {
         slider.value = scrollbar.value;
    }
}