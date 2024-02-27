using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TicTacToeUnityGame.Scripts;

/// <summary>
/// Responsible for creating instances of 2 classes from type IStorageInterface.
/// Was designed to be convenient to add or change later.
/// </summary>
public class StorageController
{
    private GameController _gameController;
    private InMemoryStorage inMemoryStorage;
    private PlayerPrefStorage playerPrefStorage;
    public StorageController(GameController gameController)
    {
        _gameController = gameController;
        inMemoryStorage = new InMemoryStorage();
        playerPrefStorage = new PlayerPrefStorage();
    }
    
    public void Save(GameStateSource storagetype)
    {
        GameData gameObject = new GameData
        {
            board = _gameController.tictactoeTiles,
            currentPlayer = _gameController._currentPlayer,
            turnCount = _gameController.turnCount
        };

        if (storagetype == GameStateSource.PlayerPrefs)
        {
            playerPrefStorage.SaveGame(gameObject);
        }
        else
        {
            inMemoryStorage.SaveGame(gameObject);
        }
    }
    public void Load(GameStateSource storagetype)
    {
        GameData gameObject = new GameData();
        if (storagetype == GameStateSource.PlayerPrefs)
        {
            gameObject = playerPrefStorage.LoadGame();
        }
        else
        {
            gameObject = inMemoryStorage.LoadGame();
        }
        _gameController.tictactoeTiles = gameObject.board;
        _gameController._currentPlayer = gameObject.currentPlayer;
        _gameController.turnCount = gameObject.turnCount;
    }
}

/// <summary>
/// Object to store in a Json Storage
/// </summary>
[Serializable]
public class GameData
{
    public int[] board;
    public PlayerType currentPlayer;
    public int turnCount;
}