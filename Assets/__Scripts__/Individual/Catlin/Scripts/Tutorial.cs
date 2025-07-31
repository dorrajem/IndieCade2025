using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI storyText;

    public float fadeDuration = 2f;
    public float quoteDelay = 1f;
    public float quoteFadeDuration = 2f;
    public float storyTextFadeDuration = 1f;
    public float delayBetweenStoryLines = 2f;

    public TextMeshProUGUI skipText;

    public List<string> storyLines;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartTutorial()
    {
        StartCoroutine(PlayTutorial());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(PlayTutorial());
            storyText.gameObject.SetActive(false);
            skipText.gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    IEnumerator PlayTutorial()
    {
        yield return new WaitForSeconds(delayBetweenStoryLines);
        for (int i = 0; i < storyLines.Count; i++)
        {
            // Fade out current text
            if (i > 0)
                yield return StartCoroutine(FadeText(storyText, 1, 0, storyTextFadeDuration / 2));

            // Update text
            storyText.text = storyLines[i];
            // Fade in text only
            yield return StartCoroutine(FadeText(storyText, 0, 1, storyTextFadeDuration));

            // Wait before next line
            yield return new WaitForSeconds(delayBetweenStoryLines);
        }

        yield return StartCoroutine(FadeText(storyText, 1, 0, storyTextFadeDuration));

        // Option A: Disable the text object
        storyText.gameObject.SetActive(false);

        // Option B (optional): Disable this script to stop future use
        this.enabled = false;


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
