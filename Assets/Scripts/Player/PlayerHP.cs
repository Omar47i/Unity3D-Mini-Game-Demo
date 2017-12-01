using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    int HP = 100;

    public void InflictDamage(int damage)
    {
        if (GameController.gameState == GameController.GameState.GameOver)
            return;

        HP -= damage;

        GameController.Instance.HUDScript.SetPlayerHP(HP);

        if (HP <= 0)
        {
            GameController.gameState = GameController.GameState.GameOver;

            GameController.Instance.HUDScript.DisplayGameOver();
        }
    }
}
