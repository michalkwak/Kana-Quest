using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;

    public EnemyHealthBar healthBar;

    public Animator enemyAnimator;

    public int maxHealth = 10;
    public int currentHealth;
    public int minHealth = 0;

    public Button restartButton;
    public Image blocker;

    private void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= minHealth)
        {
            EnemyDefeated();
            EnemyDeath();
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("Enemy health is " + health);

        if (health <= minHealth)
        {
            RestartGame();
        }
    }

    public void EnemyDefeated()
    {
        // Code to handle enemy defeated event
        Debug.Log("Enemy defeated!");
    }
    public void EnemyDeath()
    {
        enemyAnimator.SetTrigger("Death");
    }
    public void ResetHealth()
    {
        healthBar.currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    public void RestartGame()
    {
        restartButton.gameObject.SetActive(true);
        blocker.gameObject.SetActive(true);
        Debug.Log("The button is active");
    }
}
