using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        GameOver
    }

    public static GameState gameState = GameState.Playing;
    public static GameController Instance;

    public HUD HUDScript;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}

public class Tags
{
    public const string Player = "Player";
    public const string Enemy = "Enemy";
}