using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestatLevel : MonoBehaviour
{
    public BoolEventChannelSO onTogglePauseEvent;
    public GameObject pauseMenuUI;
    bool isGamePaused = false;
    private void Awake() {
        pauseMenuUI.SetActive(false);
    }
    public void ResetTheGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResumeTheGame() {
        Time.timeScale = 1;
        isGamePaused = false;
        onTogglePauseEvent.Raise(isGamePaused);
        pauseMenuUI.SetActive(isGamePaused);
    }

}
