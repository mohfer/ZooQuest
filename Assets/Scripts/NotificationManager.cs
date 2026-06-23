using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [Header("Correct Notification")]
    [SerializeField] private GameObject correctNotificationPanel;
    [SerializeField] private TextMeshProUGUI correctNotificationText;
    [SerializeField] private AudioClip correctSound;

    [Header("Wrong Notification")]
    [SerializeField] private GameObject wrongNotificationPanel;
    [SerializeField] private TextMeshProUGUI wrongNotificationText;
    [SerializeField] private AudioClip wrongSound;

    [Header("Settings")]
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float soundVolume = 0.7f;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = soundVolume;
    }

    private void Start()
    {
        correctNotificationPanel.SetActive(false);
        wrongNotificationPanel.SetActive(false);
    }

    public void ShowCorrectNotification(string message)
    {
        correctNotificationText.text = message;
        correctNotificationPanel.SetActive(true);

        if (correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }

        StartCoroutine(HideCorrectAfterDelay());
    }

    public void ShowWrongNotification(string message)
    {
        wrongNotificationText.text = message;
        wrongNotificationPanel.SetActive(true);

        if (wrongSound != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }

        StartCoroutine(HideWrongAfterDelay());
    }

    public void ShowNotification(string message)
    {
        ShowWrongNotification(message);
    }

    private IEnumerator HideCorrectAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        correctNotificationPanel.SetActive(false);
    }

    private IEnumerator HideWrongAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        wrongNotificationPanel.SetActive(false);
    }
}