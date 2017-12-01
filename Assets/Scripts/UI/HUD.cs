using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text playerHP;

    [SerializeField]
    GameObject gameOverScreen;

    public void SetPlayerHP(int hp)
    {
        playerHP.text = Mathf.Max(hp, 0).ToString();
    }

    public void DisplayGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void OnRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
