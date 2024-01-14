using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string mainMenu;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
