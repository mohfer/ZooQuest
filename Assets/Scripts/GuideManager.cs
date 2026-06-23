using UnityEngine;
using TMPro;

public class GuideManager : MonoBehaviour
{
    public static GuideManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private TextMeshProUGUI guideText;

    [Header("Sound")]
    [SerializeField] private AudioClip guideChangedSound;

    [Header("Guide Messages")]
    [SerializeField] private string level1Message = "Pergi ke Savanna untuk mendapatkan informasi";
    [SerializeField] private string level1QuizMessage = "Kembali ke lobby untuk menyelesaikan quiz Savanna";
    [SerializeField] private string level2Message = "Pergi ke Iced untuk mendapatkan informasi";
    [SerializeField] private string level2QuizMessage = "Kembali ke lobby untuk menyelesaikan quiz Iced";
    [SerializeField] private string level3Message = "Pergi ke Forest untuk mendapatkan informasi";
    [SerializeField] private string level3QuizMessage = "Kembali ke lobby untuk menyelesaikan quiz Forest";
    [SerializeField] private string completedMessage = "Selamat! Semua level telah diselesaikan";

    private string lastMessage;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateGuideText();
    }

    private void OnEnable()
    {
        UpdateGuideText();
    }

    public void UpdateGuideText()
    {
        if (GameProgressManager.Instance == null)
        {
            Invoke(nameof(UpdateGuideText), 0.1f);
            return;
        }

        if (guidePanel == null || guideText == null)
        {
            Debug.LogWarning("GuidePanel atau GuideText belum di-assign!");
            return;
        }

        int currentLevel = GameProgressManager.Instance.currentActiveLevel;
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        string message = GetGuideMessage(currentLevel, sceneName);
        guideText.text = message;

        if (message != lastMessage)
        {
            lastMessage = message;
            if (audioSource != null && guideChangedSound != null)
                audioSource.PlayOneShot(guideChangedSound);
        }

        guidePanel.SetActive(true);
    }

    private string GetGuideMessage(int currentLevel, string sceneName)
    {
        switch (currentLevel)
        {
            case GameProgressManager.LEVEL_SAVANNA:
                if (sceneName == "Lobby")
                {
                    return level1Message;
                }
                else if (sceneName == "Savanna")
                {
                    return level1QuizMessage;
                }
                return level1Message;

            case GameProgressManager.LEVEL_ICED:
                if (sceneName == "Lobby")
                {
                    return level2Message;
                }
                else if (sceneName == "Iced")
                {
                    return level2QuizMessage;
                }
                return level2Message;

            case GameProgressManager.LEVEL_FOREST:
                if (sceneName == "Lobby")
                {
                    return level3Message;
                }
                else if (sceneName == "Forest")
                {
                    return level3QuizMessage;
                }
                return level3Message;

            default:
                return completedMessage;
        }
    }

    public void HideGuide()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(false);
        }
    }

    public void ShowGuide()
    {
        UpdateGuideText();
    }
}