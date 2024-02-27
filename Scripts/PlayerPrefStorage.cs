using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TicTacToeUnityGame.Scripts;


public class PlayerPrefStorage: IStorageInterface
{
    private const string GameData = "GameData";
    public PlayerPrefStorage()
    {
    }

    public void SaveGame(GameData game)
    {
        if (game != null)
        {
            // Serialize the GameData object to JSON
            string jsonString = JsonUtility.ToJson(game);
            // Save the JSON string to PlayerPrefs
            PlayerPrefs.SetString(GameData, jsonString);
            PlayerPrefs.Save();
        }
    }
    public GameData LoadGame()
    {
        if (PlayerPrefs.HasKey(GameData))
        {
            // Retrieve the JSON string from PlayerPrefs
            string jsonString = PlayerPrefs.GetString(GameData);
            // Deserialize the JSON string to a GameData object
            return JsonUtility.FromJson<GameData>(jsonString); 
        }
        //If the file doesn't exist, return a new instance of GameData
        return new GameData();
    }
}
