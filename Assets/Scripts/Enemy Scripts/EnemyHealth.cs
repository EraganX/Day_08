using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Image healthImage;

    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar();
    }


    public void DecreaseHealth()
    {
        currentHealth--;
        HealthBar();

        if (currentHealth <= 0)
        {
            GameManager.Instance.UpdateScore();
            Destroy(gameObject);
        }
    }


    private void HealthBar()
    {
        healthImage.fillAmount = (float)currentHealth/maxHealth;
    }

}
