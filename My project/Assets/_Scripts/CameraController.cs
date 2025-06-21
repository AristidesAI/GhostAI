using UnityEngine;

/// <summary>
/// Creates a 2.5D camera that smoothly follows a target (the player).
/// It uses a perspective camera and maintains a fixed offset for an angled top-down view.
/// </summary>
public class CameraController : MonoBehaviour
{
    // ==============
    // PUBLIC VARIABLES (CONFIGURABLE IN INSPECTOR)
    // ==============

    [Header("Targeting")]
    [Tooltip("The player's transform, which the camera will follow.")]
    public Transform target;

    [Header("Camera Behavior")]
    [Tooltip("The positional offset from the target (e.g., X=0, Y= -25, Z=-10). This defines the camera angle and distance.")]
    public Vector3 offset = new Vector3(0, 5, -10);
    
    [Tooltip("The speed at which the camera follows the player. Lower values are smoother.")]
    [Range(0.01f, 2.0f)]
    public float smoothSpeed = 0.125f;


    // ==============
    // UNITY LIFECYCLE METHODS
    // ==============

    /// <summary>
    /// LateUpdate is called after all Update functions have been called. This is the best place
    /// to handle camera movement to ensure the target has completed its movement for the frame.
    /// </summary>
    void LateUpdate()
    {
        // If there's no target, do nothing.
        if (target == null)
        {
            Debug.LogWarning("Camera Controller does not have a target assigned.");
            return;
        }

        // --- Camera Movement ---
        // Calculate the desired position for the camera by adding the offset to the target's position.
        Vector3 desiredPosition = target.position + offset;
        
        // Use Vector3.Lerp to smoothly interpolate from the camera's current position to the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Apply the new position to the camera.
        transform.position = smoothedPosition;
    }
}