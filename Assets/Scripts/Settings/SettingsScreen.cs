using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    public GameObject aboutTheGameScreen;
    public GameObject aboutUsScreen;
    public GameObject privacyPolicyScreen;
    public GameObject termsOfUseScreen;
    public GameObject supportUsScreen;

    public Toggle soundToggle;
    public Toggle musicToggle;

    private void Start()
    {
        // Load player preferences for sound and music
        soundToggle.isOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("Music", 1) == 1;

        // Apply preferences
        UpdateSound();
        UpdateMusic();
    }

    public void ToggleSound()
    {
        PlayerPrefs.SetInt("Sound", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateSound();
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt("Music", musicToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateMusic();
    }

    private void UpdateSound()
    {
        SFXManager.instance.SetSoundEnabled();
    }

    private void UpdateMusic()
    {
        SFXManager.instance.SetMusicEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAboutTheGame()
    {
        aboutTheGameScreen.SetActive(true);
    }

    public void CloseAboutTheGame()
    {
        aboutTheGameScreen.SetActive(false);
    }

    public void ShowAboutUs()
    {
        aboutUsScreen.SetActive(true);
    }

    public void CloseAboutUs()
    {
        aboutUsScreen.SetActive(false);
    }

    public void ShowTermsOfUse()
    {
        termsOfUseScreen.SetActive(true);
    }

    public void CloseTermsOfUse()
    {
        termsOfUseScreen.SetActive(false);
    }

    public void ShowPrivacyPolicy()
    {
        privacyPolicyScreen.SetActive(true);
    }

    public void ClosePrivacyPolicy()
    {
        privacyPolicyScreen.SetActive(false);
    }

    public void ShowSupportUs()
    {
        supportUsScreen.SetActive(true);
    }

    public void HelpUsBMC()
    {
        Application.OpenURL("https://www.buymeacoffee.com/5xcnzc9ln0/");
    }

    public void HelpUsBank()
    {
        Application.OpenURL("https://send.monobank.ua/jar/8ZJHnKVu58");
    }

    public void CloseSupportUs()
    {
        supportUsScreen.SetActive(false);
    }
}
