using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TicTacToeUnityGame.Scripts;

public class GameController : MonoBehaviour
{
    private const int GRID_SIZE = 3; // Grid size columns number
    private const int INITIAL_TILE_VALUE = -100; //
    public GameView _gameView; 
    public GameObject board;
    public PlayerType _currentPlayer;
    public int turnCount;  //counts the number of turn played
    public int[] tictactoeTiles = new int[9];
    private bool isWin;
    public Button[] _tiles;
    public AudioSource buttonClickAudio;
    
    public GameController(GameView gameView)
    {
        _gameView = gameView;
    }

    public void GameSetUp()
    {
        _currentPlayer = PlayerType.PlayerX;
        _gameView.StartGame(_currentPlayer);
        turnCount = 0;
        isWin = false;
        for (int i= 0; i< 9; i++)
        {
            tictactoeTiles[i] = INITIAL_TILE_VALUE; //initialize
            //_tiles[i].interactable = true;
        }
    }

    // Update is called once per frame
    public void TileClicked(BoardTilePosition tilePosition)
    {
        if(tictactoeTiles[tilePosition.Row * GRID_SIZE + tilePosition.Column] == INITIAL_TILE_VALUE && !isWin)//Unable to click again the same tile
        {
            _gameView.SetTileSign(_currentPlayer, tilePosition);
            tictactoeTiles[tilePosition.Row * GRID_SIZE + tilePosition.Column] = ((int)_currentPlayer) + 1;
            turnCount++;

             //Check if win
            if (turnCount > 4)
            {
                WinnerCheck();
                //Check If tie
                if (turnCount == 9 && !isWin)
                {
                    _gameView.GameTie();
                }
            }
            SwitchPlayer();
        }
    }

    public void SetBoard()
    {
        if(_gameView != null)
        {
            for(int i=0; i< tictactoeTiles.Length; i++)
            {
                if(tictactoeTiles[i]!= -1)
                {
                    int tempPlayer = tictactoeTiles[i] - 1;
                    PlayerType player = tempPlayer == 0 ? PlayerType.PlayerX : PlayerType.PlayerO;
                    int row = i / GRID_SIZE;
                    int column = i % GRID_SIZE;
                    BoardTilePosition tilePosition = new BoardTilePosition(row, column);
                    _gameView.SetTileSign(player, tilePosition);
                }
            }
        }
    }

    private void SwitchPlayer()
    {
        if (_currentPlayer == PlayerType.PlayerX)
            _currentPlayer = PlayerType.PlayerO;
        else
            _currentPlayer = PlayerType.PlayerX;
        _gameView.ChangeTurn(_currentPlayer); // Changes the Player turn visually.
    }

    public void WinnerCheck()
    {
        int[] possiibleSolutions = InitPossibleSolutions();
        for (int i = 0; i< possiibleSolutions.Length; i++)
        {
            if(possiibleSolutions[i] == GRID_SIZE * (((int)_currentPlayer) + 1)){
                _gameView.GameWon(_currentPlayer);
                isWin = true;
                return;
            }
        }
    }

    public int[] InitPossibleSolutions()
    {
        int s1 = tictactoeTiles[0] + tictactoeTiles[1] + tictactoeTiles[2];
        int s2 = tictactoeTiles[3] + tictactoeTiles[4] + tictactoeTiles[5];
        int s3 = tictactoeTiles[6] + tictactoeTiles[7] + tictactoeTiles[8];
        int s4 = tictactoeTiles[0] + tictactoeTiles[3] + tictactoeTiles[6];
        int s5 = tictactoeTiles[1] + tictactoeTiles[4] + tictactoeTiles[7];
        int s6 = tictactoeTiles[2] + tictactoeTiles[5] + tictactoeTiles[8];
        int s7 = tictactoeTiles[0] + tictactoeTiles[4] + tictactoeTiles[8];
        int s8 = tictactoeTiles[2] + tictactoeTiles[4] + tictactoeTiles[6];
        int[] solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        return solutions;
    }
    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }
}
