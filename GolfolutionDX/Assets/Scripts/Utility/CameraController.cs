using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the Ball GameObject
    public float distance = 10f; // Distance from the target
    public float heightOffset = 2f; // Offset to keep the ball below the center of the screen
    public float rotationSpeed = 50f; // Speed of rotation using arrow keys
    public float minVerticalAngle = 0f; // Minimum vertical angle (slightly above the horizon)
    public float maxVerticalAngle = 60f; // Maximum vertical angle

    private float currentVerticalAngle = 20f; // Default starting vertical angle
    private float currentHorizontalAngle = 0f; // Track horizontal rotation

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set. Please assign the Ball GameObject to the target field.");
            return;
        }

        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get input for camera rotation
        float horizontalInput = Input.GetAxis("Horizontal"); // Left/Right arrow keys
        float verticalInput = Input.GetAxis("Vertical"); // Up/Down arrow keys

        // Update horizontal rotation
        currentHorizontalAngle += horizontalInput * rotationSpeed * Time.deltaTime;

        // Update vertical rotation (clamped)
        currentVerticalAngle = Mathf.Clamp(
            currentVerticalAngle - verticalInput * rotationSpeed * Time.deltaTime,
            minVerticalAngle,
            maxVerticalAngle
        );

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        // Calculate new offset based on angles
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Apply position with a height offset to keep the ball below the center
        transform.position = target.position + offset + Vector3.up * heightOffset;

        // Look at a point slightly above the target to center the view properly
        Vector3 lookAtPosition = target.position + Vector3.up * heightOffset / 2f;
        transform.LookAt(lookAtPosition);
    }
}


