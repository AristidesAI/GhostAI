using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tracks and displays a running timer once the game session begins.
/// This script can be integrated with Build 1.0.1 and expanded in future builds.
/// </summary>
public class GameTimer : MonoBehaviour
{
    [Header("Timer Display Settings")]
    [SerializeField] private Text timerText;
    // UI Text element where the time is displayed. Drag and drop in Inspector.

    [Header("Timer Config")]
    [SerializeField] private bool autoStartOnAwake = false;
    // If true, the timer will start automatically when Awake() is called.
    
    private float currentTime = 0f;  
    private bool isTiming = false;

    void Awake()
    {
        if (autoStartOnAwake)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (isTiming)
        {
            currentTime += Time.deltaTime;
            DisplayTime();
        }
    }

    /// <summary>
    /// Resets and starts the timer from zero.
    /// </summary>
    public void StartTimer()
    {
        currentTime = 0f;
        isTiming = true;
        DisplayTime();
    }

    /// <summary>
    /// Pauses the timer without resetting the current value.
    /// </summary>
    public void PauseTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// Resumes timing from the last recorded value.
    /// </summary>
    public void ResumeTimer()
    {
        isTiming = true;
    }

    /// <summary>
    /// Stops the timer and resets its value to zero.
    /// </summary>
    public void StopTimer()
    {
        isTiming = false;
        currentTime = 0f;
        DisplayTime();
    }

    /// <summary>
    /// Helper method to convert float time to a readable format.
    /// </summary>
    private void DisplayTime()
    {
        // Example: simple seconds format. Adjust for minutes/hours as needed.
        if (timerText != null)
        {
            timerText.text = $"{currentTime:F2} s";
        }
    }
}