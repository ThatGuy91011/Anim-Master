using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Updates UI health bar to current health
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    // Makes sure slider doesn't go over the max health
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
