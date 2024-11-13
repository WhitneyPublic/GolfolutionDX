using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the Ball GameObject
    public float distance = 10f; // Distance from the target
    public float height = 5f; // Height above the target
    public float rotationSpeed = 50f; // Speed of rotation using arrow keys
    public float minVerticalAngle = 0f; // Minimum vertical angle (horizontal plane)
    public float maxVerticalAngle = 60f; // Maximum vertical angle

    private float currentVerticalAngle = 30f; // Default starting vertical angle
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

        // Apply height adjustment and position
        transform.position = target.position + offset + Vector3.up * height;
        transform.LookAt(target);
    }
}

