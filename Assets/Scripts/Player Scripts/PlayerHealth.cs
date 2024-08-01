using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private Image healthImage;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar();
    }

    public void IncreaseHealth()
    {
        currentHealth++;
        HealthBar();
        if (currentHealth>=maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DecreaseHealth()
    {
        currentHealth--;
        HealthBar();
        if (currentHealth <= 0)
        {
            GameManager.Instance.isGameOver = true;
            //GameOver
        }
    }

    private void HealthBar()
    {
        healthImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
