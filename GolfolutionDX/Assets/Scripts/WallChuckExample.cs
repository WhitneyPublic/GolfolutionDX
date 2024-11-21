using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChuckExample : MonoBehaviour
{
    private ChuckSubInstance myChuck;
    string myGlobalVariableName;
    // Start is called before the first frame update
    void Start()
    {
        myChuck = GetComponent<ChuckSubInstance>();
        myGlobalVariableName = myChuck.GetUniqueVariableName("event");
        GetComponent<ChuckSubInstance>().RunCode(string.Format(@"
		    SawOsc sqr => LPF lpf => dac;
            Bitcrusher bc => dac;
            8=> bc.bits;
            8 => bc.downsample;
            

            0.6 => lpf.Q;
            440 => lpf.freq;

            // our notes
            [ 46, 53, 58, 60, 62, 63, 65, 67 ,69, 70, 46, 53, 58, 65, 67 ,69, 70 ] @=> int notes[];

            // basic play function (add more arguments as needed)
            fun void play( float note )
            {{
                // start the note
                Std.mtof( note - 12 )=> sqr.freq;
                150::ms * Math.random2(0, 8) => now;
            }}
            fun void playing() {{
                while( true ) {{
                    play(notes[Math.random2(0, notes.size() - 1)]);
                }}
            }}

            global Event {0};

            // infinite time-loop
            spork ~ playing();

            {0} => now;
            lpf =< dac;
            lpf => bc => dac;

            while (true) {{
                1::second => now;
            }}

	    ", myGlobalVariableName));
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
            GetComponent<ChuckSubInstance>().BroadcastEvent(myGlobalVariableName);
            print("Broadcasted event: " + myGlobalVariableName);
        }

    }
}
