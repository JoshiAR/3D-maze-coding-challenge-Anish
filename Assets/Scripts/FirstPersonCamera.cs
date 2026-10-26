using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerInput))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField, Range(0.01f, 1f)] float mouseSensitivity = 0.15f;
    [SerializeField, Range(1f, 89f)] float pitchLimit = 80f;

    Vector2 lookInput;
    float pitch;
    bool cursorCaptured;

    void Start()
    {
        CaptureCursor();
    }

    void Update()
    {
        if (cursorCaptured)
        {
            // Mouse delta is pixels per input update, not a velocity in units/second.
            transform.Rotate(Vector3.up, lookInput.x * mouseSensitivity, Space.World);
            pitch = Mathf.Clamp(pitch - lookInput.y * mouseSensitivity, -pitchLimit, pitchLimit);
            head.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
        lookInput = Vector2.zero;
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void OnReleaseCursor(InputValue value)
    {
        if (value.isPressed)
        {
            ReleaseCursor();
        }
    }

    void OnCaptureCursor(InputValue value)
    {
        if (value.isPressed && !cursorCaptured)
        {
            CaptureCursor();
        }
    }

    void CaptureCursor()
    {
        lookInput = Vector2.zero;
        cursorCaptured = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ReleaseCursor()
    {
        cursorCaptured = false;
        lookInput = Vector2.zero;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            ReleaseCursor();
        }
    }

    void OnDisable()
    {
        ReleaseCursor();
    }
}
