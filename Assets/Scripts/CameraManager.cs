using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Permet à la caméra de suivre la cible
    [Header("Camera Parameters")] public Camera mainCamera;
    public Transform target;
    public float lerpSpeed = 1.0f;

    private float _offsetX;
    private float _offsetY;
    private Vector3 _targetPos;
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

    private void Start()
    {
        if (!target || !_running) return;

        var camPos = mainCamera.transform.position;
        var targetPos = target.position;
        _offsetX = camPos.x - targetPos.x;
        _offsetY = camPos.y - targetPos.y;
    }

    private void FixedUpdate()
    {
        if (!target || !_running) return;

        var targetPos = target.position;
        var camPos = mainCamera.transform.position;
        _targetPos = new Vector3(targetPos.x + _offsetX, targetPos.y + _offsetY, camPos.z);

        mainCamera.transform.position =
            Vector3.Lerp(camPos, _targetPos, lerpSpeed * Time.deltaTime);
    }
}