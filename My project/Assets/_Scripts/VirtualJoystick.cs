using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Creates a virtual joystick that outputs an analog vector (with magnitude).
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("UI References")]
    public RectTransform joystickBase;
    public RectTransform joystickKnob;

    [Header("Control Setup")]
    public PlayerController playerController;
    public float joystickRadius = 100f;
    [Range(0, 1)]
    public float deadZone = 0.1f;

    [Header("Appearance")]
    [Range(0, 1)] 
    public float activeAlpha = 1f;
    [Range(0, 1)] 
    public float inactiveAlpha = 0.3f;

    private CanvasGroup canvasGroup;
    private Vector2 knobStartPosition;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        knobStartPosition = joystickKnob.anchoredPosition;
        SetJoystickAlpha(inactiveAlpha);
        if (playerController == null) { Debug.LogWarning("PlayerController not assigned."); }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetJoystickAlpha(activeAlpha);
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBase, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            joystickKnob.anchoredPosition = Vector2.ClampMagnitude(localPoint, joystickRadius);
            
            // This is the raw input vector, its magnitude represents the "tilt amount"
            Vector2 inputVector = joystickKnob.anchoredPosition / joystickRadius;

            if (inputVector.magnitude < deadZone)
            {
                inputVector = Vector2.zero;
            }
            
            if (playerController != null)
            {
                // **THE FIX**: Send the vector with its magnitude, NOT the normalized version.
                playerController.joystickInputDirection = inputVector;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetJoystickAlpha(inactiveAlpha);
        joystickKnob.anchoredPosition = knobStartPosition;
        
        if (playerController != null)
        {
            playerController.joystickInputDirection = Vector2.zero;
        }
    }

    private void SetJoystickAlpha(float alpha)
    {
        if (canvasGroup != null) { canvasGroup.alpha = alpha; }
    }
}
