using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    public const int LEVEL_SAVANNA = 1;
    public const int LEVEL_ICED = 2;
    public const int LEVEL_FOREST = 3;

    [Header("Current Progress")]
    public int currentActiveLevel = 1;
    
    // In-memory unlock status (tidak pakai PlayerPrefs)
    private bool level2Unlocked = false;
    private bool level3Unlocked = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Selalu mulai dari level 1, tidak load dari PlayerPrefs
            ResetProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsLevelUnlocked(int levelID)
    {
        switch (levelID)
        {
            case LEVEL_SAVANNA:
                return true; // Savanna selalu unlocked
            case LEVEL_ICED:
                return level2Unlocked;
            case LEVEL_FOREST:
                return level3Unlocked;
            default:
                return false;
        }
    }

    public void CompleteCurrentLevel()
    {
        int nextLevel = currentActiveLevel + 1;

        Debug.Log($"Level {currentActiveLevel} selesai!");

        if (nextLevel == LEVEL_ICED)
        {
            level2Unlocked = true;
            Debug.Log($"Level Iced (2) telah dibuka!");
        }
        else if (nextLevel == LEVEL_FOREST)
        {
            level3Unlocked = true;
            Debug.Log($"Level Forest (3) telah dibuka!");
        }

        currentActiveLevel = nextLevel;

        RefreshAllPortals();
        
        // Refresh guide text
        if (GuideManager.Instance != null)
        {
            GuideManager.Instance.UpdateGuideText();
        }
    }

    private void RefreshAllPortals()
    {
        ScenePortal[] portals = FindObjectsOfType<ScenePortal>();
        foreach (ScenePortal portal in portals)
        {
            portal.RefreshUnlockStatus();
        }
        Debug.Log($"{portals.Length} portal direfresh");
    }

    public string GetCurrentLevelName()
    {
        switch (currentActiveLevel)
        {
            case LEVEL_SAVANNA: return "Savanna";
            case LEVEL_ICED: return "Iced";
            case LEVEL_FOREST: return "Forest";
            default: return "Unknown";
        }
    }

    public void ResetProgress()
    {
        currentActiveLevel = 1;
        level2Unlocked = false;
        level3Unlocked = false;
        
        Debug.Log("Progress direset ke Level 1");
    }

    // Testing cheat
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProgress();
            RefreshAllPortals();
        }
    }
}