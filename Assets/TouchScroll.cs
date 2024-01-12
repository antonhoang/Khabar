using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScroll : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 touchDelta;

    public ScrollRect scrollRect;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchEndPos = touch.position;
                touchDelta = touchEndPos - touchStartPos;
                scrollRect.normalizedPosition -= touchDelta / Screen.width;
            }
        }
    }
}
