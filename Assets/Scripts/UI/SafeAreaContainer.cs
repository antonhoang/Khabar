using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaContainer : MonoBehaviour
{

    private Rect lastSafeArea;
    private RectTransform parentRectTransform;

    private void Start()
    {
        parentRectTransform = this.GetComponentInParent<RectTransform>();
    }

    private void Update()
    {
        if (lastSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea()
    {
        Rect safeAreaRect = Screen.safeArea;

        float scaleRatio = parentRectTransform.rect.width / Screen.width;

        var left = safeAreaRect.xMin * scaleRatio;
        var right = -(Screen.width - safeAreaRect.xMax) * scaleRatio;
        var top = -(Screen.height - safeAreaRect.yMax) * scaleRatio;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(left, 0);
        rectTransform.offsetMax = new Vector2(right, top);

        lastSafeArea = Screen.safeArea;
    }
}