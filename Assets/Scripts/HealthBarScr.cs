using UnityEngine;
using UnityEngine.UI;

public class HealthBarScr : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.05f;

    void Start()
    {
    }

    void Update()
    {
        if (healthSlider.value != health)
            healthSlider.value = health;

        if(healthSlider.value != easeHealthSlider.value)
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
    }

    public void SetHP(float maxHP)
    {
        easeHealthSlider.maxValue = maxHealth;
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
    }

    public void changeHP(float value)
    {
        health -= value;
    }
}
