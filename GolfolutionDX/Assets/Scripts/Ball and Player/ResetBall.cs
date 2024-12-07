using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ResetBall : MonoBehaviour
{
    public Vector3 resetPosition = new Vector3(0, 5, 0); // Default reset position
    public float resetDelay = 0.1f; // 100 milliseconds (0.1 seconds)

    private void Update()
    {
        // Check if the player presses the R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the ball immediately
            ResetBallPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with the "Ocean"
        if (other.gameObject.name == "Ocean")
        {
            // Start the coroutine to reset the position with a delay
            StartCoroutine(ResetPositionAfterDelay());
        }
    }

    private IEnumerator ResetPositionAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(resetDelay);

        // Reset the ball's position
        ResetBallPosition();
    }

    private void ResetBallPosition()
    {
        // Reset the ball's position
        transform.position = resetPosition;

        // Optional: Reset velocity if the ball has a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
