using System.Collections;
using System.Collections.Generic;
using Malimbe.PropertySerializationAttribute;
using UnityEngine;

public class _PlaySessionManager : MonoBehaviour
{
    public enum GameStates { Idle, Starting, Running, Paused, GameOver, Count }
    [Serialized] public GameStates GameState { get; private set; }
    [Serialized] public List<PlayerController> ActivePlayers { get; set; }
    [Serialized] public GameMode CurrGameMode { get; private set; }
    
    private delegate void UpdateDelegate();
    private UpdateDelegate[] _updateDelegates;

    private void Awake()
    {
        _updateDelegates = new UpdateDelegate[(int)GameStates.Count];
        _updateDelegates[(int) GameStates.Starting] = OnGameStart;
        _updateDelegates[(int) GameStates.Running] = UpdateGame;
        _updateDelegates[(int) GameStates.Paused] = PausedGame;
        _updateDelegates[(int) GameStates.GameOver] = OnEndGame;
        GameState = GameStates.Idle;
        ActivePlayers = new List<PlayerController>();
    }

    private void Update()
    {
        if (_updateDelegates[(int)GameState] != null)
            _updateDelegates[(int)GameState]();
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
    }
    
    private void FindGameMode()
    {
        CurrGameMode = GameObject.FindObjectOfType<GameMode>();
    }

    private void OnGameStart()
    {
        CurrGameMode = null;
        CurrGameMode = GameObject.FindObjectOfType<GameMode>();
        
        if (CurrGameMode == null)
            return;
            
        if (ActivePlayers.Count <= CurrGameMode.PlayerLimit)
        {
            CurrGameMode.GameStarted();
            Debug.Log("Game Started");
            GameState = GameStates.Running;
        }
    }

    private void UpdateGame()
    {
        var isGameOver = CurrGameMode.GameOverCondition();
        
//        TODO: Implement Timer
//        Update game timer
        
        if (isGameOver)
            GameState = GameStates.GameOver;
    }

    private void PausedGame()
    {
        
    }
    
    private void OnEndGame()
    {
//        ZERAR TIMER
        CurrGameMode.GameOver();
    }

    public void StartGame()
    {
        GameState = GameStates.Starting;
    }
    
    public void TriggerPause()
    {
        if (GameState == GameStates.Running)
        {
            GameState = GameStates.Paused;
            CurrGameMode.GamePaused();
        }
        else
        {
            GameState = GameStates.Running;
        }
    }
}
