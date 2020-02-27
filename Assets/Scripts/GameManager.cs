using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameObject gameMenu;
    public GameUIManager gameUIManager;


    // With public instance GameManger can be referenced from anywhere.
    public GameState gameState;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Gamemanager implements singleton pattern.
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Two GameManagers, destroying the other.");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    private void OnEnable()
    {
        gameState = GameState.Game;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        if (!gameUIManager && mode == LoadSceneMode.Single)
        {
            Debug.Log("GameUI loaded");
            SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
        }
    }

}
