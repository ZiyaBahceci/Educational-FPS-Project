using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }

    void Update()
    {

    }

    public void DamagePlayer(int damageAmount) //Controls player taking damage and dying upon health decreasing down to 0
    {
       
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }
}
