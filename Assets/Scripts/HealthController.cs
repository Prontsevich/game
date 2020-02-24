using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 10;
    public Action onDie;

    private int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = MaxHealth;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && onDie != null && !isDead)
        {
            onDie();
            isDead = true;
        }
    }
}
