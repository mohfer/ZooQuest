using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int currentActiveLevel = 1;
    public bool level2Unlocked = false;
    public bool level3Unlocked = false;
    public bool hasSaveData = false;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save(SaveData data)
    {
        data.hasSaveData = true;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved: " + saveFilePath);
    }

    public SaveData Load()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded");
            return data;
        }

        Debug.Log("No save file found");
        return null;
    }

    public bool HasSave()
    {
        return File.Exists(saveFilePath);
    }

    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted");
        }
    }
}
