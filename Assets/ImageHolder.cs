using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder : MonoBehaviour
{
    public List<Sprite> images;
    public Image currentImage;
    int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentImage.sprite = images[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CycleLeft()
    {
        if (index == 0)
        {
            index = 0;
        }
        else
        {
            index--;
        }
        currentImage.sprite = images[index];
    }

    public void CycleRight()
    {
        if (index == images.Count - 1)
        {
            index = images.Count-1;
        }
        else
        {
            index++;
        }
        currentImage.sprite = images[index];
    }

    public void DisplayImage()
    {
        gameObject.SetActive(true);
    }

    public void UnDisplay()
    {
        gameObject.SetActive(false);
    }
}
