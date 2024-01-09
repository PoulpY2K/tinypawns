using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    [Range(1f, 10f)] public float moveSpeed = 3f;
    public Vector2 forceToApply;
    public Vector2 playerInput;
    [Range(1f, 3f)] public float forceDamping = 1.2f;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _animator.SetBool(IsMoving, playerInput.magnitude > 0);
    }

    private void FixedUpdate()
    {
        Vector2 moveForce = playerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }

        _rigidbody2D.velocity = moveForce;
    }
}