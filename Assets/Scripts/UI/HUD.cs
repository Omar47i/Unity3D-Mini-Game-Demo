using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text playerHP;

    [SerializeField]
    Text coinsText;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    GameObject gameCompletedScreen;

    public void SetPlayerHP(int hp)
    {
        playerHP.text = Mathf.Max(hp, 0).ToString();
    }

    public void SetCoins(int value)
    {
        coinsText.text = value.ToString();
    }

    public void DisplayGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void DisplayGameCompleted()
    {
        gameCompletedScreen.SetActive(true);
    }

    public void OnRestart()
    {
        GameController.gameState = GameController.GameState.Playing;

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
