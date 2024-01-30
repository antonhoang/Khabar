using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    public GameObject aboutTheGameScreen;
    public GameObject aboutUsScreen;
    public GameObject privacyPolicyScreen;
    public GameObject termsOfUseScreen;
    public GameObject supportUsScreen;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Application.OpenURL("https://send.monobank.ua/jar/3eGqY9Zxn5");
    }

    public void CloseSupportUs()
    {
        supportUsScreen.SetActive(false);
    }
}
