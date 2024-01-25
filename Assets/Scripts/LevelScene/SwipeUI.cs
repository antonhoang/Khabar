using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;

public class SwipeUI : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;                    // Scrollbar¿« ¿ßƒ°∏¶ πŸ≈¡¿∏∑Œ «ˆ¿Á ∆‰¿Ã¡ˆ ∞ÀªÁ
    [SerializeField]
    private float swipeTime = 0.2f;         // ∆‰¿Ã¡ˆ∞° Swipe µ«¥¬ Ω√∞£
    [SerializeField]
    private float swipeDistance = 50.0f;        // ∆‰¿Ã¡ˆ∞° Swipeµ«±‚ ¿ß«ÿ øÚ¡˜ø©æﬂ «œ¥¬ √÷º“ ∞≈∏Æ

    private float[] scrollPageValues;           // ∞¢ ∆‰¿Ã¡ˆ¿« ¿ßƒ° ∞™ [0.0 - 1.0]
    private float valueDistance = 0;            // ∞¢ ∆‰¿Ã¡ˆ ªÁ¿Ã¿« ∞≈∏Æ
    private int currentPage = 0;            // «ˆ¿Á ∆‰¿Ã¡ˆ
    private int maxPage = 0;                // √÷¥Î ∆‰¿Ã¡ˆ
    private float startTouchX;              // ≈Õƒ° Ω√¿€ ¿ßƒ°
    private float endTouchX;                    // ≈Õƒ° ¡æ∑· ¿ßƒ°
    private bool isSwipeMode = false;       // «ˆ¿Á Swipe∞° µ«∞Ì ¿÷¥¬¡ˆ √º≈©

    private void Awake()
    {
        scrollPageValues = new float[transform.childCount];
        
        valueDistance = 1f / (scrollPageValues.Length - 1f);

        
        for (int i = 0; i < scrollPageValues.Length; ++i)
        {
            scrollPageValues[i] = valueDistance * i;
        }

        maxPage = transform.childCount;
    }

    private void Start()
    {
        SetScrollBarValue(0);
    }

    public void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (isSwipeMode == true) return;
        if (Input.GetMouseButtonDown(0))
        {
            //scroll_pos = scrollBar.value;
            startTouchX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //scroll_pos = scrollBar.value;
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }


#if UNITY_ANDROID
		if ( Input.touchCount == 1 )
		{
			Touch touch = Input.GetTouch(0);

			if ( touch.phase == TouchPhase.Began )
			{
				// ≈Õƒ° Ω√¿€ ¡ˆ¡° (Swipe πÊ«‚ ±∏∫–)
				startTouchX = touch.position.x;
			}
			else if ( touch.phase == TouchPhase.Ended )
			{
				// ≈Õƒ° ¡æ∑· ¡ˆ¡° (Swipe πÊ«‚ ±∏∫–)
				endTouchX = touch.position.x;

				UpdateSwipe();
			}
		}
#endif
    }

    private void UpdateSwipe()
    {
        
        if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        // Swipe πÊ«‚
        bool isLeft = startTouchX < endTouchX ? true : false;

        
        if (isLeft == true)
        {
            if (currentPage == 0) return;

            currentPage--;
        }
        else
        {
            if (currentPage == maxPage - 1) return;

            currentPage++;
        }

        StartCoroutine(OnSwipeOneStep(currentPage));
    }

    private IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipeMode = true;

        

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);
            yield return null;
        }

        isSwipeMode = false;
    }
}