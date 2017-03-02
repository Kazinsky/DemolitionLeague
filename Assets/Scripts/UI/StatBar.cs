using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatBar : MonoBehaviour {

    //Image to represent stat 
    [SerializeField]
    private Image bar;

    [SerializeField]
    private float speed;

    //Current Value bar is set to. Updates the image bar when the value is changed
    private float currentValue;

    //Max Value the bar can get to. Updates the image bar when the value is changed
    private float maxValue;

    //Value to apply to the fill bar
    private float fillValue;

    //Setters 
    public void SetCurrentValue(float value)
    {
        currentValue = value;
        updateFillValue();
    }

    public void SetMaxValue(float value)
    {
        maxValue = value;
        updateFillValue();
    }

    // Use this for initialization
    void Start () {
        SetCurrentValue(100);
        SetMaxValue(100);
    }
	
	// Update is called once per frame
	void Update () {

        if (bar.fillAmount != fillValue)
        {
            updateBar();
        }
    }

    private void updateBar()
    {
       bar.fillAmount = Mathf.Lerp(bar.fillAmount, fillValue, Time.deltaTime * speed);
    }

    private void updateFillValue()
    {
        fillValue = normalizeValue(currentValue, maxValue);
    }

    //Retruns a value betweeen 0 - 1. Minimum value is asumed to be 0
    private float normalizeValue(float value, float max)
    {
        return value/ max;
    }
}
