using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource gemSound, explodeSound, stoneSound, roundOverSound, swipeForward, swipeBack;

    public void PlaySwipeForward()
    {
        swipeForward.Stop();

        swipeForward.pitch = Random.Range(.8f, 1.2f);

        swipeForward.Play();
    }

    public void PlaySwipeBack()
    {
        swipeBack.Stop();

        swipeBack.pitch = Random.Range(.8f, 1.2f);

        swipeBack.Play();
    }

    public void PlayGemBreak()
    {
        gemSound.Stop();

        gemSound.pitch = Random.Range(.8f, 1.2f);

        gemSound.Play();
    }

    public void PlayExplode()
    {
        explodeSound.Stop();

        explodeSound.pitch = Random.Range(.8f, 1.2f);

        explodeSound.Play();
    }

    public void PlayStoneBreak()
    {
        stoneSound.Stop();

        stoneSound.pitch = Random.Range(.8f, 1.2f);

        stoneSound.Play();
    }

    public void PlayRoundOver()
    {
        roundOverSound.Play();
    }
}