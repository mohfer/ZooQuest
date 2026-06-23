using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Tombol "Mulai Game" - selalu reset progress
    public void PlayGame()
    {
        Debug.Log("Mulai Game - Reset progress");
        
        // Reset progress di GameProgressManager
        if (GameProgressManager.Instance != null)
        {
            GameProgressManager.Instance.ResetProgress();
        }
        
        // Load scene Lobby
        SceneManager.LoadScene(1);
    }

    // Tombol "Keluar"
    public void QuitGame()
    {
        Debug.Log("Keluar game...");
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}