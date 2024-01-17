using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour
{
    public string levelToLoad;
    public TMP_Text currentCoins;

    private void Update()
    {
        currentCoins.text = CoinManager.GetCoins().ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
