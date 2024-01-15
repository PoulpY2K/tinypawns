using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private string _currentLayer;
    private int _currentLevel;
    [Range(1f, 10f)] public float moveSpeed = 3f;
    public Vector2 forceToApply;
    public Vector2 playerInput;
    [Range(1f, 3f)] public float forceDamping = 1.2f;

    private const string SortingLayerBaseName = "Level";

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentLayer = _spriteRenderer.sortingLayerName;
        _currentLevel = int.Parse(new string(_currentLayer.Where(char.IsDigit).ToArray()));
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

        _rigidbody2D.velocity = moveForce;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Elevator")) return;
        Debug.Log(col.gameObject.GetComponent<SortingGroup>().name);

        if (col.gameObject.GetComponent<SortingGroup>().sortingLayerName == _currentLayer)
        {
            _spriteRenderer.sortingLayerName = SortingLayerBaseName + (_currentLevel + 1);
            Debug.Log(_spriteRenderer.sortingLayerName);
            gameObject.layer = LayerMask.NameToLayer(_spriteRenderer.sortingLayerName);
            _currentLevel++;
        }
        else
        {
            _spriteRenderer.sortingLayerName = SortingLayerBaseName + (_currentLevel - 1);
            gameObject.layer = LayerMask.NameToLayer(_spriteRenderer.sortingLayerName);
            _currentLevel--;
        }

        _currentLayer = _spriteRenderer.sortingLayerName;
    }
}