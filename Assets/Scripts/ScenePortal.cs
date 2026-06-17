using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public int sceneIndex;
    public int spawnID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnData.SpawnID = spawnID;
            SceneManager.LoadScene(sceneIndex);
        }
    }
}