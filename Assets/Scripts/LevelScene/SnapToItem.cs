using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapToItem : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;

    public HorizontalLayoutGroup HLG;
    bool isSnapped;
    public float snapForce;
    float snapSpeed;

    void Start()
    {
        isSnapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / sampleListItem.rect.width + HLG.spacing));
        if(scrollRect.velocity.magnitude < 200 && !isSnapped)
        {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
                contentPanel.localPosition.y,
                contentPanel.localPosition.z);
            if(contentPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing))) {
                isSnapped = true;
            }
        }
        if (scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }
    }
}
