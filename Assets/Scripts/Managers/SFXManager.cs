using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set the instance to this object
            instance = this;
            // Ensure this object persists across scenes
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name != "UniversalLevel")
        {
            PlayMainMenuSong();
        } 

            
    }

    public AudioSource
        gemSound,
        explodeSound,
        stoneSound,
        roundOverSound,
        swipeForward,
        swipeBack,
        judgeSound,
        moneyRain,
        moneyRainShort,
        purchaseSound,
        buttonClickSound,
        mainMenuSong,
        levelSong;

    public void PlayLevelSong()
    {
        mainMenuSong.Stop();
        levelSong.Stop();
        levelSong.loop = true;
        levelSong.pitch = 1f;
        levelSong.Play();
    }

    public void PlayMainMenuSong()
    {
        levelSong.Stop();
        mainMenuSong.Stop();
        mainMenuSong.loop = true;
        mainMenuSong.pitch = 1f;
        mainMenuSong.Play();
    }

    public void PlayButtonClickSound()
    {
        buttonClickSound.Stop();
        buttonClickSound.pitch = 1f;
        buttonClickSound.Play();
    }

    public void PlayPurchaseSound()
    {
        purchaseSound.Stop();

        purchaseSound.pitch = Random.Range(.8f, 1.2f);

        purchaseSound.Play();
    }

    public void PlayMoneyRainShort()
    {
        moneyRainShort.Stop();

        moneyRainShort.pitch = Random.Range(.8f, 1.2f);

        moneyRainShort.Play();
    }

    public void PlayMoneyRain()
    {
        moneyRain.Stop();

        moneyRain.pitch = Random.Range(.8f, 1.2f);

        moneyRain.Play();
    }

    public void PlayJudgeSound()
    {
        judgeSound.Stop();

        judgeSound.pitch = Random.Range(.8f, 1.2f);

        judgeSound.Play();
    }

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
        roundOverSound.Stop();
        roundOverSound.Play();
    }
}