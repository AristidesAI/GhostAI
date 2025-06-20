using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays debug information (e.g., player stats) on a UI panel.
/// Designed to integrate seamlessly with build 1.0.1 of the project.
/// </summary>
public class DebugPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text debugText; 
    // Reference to the Text UI component where debug data is displayed.

    [Header("Player References")]
    [SerializeField] private PlayerController playerController;
    // Reference to the PlayerController to read player data (e.g., velocity).

    // Additional [SerializeField] variables for controlling layout, color, etc. could go here.

    private void Update()
    {
        // Safety check: Ensure we have a valid reference to avoid null errors.
        if (debugText == null || playerController == null)
            return;

        // Example: Display basic position and velocity data for debugging.
        Vector2 velocity = playerController.rb.linearVelocity; 
        Vector2 position = playerController.rb.position;

        string debugInfo = 
            $"Player Position: {position}\n" +
            $"Player Velocity: {velocity}\n" +
            $"Joystick Input: {playerController.joystickInputDirection}";

        // You can extend this to include health, ammo, or other gameplay-related info.
        debugText.text = debugInfo;
    }
}