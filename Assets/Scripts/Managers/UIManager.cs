using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string LevelSelect;
    public int movesLeft;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text movesTextLeft;
    public TMP_Text winScore;
    public TMP_Text windText;

    public GameObject winStars1, winStars2, winStars3;
    public GameObject roundOverScreen;
    public GameObject pauseScreen;

    private Board theBoard;


    private void Awake()
    {
        theBoard = FindObjectOfType<Board>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoves();
    }

    // Update is called once per frame
    void Update()
    {

        if (movesLeft == 0)
        {
            roundOverScreen.SetActive(true);
            // Stop the game and show level complete
            // Save Khabar coins to PlayerPrefs

        } else
        {
            roundOverScreen.SetActive(false);
        }
    }

    public void UpdateMoves()
    {
        if (movesTextLeft != null)
        {
            movesTextLeft.text = movesLeft.ToString();
        }
    }

    public void PauseUnpause()
    {
        if(!pauseScreen.activeInHierarchy)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ShuffleBoard()
    {
        theBoard.ShuffleBoard();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelSelect);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
