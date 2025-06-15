using UnityEngine;

/// <summary>
/// Controls non-player characters, making them wander around the map randomly
/// to simulate other players. It handles movement, collision response, and animation.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class AIController : MonoBehaviour
{
    // ==============
    // PUBLIC VARIABLES (CONFIGURABLE IN INSPECTOR)
    // ==============
    
    [Header("AI Behavior")]
    [Tooltip("The movement speed for the AI character.")]
    public float moveSpeed = 3f;
    [Tooltip("A Vector2(min, max) defining the random range of time the AI will walk in one direction before changing.")]
    public Vector2 decisionTimeRange = new Vector2(2.0f, 5.0f);

    [Header("Component References")]
    [Tooltip("Reference to the AI's Animator component. Will be auto-assigned if not set.")]
    public Animator animator;
    [Tooltip("Reference to the AI's SpriteRenderer component. Will be auto-assigned if not set.")]
    public SpriteRenderer spriteRenderer;

    
    // ==============
    // PRIVATE VARIABLES
    // ==============

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float decisionTimer;
    private readonly int isRunningHash = Animator.StringToHash("isRunning");


    // ==============
    // UNITY LIFECYCLE METHODS
    // ==============
    
    void Awake()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // Set physics properties
        rb.freezeRotation = true;
        rb.gravityScale = 0;
    }

    void Start()
    {
        // Start with an initial decision
        ChooseNewDirection();
    }

    void Update()
    {
        // Countdown the timer for the next decision
        decisionTimer -= Time.deltaTime;
        if (decisionTimer <= 0)
        {
            ChooseNewDirection();
        }

        UpdateAnimationState();
        FlipSprite();
    }

    void FixedUpdate()
    {
        // Apply movement in FixedUpdate for physics consistency
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    /// <summary>
    /// Called when the Collider2D other enters the trigger.
    /// This is used to detect collisions with walls.
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the AI collides with anything, force it to choose a new direction immediately.
        // This helps prevent it from getting stuck on walls.
        ChooseNewDirection();
    }


    // ==============
    // CUSTOM METHODS
    // ==============

    /// <summary>
    /// Chooses a new random direction and sets a timer for the next change.
    /// </summary>
    private void ChooseNewDirection()
    {
        // Pick a random angle and convert it to a direction vector
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;

        // Reset the timer with a new random duration
        decisionTimer = Random.Range(decisionTimeRange.x, decisionTimeRange.y);
    }
    
    /// <summary>
    /// Updates the animator based on whether the AI is moving.
    /// </summary>
    private void UpdateAnimationState()
    {
        bool isMoving = moveDirection != Vector2.zero;
        animator.SetBool(isRunningHash, isMoving);
    }

    /// <summary>
    /// Flips the sprite based on horizontal movement direction.
    /// </summary>
    private void FlipSprite()
    {
        if (Mathf.Abs(moveDirection.x) > 0.01f)
        {
            spriteRenderer.flipX = moveDirection.x < 0;
        }
    }
}
