using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameManager _gm;
    private bool _running = false;

    private void Awake()
    {
        _gm = GameManager.Instance;
    }

    public void StartGame()
    {
        _running = true;
    }

    public void StopGame()
    {
        _running = false;
    }
}
