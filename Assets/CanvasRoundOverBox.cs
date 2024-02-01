using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRoundOverBox : MonoBehaviour
{
    public RectTransform box;
    public CanvasGroup background;
    public float animationDuration = 0.3f;

    private void OnEnable()
    {
        // Set initial properties
        background.alpha = 0;
        box.localPosition = new Vector2(0, -Screen.height);

        // Start animations
        StartCoroutine(AnimateBackground());
        StartCoroutine(AnimateBox());
    }

    private System.Collections.IEnumerator AnimateBackground()
    {
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            background.alpha = Mathf.Lerp(0, 0.5f, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        background.alpha = 0.5f; // Ensure the final value is set correctly
    }

    private System.Collections.IEnumerator AnimateBox()
    {
        float elapsedTime = 0;
        Vector2 initialPosition = box.localPosition;
        Vector2 targetPosition = new Vector2(initialPosition.x, 0);

        while (elapsedTime < animationDuration)
        {
            box.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        box.anchoredPosition = targetPosition; // Ensure the final value


    }
}
