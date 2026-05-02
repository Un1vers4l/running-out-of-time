using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [Header("Input References")]
    public InputActionReference moveAction;
    [Header("Audio")]
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioSource audioSource;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _moveVector;

    private readonly string _walkingAnimationTriggerName = "isWalking";
    private bool _canPlayerMove = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        DialogueManager.OnDialogueStarted += DisableMovement;
        DialogueManager.OnDialogueEnded += EnableMovement;

        EnableMovement();
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStarted -= DisableMovement;
        DialogueManager.OnDialogueEnded -= EnableMovement;

        DisableMovement();
    }

    private void Update()
    {
        if (!_canPlayerMove) return;

        _moveVector = GameManager.Instance.State == GameState.Playing ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        UpdateSpriteDirectionAndAnimation();
    }

    private void FixedUpdate()
    {
        if (!_canPlayerMove) return;

        _rigidBody.MovePosition(_rigidBody.position + moveSpeed * Time.fixedDeltaTime * _moveVector);
    }

    private void UpdateSpriteDirectionAndAnimation()
    {
        if (_moveVector == Vector2.zero)
        {
            _animator.SetBool(_walkingAnimationTriggerName, false);
            StopWalkingSound();
            return;
        }

        _animator.SetBool(_walkingAnimationTriggerName, true);
        PlayWalkingSound();

        if (_moveVector.x != 0)
        {
            _spriteRenderer.flipX = _moveVector.x < 0;
        }
    }

    private void PlayWalkingSound()
    {
        if (walkingSound == null || audioSource == null) return;
        if (audioSource.isPlaying) return;

        audioSource.clip = walkingSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void StopWalkingSound()
    {
        if (audioSource == null) return;
        audioSource.Stop();
    }

    private void EnableMovement()
    {
        moveAction.action.Enable();
        _canPlayerMove = true;
    }

    private void DisableMovement()
    {
        moveAction.action.Disable();
        _canPlayerMove = false;
    }
}