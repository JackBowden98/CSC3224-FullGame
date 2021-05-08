﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Image RingStaminaBar;

    public float stamina, maxStamina = 100;
    float lerpSpeed;

    private void Start()
    {
        stamina = maxStamina;
    }

    private void Update()
    {
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
            RingStaminaBar.enabled = false;
        }
        else
        {
            RingStaminaBar.enabled = true;
            Heal(0.1f);
        }

        lerpSpeed = 7f * Time.deltaTime;

        StaminaBarFiller();
        ColorChanger();
    }

    void StaminaBarFiller()
    {
        RingStaminaBar.fillAmount = Mathf.Lerp(RingStaminaBar.fillAmount, (stamina / maxStamina), lerpSpeed);

    }
    void ColorChanger()
    {
        Color StaminaColour = Color.Lerp(Color.red, Color.green, (stamina / maxStamina));
        RingStaminaBar.color = StaminaColour;
    }

    public void Heal(float healingPoints)
    {
        if (stamina < maxStamina)
            stamina += healingPoints;
    }
}
