using TicTacToeUnityGame.Scripts;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GameBoardLogic
{
    private UserActionEvents _userActionEvents;
    private GameController _gameController;
    private BoardGridSize boardGridSize;
    private StorageController _storageController;
    
    public GameBoardLogic(GameView gameView, UserActionEvents userActionEvents)
    {
        _gameController = new GameController(gameView);
        _userActionEvents = userActionEvents;
        _storageController = new StorageController(_gameController);
    }

    public void Initialize(int columns, int rows)
    {
        boardGridSize = new BoardGridSize(columns, rows);
        _userActionEvents.StartGameClicked += OnStartGameClicked;
        _userActionEvents.TileClicked += OnTileClicked;
        _userActionEvents.SaveStateClicked += OnSaveStateClicked;
        _userActionEvents.LoadStateClicked += OnLoadStateClicked;
    }

    public void DeInitialize()
    {
        _userActionEvents.StartGameClicked -= OnStartGameClicked;
        _userActionEvents.TileClicked -= OnTileClicked;
        _userActionEvents.SaveStateClicked -= OnSaveStateClicked;
        _userActionEvents.LoadStateClicked -= OnLoadStateClicked;
    }

    private void OnStartGameClicked()
    {
        _gameController.GameSetUp();
    }

    private void OnTileClicked(BoardTilePosition tilePosition)
    {
        _gameController.TileClicked(tilePosition);
    }

    private void OnSaveStateClicked(GameStateSource storageType)
    {
        //Saving game state logic based on the storageType
        _storageController.Save(storageType);
    }

    private void OnLoadStateClicked(GameStateSource storageType)
    {
        //Loads the last saved game state
        _storageController.Load(storageType);
        _gameController.SetBoard();
    }
}
