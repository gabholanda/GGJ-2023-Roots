using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;
    private Slider slider;

    void Awake()
    {
       slider = GetComponent<Slider>(); 
    }

    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = (float)playerHealth.currentHealth / (float)playerHealth.maxHealth;
        if(fillValue <= slider.maxValue / 3)
        {
            fillImage.color = Color.red; //crit hp is low
        }
        else if(fillValue > slider.maxValue / 3)
        {
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}
