using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameUI;
    private bool gamePaused;
    private bool gameOver;
    void Start() {
        NewGame();
    }
    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Paused();
        if(gameOver)
            GameOver();
    }

    private void NewGame() {
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
        gameOverMenu.SetActive(true);
    }
}
