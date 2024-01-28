using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    [Range(1f, 100f)] public float moveSpeed = 30f;
    public Vector2 forceToApply;
    public Vector2 playerInput;
    [Range(1f, 3f)] public float forceDamping = 1.2f;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
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

        _rb.AddForce(moveForce);
        
        if (_rb.simulated)
        {
            UpdateDirectionAnimation(moveForce);
        }
    }

    private void UpdateDirectionAnimation(Vector2 movement)
    {
        switch (movement.x)
        {
            case > 0:
                _sr.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
                break;
            case < 0:
                _sr.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
                break;
        }
    }

    private void OnFire()
    {
        _animator.SetTrigger(Attack);
    }
}