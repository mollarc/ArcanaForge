using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duration of the fade-in effect
    private Image fadeImage;
    private Color fadeColor;

    void Start()
    {
        fadeImage = GetComponentInChildren<Image>();
        fadeColor = fadeImage.color;
        fadeColor.a = 1.0f; // Start fully opaque
        fadeImage.color = fadeColor;
        StartCoroutine(FadeInEffect());
    }

    public IEnumerator FadeInEffect()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0.0f; // Ensure it's fully transparent at the end
        fadeImage.color = fadeColor;
    }

    public IEnumerator FadeOutEffect()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(0f + (elapsedTime / fadeDuration));
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1.0f; // Ensure it's fully visible at the end
        fadeImage.color = fadeColor;
    }
}
