using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FomeBar : MonoBehaviour
{
    public Slider slider;

    [SerializeField] private PlayerController playerController;

    public void SetMaxFome(float fome)
    {
        slider.maxValue = fome;
        slider.value = fome;
    }

    public void Fome(float fome)
    {
        slider.value = fome;
    }

    private void Start() {
        SetMaxFome(playerController.HungerTimer);
    }

    private void Update() {
        Fome(playerController.HungerTimer);
    }
}
