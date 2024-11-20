using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
    private Rigidbody rb;
    private AudioSource audioSource;

    public Slider chargeSlider;

    public float stopThreshold = 0.2f;

    // charging stuff
    public float maxKickForce = 60f; // Maximum force to apply
    public float minKickForce = 5f; // Minimum force to apply when charged slightly
    public float chargeSpeed = 10f; // Speed at which the kick force charges

    private float currentCharge = 0f; // Current force value
    private bool isCharging = false; // Whether the player is charging the kick

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        chargeSlider.minValue = minKickForce;
        chargeSlider.maxValue = maxKickForce;
    }

    // Update is called once per frame
    void Update()
    {
        // apply kicking forces when space bar is pressed

        // Start charging when spacebar is held
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentCharge = minKickForce; // Start with the minimum force
        }

        // Increase charge while spacebar is held
        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            currentCharge += chargeSpeed * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, minKickForce, maxKickForce);
        }

        // Apply force when spacebar is released
        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            KickBall();
            isCharging = false;
            currentCharge = 0f; // Reset charge
        }

        // stop the ball if it is moving slowly
        if (rb.velocity.magnitude > 0.0f) { // ball is moving
            Vector2 ballHorizontal = new Vector2(rb.velocity.x, rb.velocity.z);
            if (ballHorizontal.magnitude < stopThreshold) {
                rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
                rb.freezeRotation = true;
                rb.freezeRotation = false;
            }
        }

        UpdateUI();

    }

    void KickBall()
    {
        // get main camera
        GameObject mainCamera = GameObject.Find("Main Camera");
        // get the horizontal direction of the camera
        Vector3 cameraForward = (new Vector3(mainCamera.transform.forward.x, 0.0f, mainCamera.transform.forward.z)).normalized;
        
        print(cameraForward);
        print("Kicking with force: " + currentCharge);

        rb.AddForce(cameraForward * currentCharge, ForceMode.Impulse);
        rb.AddForce(Vector3.up * (currentCharge * (currentCharge/maxKickForce) + 0.5f), ForceMode.Impulse);
        // apply a random rotational force to the ball
        Vector3 randSpin = Random.insideUnitSphere * currentCharge * 1.5f;
        rb.AddTorque(randSpin);

        // play sound
        audioSource.PlayOneShot(audioSource.clip);
    }

    void UpdateUI()
    {
        //Debug.Log($"Updating UI: {currentCharge}");
        // Update the charge slider
        chargeSlider.value = currentCharge;
    }
}
