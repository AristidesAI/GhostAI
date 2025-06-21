using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Central game manager that handles game states, pausing, and tagging mechanics.
/// Acts as the primary controller for the game's flow and rules.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    [Tooltip("Current game state")]
    [SerializeField] private GameState currentGameState = GameState.Playing;
    
    [Header("Player References")]
    [Tooltip("Reference to the player controller")]
    [SerializeField] private PlayerController playerController;
    
    [Header("AI References")]
    [Tooltip("List of AI characters in the game")]
    [SerializeField] private List<AIController> aiControllers = new List<AIController>();
    
    [Header("UI References")]
    [Tooltip("Reference to the pause panel UI")]
    [SerializeField] private GameObject pausePanel;
    [Tooltip("Reference to the tag button UI")]
    [SerializeField] private GameObject tagButtonObject;
    [Tooltip("CanvasGroup for tag button to control opacity")]
    [SerializeField] private CanvasGroup tagButtonCanvasGroup;
    [Tooltip("Reference to the game over panel UI")]
    [SerializeField] private GameObject gameOverPanel;
    [Tooltip("Reference to the blur effect for pause screen")]
    [SerializeField] private GameObject blurOverlay;
    [Tooltip("Reference to the tag effect particle system")]
    [SerializeField] private GameObject tagEffectPrefab;
    
    [Header("Game Timer")]
    [Tooltip("Reference to the game timer")]
    [SerializeField] private GameTimer gameTimer;
    
    [Header("Tag Settings")]
    [Tooltip("Range within which the player can tag AI")]
    [SerializeField] private float tagRange = 2.0f;
    [Tooltip("Alpha value for tag button when in range")]
    [Range(0, 1)]
    [SerializeField] private float tagButtonActiveAlpha = 1.0f;
    [Tooltip("Alpha value for tag button when out of range")]
    [Range(0, 1)]
    [SerializeField] private float tagButtonInactiveAlpha = 0.3f;
    
    [Header("Camera Settings")]
    [Tooltip("Reference to the camera controller")]
    [SerializeField] private CameraController cameraController;
    
    // Private variables
    private int remainingAI = 0;
    private AIController targetAI = null;
    
    /// <summary>
    /// Game state enum to track the current state of the game
    /// </summary>
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }
    
    void Start()
    {
        // Initialize the game
        SetGameState(GameState.Playing);
        UpdateRemainingAI();
        
        // Ensure UI is properly initialized
        if (pausePanel != null) pausePanel.SetActive(false);
        if (blurOverlay != null) blurOverlay.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        
        // Setup tag button
        UpdateTagButtonState(false);
    }
    
    void Update()
    {
        // Handle escape key for pausing/unpausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
        if (currentGameState == GameState.Playing)
        {
            // Check for tagging potential
            CheckTaggingDistance();
            
            // Check for game over condition
            if (remainingAI <= 0 && currentGameState != GameState.GameOver)
            {
                SetGameState(GameState.GameOver);
            }
        }
    }
    
    /// <summary>
    /// Toggles the game between paused and playing states
    /// </summary>
    public void TogglePause()
    {
        if (currentGameState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
        }
        else if (currentGameState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
        }
    }
    
    /// <summary>
    /// Sets the game to a specific state and updates the UI and gameplay accordingly
    /// </summary>
    /// <param name="newState">The new game state</param>
    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
        
        switch (currentGameState)
        {
            case GameState.Playing:
                // Resume game
                Time.timeScale = 1f;
                if (pausePanel != null) pausePanel.SetActive(false);
                if (blurOverlay != null) blurOverlay.SetActive(false);
                if (gameTimer != null) gameTimer.ResumeTimer();
                break;
                
            case GameState.Paused:
                // Pause game
                Time.timeScale = 0f;
                if (pausePanel != null) pausePanel.SetActive(true);
                if (blurOverlay != null) blurOverlay.SetActive(true);
                if (gameTimer != null) gameTimer.PauseTimer();
                break;
                
            case GameState.GameOver:
                // Game over
                Time.timeScale = 0f;
                if (gameOverPanel != null) gameOverPanel.SetActive(true);
                if (blurOverlay != null) blurOverlay.SetActive(true);
                if (gameTimer != null) gameTimer.PauseTimer();
                break;
        }
    }
    
    /// <summary>
    /// Restarts the game by resetting all game elements
    /// </summary>
    public void RestartGame()
    {
        // Reset timer
        if (gameTimer != null) gameTimer.StartTimer();
        
        // Reset player position if needed
        if (playerController != null)
        {
            playerController.transform.position = Vector3.zero;
            playerController.rb.linearVelocity = Vector2.zero;
        }
        
        // Reactivate all AI
        foreach (AIController ai in aiControllers)
        {
            if (ai != null)
            {
                ai.gameObject.SetActive(true);
            }
        }
        
        // Update remaining AI count
        UpdateRemainingAI();
        
        // Reset game state
        SetGameState(GameState.Playing);
        
        // Hide game over panel
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (blurOverlay != null) blurOverlay.SetActive(false);
    }
    
    /// <summary>
    /// Checks if the player is close enough to any AI to tag them
    /// </summary>
    private void CheckTaggingDistance()
    {
        targetAI = null;
        float closestDistance = float.MaxValue;
        
        // Skip if no player controller
        if (playerController == null) return;
        
        foreach (AIController ai in aiControllers)
        {
            // Skip inactive AI
            if (ai == null || !ai.gameObject.activeInHierarchy) continue;
            
            // Calculate distance
            float distance = Vector2.Distance(playerController.transform.position, ai.transform.position);
            
            // Check if this AI is in range and closer than other candidates
            if (distance <= tagRange && distance < closestDistance)
            {
                closestDistance = distance;
                targetAI = ai;
            }
        }
        
        // Update tag button based on whether there's a target in range
        UpdateTagButtonState(targetAI != null);
    }
    
    /// <summary>
    /// Updates the tag button's visibility and interactability based on whether there's a valid target
    /// </summary>
    /// <param name="hasTarget">Whether there's a target in range</param>
    private void UpdateTagButtonState(bool hasTarget)
    {
        if (tagButtonObject == null || tagButtonCanvasGroup == null) return;
        
        // Always keep the button object active for layout purposes
        tagButtonObject.SetActive(true);
        
        // Set alpha based on whether there's a target in range
        tagButtonCanvasGroup.alpha = hasTarget ? tagButtonActiveAlpha : tagButtonInactiveAlpha;
        
        // Only allow interaction when there's a target
        tagButtonCanvasGroup.interactable = hasTarget;
        tagButtonCanvasGroup.blocksRaycasts = hasTarget;
    }
    
    /// <summary>
    /// Tags the current target AI, removing them from the game
    /// </summary>
    public void TagTarget()
    {
        if (targetAI == null || currentGameState != GameState.Playing) return;
        
        // Spawn tag effect
        if (tagEffectPrefab != null)
        {
            Instantiate(tagEffectPrefab, targetAI.transform.position, Quaternion.identity);
        }
        
        // Remove the AI from camera tracking
        if (cameraController != null)
        {
            cameraController.RemoveAITarget(targetAI.transform);
        }
        
        // Deactivate the AI
        targetAI.gameObject.SetActive(false);
        
        // Update remaining AI count
        UpdateRemainingAI();
    }
    
    /// <summary>
    /// Updates the count of remaining active AI
    /// </summary>
    private void UpdateRemainingAI()
    {
        remainingAI = 0;
        foreach (AIController ai in aiControllers)
        {
            if (ai != null && ai.gameObject.activeInHierarchy)
            {
                remainingAI++;
                
                // Ensure camera is tracking this AI
                if (cameraController != null)
                {
                    cameraController.AddAITarget(ai.transform);
                }
            }
        }
    }
}
