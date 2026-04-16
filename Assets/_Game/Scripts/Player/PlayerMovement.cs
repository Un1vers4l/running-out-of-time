using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [Header("Input References")]
    public InputActionReference moveAction;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _moveVector;

    private readonly string _walkingAnimationTriggerName = "isWalking";

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        bool isPlaying = GameTimerStateMachine.Instance != null
            && GameTimerStateMachine.Instance.State == GameTimerStateMachine.GameState.Playing;

        _moveVector = isPlaying ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        UpdateSpriteDirectionAndAnimation();
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + moveSpeed * Time.fixedDeltaTime * _moveVector);
    }

    private void UpdateSpriteDirectionAndAnimation()
    {
        if (_moveVector == Vector2.zero)
        {
            _animator.SetBool(_walkingAnimationTriggerName, false);
            return;
        }

        _animator.SetBool(_walkingAnimationTriggerName, true);

        if (_moveVector.x != 0)
        {
            _spriteRenderer.flipX = _moveVector.x < 0;
        }
    }
}