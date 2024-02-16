using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private bool soundEnabled = true;
    private bool musicEnabled = true;

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

    public void SetSoundEnabled(bool enabled)
    {
        soundEnabled = enabled;
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
    }

    public void PlayLevelSong()
    {
        if (musicEnabled)
            mainMenuSong.Stop();
            levelSong.Stop();
            levelSong.loop = true;
            levelSong.pitch = 1f;
            levelSong.Play();
    }

    public void PlayMainMenuSong()
    {
        if (musicEnabled)
            levelSong.Stop();
            mainMenuSong.Stop();
            mainMenuSong.loop = true;
            mainMenuSong.pitch = 1f;
            mainMenuSong.Play();
    }

    public void PlayButtonClickSound()
    {
        if (soundEnabled)
            buttonClickSound.Stop();
            buttonClickSound.pitch = 1f;
            buttonClickSound.Play();
    }

    public void PlayPurchaseSound()
    {
        if (soundEnabled)
            purchaseSound.Stop();
            purchaseSound.pitch = Random.Range(.8f, 1.2f);
            purchaseSound.Play();
    }

    public void PlayMoneyRainShort()
    {
        if (soundEnabled)
            moneyRainShort.Stop();
            moneyRainShort.pitch = Random.Range(.8f, 1.2f);
            moneyRainShort.Play();
    }

    public void PlayMoneyRain()
    {
        if (soundEnabled)
            moneyRain.Stop();
            moneyRain.pitch = Random.Range(.8f, 1.2f);
            moneyRain.Play();
    }

    public void PlayJudgeSound()
    {
        if (soundEnabled)
            judgeSound.Stop();
            judgeSound.pitch = Random.Range(.8f, 1.2f);
            judgeSound.Play();
    }

    public void PlaySwipeForward()
    {
        if (soundEnabled)
            swipeForward.Stop();
            swipeForward.pitch = Random.Range(.8f, 1.2f);
            swipeForward.Play();
    }

    public void PlaySwipeBack()
    {
        if (soundEnabled)
            swipeBack.Stop();
            swipeBack.pitch = Random.Range(.8f, 1.2f);
            swipeBack.Play();
    }

    public void PlayGemBreak()
    {
        if (soundEnabled)
            gemSound.Stop();
            gemSound.pitch = Random.Range(.8f, 1.2f);
            gemSound.Play();
    }

    public void PlayExplode()
    {
        if (soundEnabled)
            explodeSound.Stop();
            explodeSound.pitch = Random.Range(.8f, 1.2f);
            explodeSound.Play();
    }

    public void PlayStoneBreak()
    {
        if (soundEnabled)
            stoneSound.Stop();
            stoneSound.pitch = Random.Range(.8f, 1.2f);
            stoneSound.Play();
    }

    public void PlayRoundOver()
    {
        if (soundEnabled)
            roundOverSound.Stop();
            roundOverSound.Play();
    }
}