using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar
{
    private int maxHealth;
    private int currentHealth;

    public HealthBar(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public bool isDead()
    {
        return (currentHealth <= 0);
    }

    public void changeHp(int amount)
    {
        currentHealth -= amount;
    }
}
