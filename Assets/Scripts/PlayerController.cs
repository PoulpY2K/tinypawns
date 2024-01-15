using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [Range(1f, 10f)] public float moveSpeed = 3f;
    public Vector2 forceToApply;
    public Vector2 playerInput;
    [Range(1f, 3f)] public float forceDamping = 1.2f;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _animator.SetBool(IsMoving, playerInput.magnitude > 0);
    }

    private void FixedUpdate()
    {
        var moveForce = playerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }

        _rb.velocity = moveForce;
    }
}