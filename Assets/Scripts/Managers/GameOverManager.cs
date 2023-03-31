using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public VoidEventSO onPlayerDeath;
    public GameObject gameOverScreenUI;
    
    private void Awake() {
        gameOverScreenUI.SetActive(false);
    }

    private void OnEnable() {
        onPlayerDeath.OnEventRaised += DisplayGameOverScreen;
    }

    private void DisplayGameOverScreen()
    {
        gameOverScreenUI.SetActive(true);
    }

    private void OnDisable() {
        onPlayerDeath.OnEventRaised -= DisplayGameOverScreen;
    }
}