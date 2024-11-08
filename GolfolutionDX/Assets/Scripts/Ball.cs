using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody rb;
    //private SphereCollider sc;
    public float speed = 10.0f;

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
        }

    }
}
