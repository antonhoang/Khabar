using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public int levelToLoad;
    public const string CURRENT_LEVEL_KEY = "CurrentLevel";

    void Start()
    {
    }

    public void LoadLevel()
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, levelToLoad);
        PlayerPrefs.Save();

        string sceneName = "L" + levelToLoad;
        SceneManager.LoadScene(sceneName);
    }



}
