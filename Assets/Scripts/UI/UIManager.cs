using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Types;

public class UIManager : MonoBehaviour
{
    public void LoadPlayerJoinScene()
    {
        //SceneManager.LoadScene("PlayerJoin");
    }

    public void LoadGameScene()
    {
        GameManager.Instance.gameState = GameState.Game;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
