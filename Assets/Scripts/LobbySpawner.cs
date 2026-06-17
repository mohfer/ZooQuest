using UnityEngine;

public class LobbySpawner : MonoBehaviour
{
    public Transform spawnDefault;
    public Transform spawnForest;
    public Transform spawnIce;
    public Transform spawnSavanna;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch (SpawnData.SpawnID)
        {
            case 1:
                player.transform.position = spawnSavanna.position;
                break;

            case 2:
                player.transform.position = spawnIce.position;
                break;

            case 3:
                player.transform.position = spawnForest.position;
                break;

            default:
                player.transform.position = spawnDefault.position;
                break;
        }

        SpawnData.SpawnID = 0;
    }
}