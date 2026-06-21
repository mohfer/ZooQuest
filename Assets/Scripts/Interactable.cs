using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject prompt;

    [Header("Konten")]
    public string title;

    [TextArea(3, 10)]
    public string information;

    public Sprite animalImage;

    private bool playerInRange;

    private void Start()
    {
        prompt.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.Show(
                title,
                information,
                animalImage
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            prompt.SetActive(false);
        }
    }
}