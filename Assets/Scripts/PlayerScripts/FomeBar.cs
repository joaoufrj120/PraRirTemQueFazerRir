using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FomeBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFome(float fome)
    {
        slider.maxValue = fome;
        slider.value = fome;
    }

    public void Fome(float fome)
    {
        slider.value = fome;
    }
}
