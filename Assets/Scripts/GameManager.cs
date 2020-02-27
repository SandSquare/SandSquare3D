using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    // With public instance GameManger can be referenced from anywhere.
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
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
