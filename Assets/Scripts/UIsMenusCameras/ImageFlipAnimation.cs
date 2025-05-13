using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlipAnimation : MonoBehaviour
{
    public List<Sprite> sprites;
    [SerializeField] Image image;

    [SerializeField] float fps = 10;

    public void Start()
    {
        Play();
    }

    public void Play()
    {
        Stop();
        StartCoroutine(AnimSequence());
    }

    public void Stop()
    {
        StopAllCoroutines();
        ShowFrame(0);
    }

    IEnumerator AnimSequence()
    {
        var delay = new WaitForSeconds(1 / fps);
        int index = 0;
        while (true)
        {
            if (index >= sprites.Count) index = 0;
            ShowFrame(index);
            index++;
            yield return delay;
        }
    }

    void ShowFrame(int index)
    {
        image.sprite = sprites[index];
    }
}