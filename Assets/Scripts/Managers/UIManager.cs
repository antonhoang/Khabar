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

    public GameObject roundOverScreen;
    public GameObject pauseScreen;

    private Board theBoard;
    public ParticleSystem coinEffect;


    private void Awake()
    {
        coinEffect.Stop();
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
        if(!pauseScreen.activeSelf)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            
        } else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void ShuffleBoard()
    {
        Time.timeScale = 1f;
        theBoard.ShuffleBoard();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelSelect);
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToStore()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Store");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

}
