using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody rb;
    private AudioSource audioSource;
    //private SphereCollider sc;
    public float speed = 10.0f;
    public float stopThreshold = 0.2f;

    // charging stuff


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //sc = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // apply kicking forces when space bar is pressed

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // get main camera
            GameObject mainCamera = GameObject.Find("Main Camera");
            // get the horizontal direction of the camera
            Vector3 cameraForward = (new Vector3(mainCamera.transform.forward.x, 0.0f, mainCamera.transform.forward.z)).normalized;
            print(cameraForward);
            rb.AddForce(cameraForward * speed, ForceMode.Impulse);
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            // apply a random rotational force to the ball
            Vector3 randSpin = Random.insideUnitSphere * speed * 2;
            rb.AddTorque(randSpin);

            // play sound
            audioSource.PlayOneShot(audioSource.clip);
            
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

    }
}
