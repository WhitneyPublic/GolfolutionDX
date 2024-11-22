using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sunLight; // Assign your Directional Light here
    public float dayDuration = 120f; // Duration of a full day/night cycle in seconds
    public float startTimestamp = 0f; // Initial timestamp of the cycle, in seconds
    private float time = 0f; // Tracks time progression in the cycle, starts at mis

    void Start()
    {
        // Set the initial time
        time = startTimestamp;
    }

    void Update()
    {
        // Update time
        time += Time.deltaTime;
        float cycleProgress = (time / dayDuration) % 1f; // Normalize to [0, 1] range

        // Rotate the light to simulate sun movement
        sunLight.transform.rotation = Quaternion.Euler(new Vector3((cycleProgress * 360f) - 90f, 170f, 0f));

        // Adjust intensity and color based on cycle progress
        if (cycleProgress <= 0.25f) // Dawn
        {
            sunLight.color = Color.Lerp(Color.black, new Color(1f, 0.5f, 0.25f), cycleProgress * 4f);
            sunLight.intensity = Mathf.Lerp(0f, 1f, cycleProgress * 4f);
        }
        else if (cycleProgress <= 0.5f) // Morning to Noon
        {
            sunLight.color = Color.Lerp(new Color(1f, 0.5f, 0.25f), Color.white, (cycleProgress - 0.25f) * 4f);
            sunLight.intensity = Mathf.Lerp(1f, 1.2f, (cycleProgress - 0.25f) * 4f);
        }
        else if (cycleProgress <= 0.75f) // Afternoon to Dusk
        {
            sunLight.color = Color.Lerp(Color.white, new Color(1f, 0.25f, 0.1f), (cycleProgress - 0.5f) * 4f);
            sunLight.intensity = Mathf.Lerp(1.2f, 0f, (cycleProgress - 0.5f) * 4f);
        }
        else // Night
        {
            sunLight.color = Color.Lerp(new Color(1f, 0.25f, 0.1f), Color.black, (cycleProgress - 0.75f) * 4f);
            sunLight.intensity = Mathf.Lerp(0f, 0f, (cycleProgress - 0.75f) * 4f);
        }
    }
}

