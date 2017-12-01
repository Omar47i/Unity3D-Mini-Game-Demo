using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;
using System;

public class DataParser : MonoBehaviour
{
	string fileName = "GameConfiguration.json";
    GameData loadedData;

    public static DataParsedEvent DataParsed = new DataParsedEvent();

    void Start()
    {
        LoadMapData();
    }

    private void LoadMapData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

			loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            DataParsed.Invoke(loadedData);
        }
        else
        {
            Debug.LogError("Cannot load data!");
        }
    }
}

public class DataParsedEvent : UnityEvent<GameData>
{
}