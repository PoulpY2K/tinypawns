using Map;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Parameters")] [Range(1f, 100f)]
        public float moveSpeed = 30f;

        [Header("Icons")] public GameObject interactIcon;

        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _sr;
        private Vector2 _forceToApply;
        private Vector2 _playerInput;
        private Weapon _weapon;
        private Vector2 _oldPosition;
        private bool _isInteracting;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Pressed = Animator.StringToHash("Pressed");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sr = GetComponent<SpriteRenderer>();
            _weapon = GetComponentInChildren<Weapon>();
            interactIcon.SetActive(false);
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
            return moveForce;
        }

        private void UpdateDirectionAnimation(Vector2 movement)
        {
            // On flip l'asset selon la direction du déplacement
            switch (movement.x)
            {
                case > 0:
                    _sr.flipX = false;
                    if (_weapon)
                    {
                        _weapon.IsFacingRight(true);
                    }

                    break;
                case < 0:
                    _sr.flipX = true;
                    if (_weapon)
                    {
                        _weapon.IsFacingRight(false);
                    }

                    break;
            }
        }

        private void OnFire()
        {
            _animator.SetTrigger(Attack);
        }

        public void ShowInteractIcon()
        {
            interactIcon.SetActive(true);
        }

        public void HideInteractIcon()
        {
            interactIcon.SetActive(false);
        }

        private void OnInteract()
        {
            var hits = Physics2D.BoxCastAll(transform.position, new Vector2(1f, 0.1f), 0, Vector2.zero);
            if (hits.Length <= 0) return;

            foreach (var hit in hits)
            {
                hit.transform.TryGetComponent<Structure>(out var i);
                if (i && !_isInteracting)
                {
                    if (i.Destroyed)
                        return;

                    interactIcon.GetComponent<Animator>().SetTrigger(Pressed);
                    _sr.enabled = false;
                    _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    _oldPosition = gameObject.transform.position;
                    gameObject.transform.position = i.transform.position;
                    _isInteracting = true;

                    i.Interact();
                    return;
                }

                if (i && _isInteracting)
                {
                    interactIcon.GetComponent<Animator>().SetTrigger(Pressed);
                    _sr.enabled = true;
                    gameObject.transform.position = _oldPosition;
                    _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    _isInteracting = false;

                    i.Interact();
                    if (i.Destroyed)
                    {
                        HideInteractIcon();
                    }

                    return;
                }
            }
        }
    }
}