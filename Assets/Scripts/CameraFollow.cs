using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Permet à la caméra de suivre la cible
    [Header("Camera Parameters")]
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 _offset;
    private Vector3 _targetPos;

    private void Start()
    {
        if (!target) return;

        _offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        _targetPos = target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _targetPos, lerpSpeed * Time.deltaTime);
    }
}