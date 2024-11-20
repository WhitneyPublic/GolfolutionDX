using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChuckExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {	
        GetComponent<ChuckSubInstance>().RunCode(@"
		    SinOsc sin => dac;
            Bitcrusher bc => dac;
            8=> bc.bits;
            8 => bc.downsample;
            fun void play()
            {
		        while( true )
		        {
			        Math.random2( 220, 440 ) => sin.freq;
			        100::ms => now;
		        }
            }
            global Event impactHappened;

            spork ~ play();

            impactHappened => now;
            sin =< dac;
            sin => bc => dac;

            while (true) {
                1::second => now;
            }

	    ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // print the object that the ball collided with
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Ball")
        {
            GetComponent<ChuckSubInstance>().BroadcastEvent("impactHappened");
        }

    }
}
