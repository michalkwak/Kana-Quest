using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public HealthBar healthBar;

    public int maxHealth = 4;
    public int currentHealth;
    public int minHealth = 0;

    public Button restartButton;
    public Image blocker;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxhealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void SetMaxhealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("Current health is " + health);

        if(health <= minHealth)
        {
            RestartGame();
        }
    }

    public void ResetHealth()
    {
        healthBar.currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        Debug.Log("Health restored");
    }

    public void RestartGame()
    {
        restartButton.gameObject.SetActive(true);
        blocker.gameObject.SetActive(true);
        Debug.Log("The button is active");  
    }
}
