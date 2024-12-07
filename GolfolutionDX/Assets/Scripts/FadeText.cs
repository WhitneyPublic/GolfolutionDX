using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour
{
    public float fadeDuration = 3f; // Time it takes to fade out (in seconds)
    public float displayDuration = 10f; // Time to display text before fading
    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Get the CanvasGroup component
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing. Please add it to the text object.");
            return;
        }

        // Start the fade coroutine
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Gradually fade out the text
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure the alpha is set to 0 at the end
        canvasGroup.alpha = 0f;

        // Optionally disable the text object
        gameObject.SetActive(false);
    }
}

