using UnityEngine;

/// <summary>
/// Manages player character movement with acceleration, friction, and analog input.
/// This script uses Rigidbody.AddForce for a more physical feel.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    // ==============
    // PUBLIC VARIABLES (CONFIGURABLE IN INSPECTOR)
    // ==============

    [Header("Movement Physics")]
    [Tooltip("The maximum speed the player can reach.")]
    public float maxSpeed = 7f;
    [Tooltip("How quickly the player accelerates to max speed. Higher values are faster.")]
    public float acceleration = 80f;
    [Tooltip("How quickly the player stops when there is no input. Higher values mean more friction.")]
    public float deceleration = 100f;

    [Header("Component References")]
    [Tooltip("Reference to the player's Rigidbody2D component.")]
    public Rigidbody2D rb;
    [Tooltip("Reference to the player's Animator component.")]
    public Animator animator;

    // This Vector2 is controlled by the VirtualJoystick.cs script. 
    // It now receives a vector with magnitude, not just a normalized direction.
    [HideInInspector]
    public Vector2 joystickInputDirection;


    // ==============
    // PRIVATE VARIABLES
    // ==============
    
    private Vector2 finalMoveDirection;


    // ==============
    // UNITY LIFECYCLE METHODS
    // ==============

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();

        rb.freezeRotation = true;
        rb.gravityScale = 0;
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }


    // ==============
    // CUSTOM METHODS
    // ==============

    /// <summary>
    /// Gathers input from keyboard and joystick.
    /// </summary>
    private void ProcessInputs()
    {
        Vector2 keyboardInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Prioritize joystick input if it's being used.
        if (joystickInputDirection.sqrMagnitude > 0)
        {
            // The joystick provides both direction and magnitude (how far it's tilted)
            finalMoveDirection = joystickInputDirection;
        }
        else
        {
            // Keyboard input is digital, so we normalize it to get consistent direction.
            finalMoveDirection = keyboardInput.normalized;
        }
    }

    /// <summary>
    /// Applies forces to the Rigidbody for acceleration and deceleration.
    /// </summary>
    private void MovePlayer()
    {
        // Calculate the target velocity based on input direction and maxSpeed
        Vector2 targetVelocity = finalMoveDirection * maxSpeed;

        // Calculate the difference between current velocity and target velocity
        Vector2 velocityDifference = targetVelocity - rb.linearVelocity;
        
        // Determine which force to use: acceleration or deceleration
        float force = (finalMoveDirection.magnitude > 0) ? acceleration : deceleration;

        // Apply the force to accelerate/decelerate the player
        // This creates the feeling of momentum and friction
        rb.AddForce(velocityDifference * force * Time.fixedDeltaTime, ForceMode2D.Impulse);

        // To prevent sliding forever, we can cap the maximum velocity
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
