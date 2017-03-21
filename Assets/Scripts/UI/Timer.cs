using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField]
    private Text timerText;

    private float time;

    private int minutes;
    private int seconds;
    private int fraction;

    public bool Run { get; set; }

    // Use this for initialization
    void Start () {
        time = 0;
        timerText.text = "00:00";
    }
	
	// Update is called once per frame
	void Update () {
        if (Run)
        {
            time += Time.deltaTime;

            minutes = (int) time / 60;
            seconds = (int) time % 60;
            fraction = (int) (time * 100) % 100;

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
       
    }
}
