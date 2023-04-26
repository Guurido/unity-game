using UnityEngine;

public class gameOverManager : MonoBehaviour
{
    public VoidEventChannelSO onPlayerDeath;
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
