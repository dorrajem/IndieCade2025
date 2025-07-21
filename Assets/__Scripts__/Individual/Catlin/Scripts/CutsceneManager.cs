using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Image fadeImage;
    public TextMeshProUGUI quoteText;
    public TextMeshProUGUI storyText;
    public Image storyImage;

    [Header("Cutscene Timing")]
    public float fadeDuration = 2f;
    public float quoteDelay = 1f;
    public float quoteFadeDuration = 2f;
    public float storyTextFadeDuration = 1f;
    public float delayBetweenStoryLines = 2f;

    [Header("Story Content")]
    [TextArea(3, 10)]
    public List<string> storyLines;
    public List<Sprite> storyImages;

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        //Fade to black
        yield return StartCoroutine(FadeImage(0, 1, fadeDuration));

        //Show quote
        yield return new WaitForSeconds(quoteDelay);
        yield return StartCoroutine(FadeText(quoteText, 0, 1, quoteFadeDuration));

        //Fade to black
        yield return new WaitForSeconds(quoteDelay);
        yield return StartCoroutine(FadeText(quoteText, 1, 0, quoteFadeDuration / 2));

        for (int i = 0; i < storyLines.Count; i++)
        {
            // Fade out current text
            if (i > 0)
                yield return StartCoroutine(FadeText(storyText, 1, 0, storyTextFadeDuration / 2));

            // Update text
            storyText.text = storyLines[i];

            // Set new image sprite (instant)
            if (i < storyImages.Count && storyImages[i] != null)
            {
                storyImage.sprite = storyImages[i];

                // Force image alpha to 1 (fully visible)
                Color imgColor = storyImage.color;
                imgColor.a = 1f;
                storyImage.color = imgColor;
            }

            // Fade in text only
            yield return StartCoroutine(FadeText(storyText, 0, 1, storyTextFadeDuration));

            // Wait before next line
            yield return new WaitForSeconds(delayBetweenStoryLines);
        }

        //Fade to black
        yield return StartCoroutine(FadeImage(1, 1, 0));
        GameSceneManager gsm = GameObject.FindFirstObjectByType<GameSceneManager>();
        if (gsm != null)
        {
            gsm.Battle();
        }

    }

    IEnumerator FadeImage(float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            fadeImage.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = toAlpha;
        fadeImage.color = color;
    }

    IEnumerator FadeImageUI(Image img, float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = img.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            img.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = toAlpha;
        img.color = color;
    }

    IEnumerator FadeText(TextMeshProUGUI textElement, float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = textElement.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            textElement.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = toAlpha;
        textElement.color = color;
    }
}