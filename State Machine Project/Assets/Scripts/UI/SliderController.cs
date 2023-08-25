using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float lerpDuration;
    public float speed;

    public float maxValue;

    public void Start()
    {
        Reset();
    }

    public void Reset()
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void UpdateSlider(float value)
    {
        float currentValue = slider.value;

        StartCoroutine(LerpSlider(currentValue, value));
    }

    private IEnumerator LerpSlider(float currentValue, float value)
    {
        float time = 0;

        while(time < lerpDuration)
        {
            time += Time.deltaTime * speed;

            slider.value = Mathf.Lerp(currentValue, value, time / lerpDuration);

            yield return null;
        }
    }
}
