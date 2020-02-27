using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager instance;
    public GameObject winPanel;
    public bool menuOn = false;

    // With public instance GameUIManager can be referenced from anywhere.
    public static GameUIManager Instance
    {
        get
        {
            return instance;
        }
    }

    // GameUIManager implements singleton pattern.
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Two GameUIManagers, destroying the other.");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameManager.Instance.gameUIManager = this;
    }

    //Toggles WinPanel on/off
    // TODO: Change to Menupanel
    public void ToggleWinPanel()
    {
        //menuOn = true;
        Debug.Log("Game paused");
        //Time.timeScale = 0;
        winPanel.SetActive(!winPanel.activeInHierarchy);
        //winPanel.SetActive(true);
    }
}
