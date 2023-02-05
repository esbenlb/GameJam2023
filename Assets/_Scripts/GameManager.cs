using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Master master;
    private int score;
    private int highScore;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameUI;
    private bool gamePaused;
    private bool gameOver;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    void Start() {
        NewGame();
    }
    void Update() {
        ScoreHandler();
        if(score > 5)
            GameOver();
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Paused();
        if(gameOver || master.stats.nitrogen < 0f || master.stats.water < 0f)
            GameOver();
    }

    private void NewGame() {
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        gamePaused = false;
        gameOver = false;
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    private void Paused() {
        if(!gamePaused && !gameOver) {
            gamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else if(gamePaused && !gameOver){
            gamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    private void GameOver() {
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        gameUI.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    private void ScoreHandler() {
        score = GameUI.dayNbr;
        if(score > highScore)
            highScore = score;
        //  Convert to text
        highScoreText.text = 
        "High Score\n" +
        highScore.ToString() +
        "\n"
        ;
        scoreText.text = 
        "Score\n" +
        score.ToString() +
        "\n"
        ;
    }
}
