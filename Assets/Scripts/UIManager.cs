using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gm;
    public GameObject[] hearts;
    public GameObject startButton;
    public GameObject healthBar;

    private void Awake()
    {
        _gm = GameManager.Instance;
    }

    private void Update()
    {
        var player = FindFirstObjectByType<PlayerController>();
        if (!player || !player.TryGetComponent<Damageable>(out var dmg)) return;

        if (dmg.Health < hearts.Length)
        {
            var lastHeart = hearts[hearts.Length - 1];
            hearts = hearts.Take(hearts.Length - 1).ToArray();
            Destroy(lastHeart);
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        healthBar.SetActive(true);
    }

    public void StopGame()
    {
        startButton.SetActive(true);
        healthBar.SetActive(false);
    }
}