using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Pause menu, pausing mechanics, and restart logic.
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    [Header("Pause Menu References")]
    [SerializeField] private CanvasGroup pauseMenuCanvasGroup;
    // Main panel for the pause menu
    
    [SerializeField] private Button pauseButton;
    // Separate button in top-right corner to open/collapse the pause menu

    private bool isPaused = false;
    private float previousTimeScale = 1f;

    private void Start()
    {
        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0f;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

        // Ensure pauseButton is assigned and set its onClick listener
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
    }

    /// <summary>
    /// Toggles the pause state. Called when the Pause button is clicked.
    /// </summary>
    public void TogglePause()
    {
        if (!isPaused)
        {
            // Pause the game
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isPaused = true;
            ShowPauseMenu();
        }
        else
        {
            // Unpause the game
            Time.timeScale = previousTimeScale;
            isPaused = false;
            HidePauseMenu();
        }
    }

    /// <summary> Show the pause menu canvas group. </summary>
    private void ShowPauseMenu()
    {
        if (pauseMenuCanvasGroup)
        {
            pauseMenuCanvasGroup.alpha = 1f;
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }
    }

    /// <summary> Hide the pause menu canvas group. </summary>
    private void HidePauseMenu()
    {
        if (pauseMenuCanvasGroup)
        {
            pauseMenuCanvasGroup.alpha = 0f;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }
    }

    /// <summary>
    /// Called by Pause Menu "Continue" button to resume.
    /// </summary>
    public void OnClickContinue()
    {
        TogglePause();
    }

    /// <summary>
    /// Called by Pause Menu "Restart" button to go back to main menu or reload scene.
    /// </summary>
    public void OnClickRestart()
    {
        // Option 1: Reload current scene from start
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}