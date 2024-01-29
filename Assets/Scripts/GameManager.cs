using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }
    //public UIManager UIManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        
        //UIManager = GetComponent<UIManager>();
    }

    private void StopGame()
    {
        //UIManager.StopGame();
    }

    public void StartGame()
    {
        //UIManager.StartGame();
    }
}