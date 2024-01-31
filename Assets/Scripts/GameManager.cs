using UnityEngine;

[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public InventoryManager InventoryManager { get; private set; }
    public UIManager UIManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);

        CameraManager = GetComponent<CameraManager>();
        InventoryManager = GetComponent<InventoryManager>();
        UIManager = GetComponent<UIManager>();
    }

    public void StartGame()
    {
        CameraManager.StartGame();
        UIManager.StartGame();
        InventoryManager.StartGame();
    }

    public void StopGame()
    {
        CameraManager.StopGame();
        UIManager.StopGame();
        InventoryManager.StopGame();
    }
}