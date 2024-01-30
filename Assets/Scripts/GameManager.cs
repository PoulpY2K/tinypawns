using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public InventoryManager InventoryManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);

        CameraManager = GetComponent<CameraManager>();
        InventoryManager = GetComponent<InventoryManager>();
    }

    private void Start()
    {
        CameraManager.StartGame();
        InventoryManager.StartGame();
    }

    public void Stop()
    {
        CameraManager.StopGame();
        InventoryManager.StopGame();
    }
}