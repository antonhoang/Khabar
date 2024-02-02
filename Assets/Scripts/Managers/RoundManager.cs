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

    private void LateUpdate()
    {
        
        if (uiMan.movesLeft == 0 && board.currentState == Board.BoardState.move)
        {
            CoinManager.AddCoins(currentScore);
            uiMan.roundOverScreen.SetActive(true);
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
}
