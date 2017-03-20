using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour {
    private float timer = 0.0f;
    private bool running = false;
    private bool animating = false;
    public float startupTime = .15f;
    public GameObject spikes;

    private Vector3 orig;

    // Use this for initialization
    void Start()
    {
        orig = transform.position;
    }

    void ToggleState()
    {
        running = !running;
        if (running)
        {
            GetComponent<AudioSource>().Play();
        }
        animating = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            ToggleState();
            timer = .0f;
        }

        float timerClamped = Mathf.Clamp(timer, 0, startupTime) / startupTime;
        
        if (animating)
        {
            if (running)
            {
                transform.position = Vector3.Lerp(orig, orig + new Vector3(0, 0.6f, 0), timerClamped);
            }
            else
            {
                transform.position = Vector3.Lerp(orig + new Vector3(0, 0.6f, 0), orig, timerClamped);
            }
        }
        if (timerClamped >= 1.0f)
        {
            animating = false;
        }
    }
}