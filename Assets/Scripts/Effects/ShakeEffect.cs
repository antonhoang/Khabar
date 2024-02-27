using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField]
    private float shakeDuration = 0.8f;

    [SerializeField]
    private float shakeMagnitude = 0.02f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void Shake()
    {
        if (IsInvoking("DoShake"))
            CancelInvoke("DoShake");

        InvokeRepeating("DoShake", 0, 0.03f);
        Invoke("StopShake", shakeDuration);
    }

    private void DoShake()
    {
        float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
        Vector3 offset = new Vector3(offsetX, 0, 0);

        float lerpFactor = Mathf.PingPong(Time.time * 10 / shakeDuration, 1f);
        transform.position = Vector3.Lerp(originalPosition, originalPosition + offset, lerpFactor);
    }

    private void StopShake()
    {
        CancelInvoke("DoShake");
        transform.position = originalPosition;
    }
}

