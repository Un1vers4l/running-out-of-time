using UnityEngine;
using UnityEngine.InputSystem; // Wichtig für das neue Input System
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    [Header("Input References")]
    public InputActionReference moveAction;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        moveInput = moveAction.action.ReadValue<Vector2>();
        // TODO: flip sprite and start walk animation when its clear which sprite we are using (might come with custom animation controller)
        // UpdateAnimationAndDirection();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    // private void UpdateAnimationAndDirection()
    // {
    //     animator.SetFloat("Speed", Mathf.Abs(moveInput.x));

    //     if (moveInput.x > 0)
    //     {
    //         spriteRenderer.flipX = false; // Schaut nach rechts
    //     }
    //     else if (moveInput.x < 0)
    //     {
    //         spriteRenderer.flipX = true;  // Schaut nach links
    //     }
    // }
}