using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 5;
    private int CurrentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.SetHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            //Destroy(gameObject);
        }
    }
}
