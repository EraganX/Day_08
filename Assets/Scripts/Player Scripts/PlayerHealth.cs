using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void IncreaseHealth()
    {
        currentHealth++;

        if (currentHealth>=maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DecreaseHealth()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            //GameOver
        }
    }
}
