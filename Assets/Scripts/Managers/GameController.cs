using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        GameOver,
        GameCompleted
    }

    [HideInInspector]
    public int coins = 0;      // starting number of coins
    public int totalCoins;     // set from the Grid.cs script

    public static GameState gameState = GameState.Playing;
    public static GameController Instance;

    public HUD HUDScript;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        HUDScript.SetCoins(coins);
    }

    public void AddCoins(int _coins)
    {
        coins += _coins;

        // If player collected all the coins, then he won the game
        if (coins >= totalCoins)
        {
            gameState = GameState.GameCompleted;

            HUDScript.DisplayGameCompleted();
        }

        HUDScript.SetCoins(coins);
    }
}

public class Tags
{
    public const string Player = "Player";
    public const string Turret = "Turret";
}