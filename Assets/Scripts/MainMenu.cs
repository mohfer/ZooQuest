using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        // Sembunyikan tombol Continue jika tidak ada save data
        if (continueButton != null)
        {
            continueButton.SetActive(SaveManager.Instance.HasSave());
        }
    }

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

    // Tombol "Lanjutkan" - load save data
    public void ContinueGame()
    {
        Debug.Log("Lanjutkan Game");
        
        if (GameProgressManager.Instance != null)
        {
            GameProgressManager.Instance.LoadProgress();
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