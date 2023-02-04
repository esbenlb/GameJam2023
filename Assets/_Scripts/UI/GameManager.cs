using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool gamePaused;
    void Start() {
        gamePaused = false;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Paused();
    }

    private void Paused() {
        if(!gamePaused) {
            gamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else {
            gamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
