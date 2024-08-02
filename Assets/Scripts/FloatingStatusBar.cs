using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Referenced BMo youtube tutorial on floating health bars: https://www.youtube.com/watch?v=_lREXfAMUcE

public class FloatingStatusBar : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void SetCurrentValue(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
