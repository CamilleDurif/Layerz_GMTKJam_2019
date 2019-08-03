using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour 
{
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    public float MaxValue {get; set;}

    public float Value 
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

	void Start () 
    {
		
	}
	
	void Update () 
    {
        HandleBar();
	}

    private void HandleBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }

    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //(78 - 0) * (1 -  0) / (230 - 0) + 0
        // 78 * 1 / 230 = 0,339
    }
}
