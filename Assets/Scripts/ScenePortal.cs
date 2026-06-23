using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public int sceneIndex;
    public int spawnID;

    [Header("Lock System")]
    public bool isReturnPortal;
    public int targetLevelID;

    [Header("Visual (Optional)")]
    public SpriteRenderer portalSprite;
    public Color lockedColor = Color.gray;
    public Color unlockedColor = Color.white;

    private bool isUnlocked;

    private void Start()
    {
        CheckUnlockStatus();
    }

    private void CheckUnlockStatus()
    {
        if (isReturnPortal)
        {
            isUnlocked = true;
            if (portalSprite != null)
            {
                portalSprite.color = unlockedColor;
            }
            return;
        }

        if (GameProgressManager.Instance == null)
        {
            Invoke(nameof(CheckUnlockStatus), 0.1f);
            return;
        }

        isUnlocked = GameProgressManager.Instance.IsLevelUnlocked(targetLevelID);

        if (portalSprite != null)
        {
            portalSprite.color = isUnlocked ? unlockedColor : lockedColor;
        }
    }

    // Method public untuk refresh dari luar
    public void RefreshUnlockStatus()
    {
        CheckUnlockStatus();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isUnlocked)
            {
                SpawnData.SpawnID = spawnID;
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                NotificationManager.Instance.ShowNotification(
                    "Portal terkunci! Selesaikan quiz terlebih dahulu."
                );
            }
        }
    }

    private void OnEnable()
    {
        CheckUnlockStatus();
    }
}