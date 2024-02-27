using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TicTacToeUnityGame.Scripts;
using Unity.Plastic.Newtonsoft.Json;
using System.IO;

public class InMemoryStorage : IStorageInterface
{
    public InMemoryStorage()
    {
    }

    public void SaveGame(GameData game)
    {
        if (game != null)
        {
            string gameToStorage = JsonUtility.ToJson(game);
            string filePath = Application.persistentDataPath + "/gameData.json"; // File path to store JSON data
            System.IO.File.WriteAllText(filePath, gameToStorage);
        }
    }

    public GameData LoadGame()
    {
        string filePath = Application.persistentDataPath + "/gameData.json"; // Loads from File path the JSON data
        //Check if the file exists
        if (File.Exists(filePath))
        {
            string gameFromStorage = System.IO.File.ReadAllText(filePath);
            // Deserialize the JSON string to a GameData object
            return JsonUtility.FromJson<GameData>(gameFromStorage); 
        }
        //If the file doesn't exist, return a new instance of GameData
        return new GameData();
    }
}