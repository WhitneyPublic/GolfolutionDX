using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool hasScored = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // print the object that the ball collided with
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Ball" && !hasScored)
        {
            hasScored = true;
            Debug.Log("Goal!");
            // get material of the child object

            Material[] materials = GetComponentsInChildren<Material>();

            // light blue color
            Color lightBlue = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            for (int i = 0; i < materials.Length; i++)
            {
                // set the emission color to light blue
                materials[i].SetColor("_EmissionColor", lightBlue);
            }
        }

    }
}
