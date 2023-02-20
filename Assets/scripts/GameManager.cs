using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChange;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Game);
    }
    
    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Game:
                break;
            case GameState.End:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        State = newState;
        OnGameStateChange?.Invoke(newState);
    }
}

public enum GameState {
    Game, 
    End
}