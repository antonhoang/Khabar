using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageWithCorners : MonoBehaviour
{
    public Image image;
    public float cornerRadius = 10f;

    void Start()
    {
        if (image == null)
        {
            // If the 'image' field is not assigned, try to find an Image component on this GameObject.
            image = GetComponent<Image>();
        }

        if (image != null)
        {
            // Call a method to set the corner radius of the Image.
            SetCornerRadius(cornerRadius);
        }
        else
        {
            Debug.LogWarning("No Image component found.");
        }
    }

    public void SetCornerRadius(float radius)
    {
        if (image != null)
        {
            // Set the corner radius by modifying the maskable graphic's mask.
            var mask = image.GetComponent<MaskableGraphic>();
            if (mask != null)
            {
                mask.maskable = true;
                mask.SetAllDirty();
                mask.canvasRenderer.EnableRectClipping(new Rect(0f, 0f, radius, radius));
            }
            else
            {
                Debug.LogWarning("No MaskableGraphic component found.");
            }
        }
        else
        {
            Debug.LogWarning("No Image component found.");
        }
    }
}


