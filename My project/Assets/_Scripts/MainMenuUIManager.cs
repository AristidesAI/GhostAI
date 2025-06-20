using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Main Menu UI and handles color selection for the player.
/// </summary>
public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu References")]
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    // Canvas Group for the main menu (buttons, title, background, etc.).
    
    [SerializeField] private GameObject gameplayUI;
    // Parent object or Canvas that holds gameplay UI elements (joystick, pause button, etc.).
    
    [Header("Player Color Settings")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    // Reference to the player sprite renderer for color or sprite-swapping.

    [SerializeField] private Sprite[] colorVariants;
    // Array of player sprite variants in different colors. Use identical sprite shapes with different tints.

    private int currentColorIndex = 0;

    private void Awake()
    {
        // Initialize UI: show main menu, hide gameplay UI
        ShowMainMenu();
    }

    /// <summary> Called by the "Start" button. </summary>
    public void OnStartGame()
    {
        // Hide main menu and show gameplay UI elements
        HideMainMenu();
        if (gameplayUI != null)
        {
            gameplayUI.SetActive(true);
        }
    }

    /// <summary> Called by the "Color" button to cycle the player's color. </summary>
    public void OnColorButton()
    {
        if (colorVariants == null || colorVariants.Length == 0) return;

        // Cycle index and update sprite
        currentColorIndex = (currentColorIndex + 1) % colorVariants.Length;
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.sprite = colorVariants[currentColorIndex];
        }
    }

    /// <summary> Shows the main menu and hides gameplay UI (on scene load or restart). </summary>
    private void ShowMainMenu()
    {
        if (mainMenuCanvasGroup != null)
        {
            mainMenuCanvasGroup.alpha = 1f;
            mainMenuCanvasGroup.interactable = true;
            mainMenuCanvasGroup.blocksRaycasts = true;
        }

        if (gameplayUI != null)
        {
            gameplayUI.SetActive(false);
        }
    }

    /// <summary> Hides the main menu (once the game starts). </summary>
    private void HideMainMenu()
    {
        if (mainMenuCanvasGroup != null)
        {
            mainMenuCanvasGroup.alpha = 0f;
            mainMenuCanvasGroup.interactable = false;
            mainMenuCanvasGroup.blocksRaycasts = false;
        }
    }
}