using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 20;
    public int currHealth;
    public float speed = 1.25f;
    public float maxStamina = 10;
    public float currStamina;
    public float staminaRechargeRate = 2.5f;
    public float ammoCapacity = 100;
    public float currentAmmoCount = 60;
    public Slider healthSlider;
    public Slider staminaSlider;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        currStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currHealth;
        staminaSlider.value = currStamina;

        if (currHealth <= 0)
        {
            //Game over
            Destroy(gameObject);
        }
    }
}
