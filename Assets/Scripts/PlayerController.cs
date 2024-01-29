using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Layers")] [Range(1f, 100f)] public float moveSpeed = 30f;
    [Range(1f, 3f)] public float forceDamping = 1.2f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private Vector2 _forceToApply;
    private Vector2 _playerInput;

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
        // On récupère les entrées utilisateur
        _playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _animator.SetBool(IsMoving, _playerInput.magnitude > 0);
    }

    private void FixedUpdate()
    {
        var moveForce = CalculateMoveForce();
        _rb.AddForce(moveForce);

        if (_rb.simulated)
        {
            UpdateDirectionAnimation(moveForce);
        }
    }

    private Vector2 CalculateMoveForce()
    {
        var moveForce = _playerInput * moveSpeed;
        moveForce += _forceToApply;
        _forceToApply /= forceDamping;
        if (Mathf.Abs(_forceToApply.x) <= 0.01f && Mathf.Abs(_forceToApply.y) <= 0.01f)
        {
            _forceToApply = Vector2.zero;
        }

        return moveForce;
    }

    private void UpdateDirectionAnimation(Vector2 movement)
    {
        // On flip l'asset selon la direction du déplacement
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