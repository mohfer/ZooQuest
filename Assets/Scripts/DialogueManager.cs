using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;

    public TMP_Text titleText;
    public TMP_Text informationText;

    public Image animalImage;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string title, string information, Sprite image)
    {
        // Null check untuk debugging
        if (dialoguePanel == null) Debug.LogError("dialoguePanel belum di-assign!");
        if (titleText == null) Debug.LogError("titleText belum di-assign!");
        if (informationText == null) Debug.LogError("informationText belum di-assign!");
        if (animalImage == null) Debug.LogError("animalImage belum di-assign!");
        if (image == null) Debug.LogError("Sprite image dari Interactable null!");

        // Aktifkan panel dialog
        dialoguePanel.SetActive(true);

        // Isi konten
        titleText.text = title;
        informationText.text = information;
        animalImage.sprite = image;
        animalImage.SetNativeSize();
    }

    public void Close()
    {
        dialoguePanel.SetActive(false);
    }
}