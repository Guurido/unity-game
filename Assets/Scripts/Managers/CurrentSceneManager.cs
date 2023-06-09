using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{
    public StringEventChannelSO OnLevelEnded;
    public VoidEventChannelSO OnEventLastCheckpointSO;

    private void OnEnable() {
        // On souscrit aux évènements
        OnLevelEnded.OnEventRaised += LoadScene;
    }

    private void OnDisable() {
        // On se désabonne aux évènements
        OnLevelEnded.OnEventRaised -= LoadScene;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            RestartLastCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleGameWindowSizeInEditor();
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            QuitGame();
        }

        if(Input.GetKeyDown(KeyCode.Alpha0)) {
            LoadScene("Debug");
        }
#endif
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLastCheckpoint()
    {
        Debug.Log("RestartLastCheckpoint");
        OnEventLastCheckpointSO.Raise();
        // Refill life to full
        // Position to last checkpoint
        // Remove menu
        // Reset Rigidbody
        // Reactivate Player movements
        // Reset Player's rotation
    }

    public void GameOverScreen()
    {
        Debug.Log("GameOverScreen");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #if UNITY_EDITOR
    private void ToggleGameWindowSizeInEditor()
    {
        UnityEditor.EditorWindow window = UnityEditor.EditorWindow.focusedWindow;
        window.maximized = !window.maximized;
    }
    #endif
}
