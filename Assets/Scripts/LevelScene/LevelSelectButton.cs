using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public static int selectedLevel;
    public int levelToLoad;
    public const string CURRENT_LEVEL_KEY = "CurrentLevel";

    void Start()
    {
    }

    public void LoadLevel()
    {
        selectedLevel = levelToLoad;
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, levelToLoad);
        PlayerPrefs.Save();

        string sceneName = "L" + levelToLoad;
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene("UniversalLevel");
    }



}
