using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody rb;
    //private SphereCollider sc;
    public float speed = 10.0f;
    public float stopThreshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //sc = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // apply kicking forces when space bar is pressed

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            // apply a random rotational force to the ball
            Vector3 randSpin = Random.insideUnitSphere * speed * 2;
            rb.AddTorque(randSpin);
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
