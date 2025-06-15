using UnityEngine;

/// <summary>
/// Controls player animations and flips the sprite.
/// Now includes logic to vary the animation speed based on the character's velocity.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimation : MonoBehaviour
{
    [Header("Component References")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("Animation Speed")]
    [Tooltip("The playback speed of the run animation when at minimum speed.")]
    public float minRunAnimationSpeed = 0.5f;
    [Tooltip("The playback speed of the run animation when at maximum speed.")]
    public float maxRunAnimationSpeed = 1.5f;

    // Private variables
    private readonly int isRunningHash = Animator.StringToHash("isRunning");
    private PlayerController playerController;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        
        // Get a reference to the PlayerController to know the max speed and get input data
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateAnimationState();
        FlipSprite();
    }

    /// <summary>
    /// Sets the "isRunning" parameter and adjusts the animation speed based on velocity.
    /// </summary>
    private void UpdateAnimationState()
    {
        // Use a small threshold to determine if the player is considered "moving"
        bool isMoving = rb.linearVelocity.sqrMagnitude > 0.1f;
        animator.SetBool(isRunningHash, isMoving);

        if (isMoving)
        {
            // Calculate current speed as a percentage of max speed (0 to 1)
            float speedPercent = rb.linearVelocity.magnitude / playerController.maxSpeed;

            // Use the percentage to set the animation speed within the defined min/max range
            animator.speed = Mathf.Lerp(minRunAnimationSpeed, maxRunAnimationSpeed, speedPercent);
        }
        else
        {
            // When idle, reset the animation speed to normal
            animator.speed = 1f;
        }
    }

    /// <summary>
    /// Flips the sprite based on the player's direct horizontal input.
    /// </summary>
    private void FlipSprite()
    {
        // Only execute flip logic if the animator is in the running state.
        if (animator.GetBool(isRunningHash))
        {
            // **THE FIX**: Base the flip on direct input, not velocity, to avoid flickering.
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            // Prioritize joystick input if it is being used
            if (Mathf.Abs(playerController.joystickInputDirection.x) > 0.1f)
            {
                horizontalInput = playerController.joystickInputDirection.x;
            }

            // Only flip the sprite if there is significant horizontal input.
            // This prevents the sprite from changing direction during pure vertical movement.
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                spriteRenderer.flipX = horizontalInput < 0;
            }
        }
    }
}
