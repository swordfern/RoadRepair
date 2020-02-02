using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthSliderFill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateHealthSlider(int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;
        healthSliderFill.color = Color.green;
    }

    public void UpdateHealthSlider(int currHealth, int maxHealth)
    {
        healthBarSlider.value = currHealth;

        // 0 - 33% red
        // 34 - 66% yellow
        // 67 - 100% green

        float ratio = (float) currHealth / maxHealth;

        if (ratio < .34)
        {
            healthSliderFill.color = Color.red;
        } else if (ratio < .67)
        {
            healthSliderFill.color = Color.yellow;
        } else
        {
            healthSliderFill.color = Color.green;
        }
    }
}
