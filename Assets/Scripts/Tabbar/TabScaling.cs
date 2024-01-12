using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TabScaling : MonoBehaviour
{
    public bool isInitial;

    public RectTransform uiElement; // Reference to the UI element's RectTransform

    private Vector3 originalScale = Vector3.one;
    private Vector3 selectedScale = Vector3.one * 1.2f;
    private Vector3 originalPosition;
    private float animationDuration = 0.2f;
    private float targetYPosition;
    private void Awake()
    {
        originalPosition = uiElement.anchoredPosition;
    }

    private void Start()
    {
        uiElement = GetComponent<RectTransform>();
        
        if (isInitial)
        {
            Select();
        } else
        {
            transform.localScale = originalScale;
            StartCoroutine(AnimateYPosition(originalPosition.y));
        }
    }

    public void Select()
    {
        targetYPosition = originalPosition.y + 60f;
        // Animate the scale change
        StartCoroutine(AnimateScale(selectedScale));

        // Animate the position change
        StartCoroutine(AnimateYPosition(targetYPosition));
    }

    public void Deselect()
    {
        targetYPosition = originalPosition.y;
        // Animate the scale change
        StartCoroutine(AnimateScale(originalScale));

        // Animate the position change
        StartCoroutine(AnimateYPosition(targetYPosition));
    }

    private IEnumerator AnimateYPosition(float targetY)
    {
        
        float elapsedTime = 0f;
        float initialY = uiElement.anchoredPosition.y;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float newY = Mathf.Lerp(initialY, targetY, t);
            uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x, newY);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x, targetY); // Ensure the final Y position is exactly the target position
        
    }

    private IEnumerator AnimateScale(Vector3 targetScale)
    {
        float duration = 0.2f; // Adjust the duration of the animation
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsed / duration);
            yield return null;
        }

        transform.localScale = targetScale; // Ensure the final scale is exactly the target scale
    }

    public void OnClick()
    {
        // Notify the TabPanel when this tab is clicked
        PanelTabBar tabPanel = transform.parent.GetComponent<PanelTabBar>();
        if (tabPanel != null)
        {
            tabPanel.OnTabClicked(this);
        }
    }
}
