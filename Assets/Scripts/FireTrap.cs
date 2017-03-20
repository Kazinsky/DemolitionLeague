using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour {

    public ParticleSystem p;
    private float timer = 0.0f;
    private bool running = false;
    public float startupTime = .15f;

    // Use this for initialization
    void Start () {
		
	}

    void ToggleState()
    {
        running = !running;
        if (running)
        {
            GetComponent<AudioSource>().Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            ToggleState();
            timer = .0f;
        }

        var em = p.emission;
        em.enabled = running;
        float timerClamped = Mathf.Clamp(timer, 0, startupTime) / startupTime;
        float maxPitch = 1.5f;
        float minPitch = 0.3f;
        GetComponent<AudioSource>().pitch = (running ? timerClamped: 1 - timerClamped) * (maxPitch - minPitch) + minPitch;

        float maxVolume = 0.8f;
        float minVolume = 0.2f;
        GetComponent<AudioSource>().volume = (running ? timerClamped : 1 - timerClamped) * (maxVolume - minVolume) + minVolume;
    }
}
