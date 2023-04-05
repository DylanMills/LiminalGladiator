using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenDisplayer : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] TextMeshProUGUI uiText;
    public float fadeDuration = 1f;
    public float delayBeforeShowText = 1f;

    private bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
        uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, 0f);
    }

    public void BeginEndScreen()
    {
        if (!isFading)
            StartCoroutine(FadeScreenAndShowText());
    }

    IEnumerator FadeScreenAndShowText()
    {
        isFading = true;

        //Fade the screen to black
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);

        //Wait for a delay before showing the UI text
        yield return new WaitForSeconds(delayBeforeShowText);

        //Fade the screen back to transparent
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, 1f);
    }
}
