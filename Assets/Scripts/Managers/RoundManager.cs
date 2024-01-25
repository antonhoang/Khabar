using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    //public float roundTime = 60f;
    private UIManager uiMan;
    private bool endingRound = false;
    private Board board;

    public int currentScore;
    public float displayScore, scoreSpeed;

    public int scoreTarget1, scoreTarget2, scoreTarget3;
    // Start is called before the first frame update
    void Awake()
    {
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        displayScore = Mathf.Lerp(displayScore, currentScore, scoreSpeed * Time.deltaTime); 
        uiMan.scoreText.text = displayScore.ToString("0");
    }

    public void ResultingScore()
    {
        if (uiMan.movesLeft == 0)
        {
            CoinManager.AddCoins(currentScore);
        }
        
    }

    private void LateUpdate()
    {
        if (uiMan.movesLeft == 0)
        {
            uiMan.roundOverScreen.SetActive(true);
        }
        else
        {
            uiMan.roundOverScreen.SetActive(false);
        }
    }

    private void RoundTime()
    {
        //if(roundTime > 0)
        //{
        //    roundTime -= Time.deltaTime;

        //    if (roundTime <= 0)
        //    {
        //        roundTime = 0;

        //        endingRound = true;
        //    }
        //}

        //if (endingRound && board.currentState == Board.BoardState.move)
        //{
        //    WinCheck();
        //    endingRound = false;
        //}

        //uiMan.timeText.text = roundTime.ToString("0.0") + "s";
    }

    private void WinCheck()
    {
        uiMan.roundOverScreen.SetActive(true);

        uiMan.winScore.text = currentScore.ToString();
        if(currentScore >= scoreTarget3)
        {
            uiMan.windText.text = "Congratulations! You earned 3 stars!";
            uiMan.winStars3.SetActive(true);

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star2", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star3", 1);
        }
        else if (currentScore >= scoreTarget2)
        {
            uiMan.windText.text = "Congratulations! You earned 2 stars!";
            uiMan.winStars2.SetActive(true);

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star2", 1);
        }

        else if (currentScore >= scoreTarget1)
        {
            uiMan.windText.text = "Congratulations! You earned 1 stars!";
            uiMan.winStars1.SetActive(true);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
        }
        else
        {
            uiMan.windText.text = "Oh no! No Stars for you! Try again?";
        }
    }
}
