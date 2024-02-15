using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UniversalLevelManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image background;

    void Start()
    {
        int level = LevelSelectButton.selectedLevel;
        background.sprite = backgrounds[level];
        SFXManager.instance.PlayLevelSong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string mainMenu = "StartScene";

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
