using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("UI")]
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        // Singleton (optional)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Hide pause panel saat start
        pausePanel.SetActive(false);
        
        // Pastikan game tidak paused
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    private void Update()
    {
        // Detect ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        
        // Pause game
        Time.timeScale = 0f;
        
        // Mute audio
        AudioListener.pause = true;
        
        Debug.Log("Game paused");
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        
        // Resume game
        Time.timeScale = 1f;
        
        // Unmute audio
        AudioListener.pause = false;
        
        Debug.Log("Game resumed");
    }

    public void QuitGame()
    {
        Debug.Log("Kembali ke menu utama...");
        
        // Resume dulu sebelum load scene (penting!)
        Time.timeScale = 1f;
        AudioListener.pause = false;
        
        // Load main menu (scene index 0)
        SceneManager.LoadScene(0);
    }
}