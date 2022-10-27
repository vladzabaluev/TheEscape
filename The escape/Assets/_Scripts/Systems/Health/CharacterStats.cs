using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public int damage;
    public GameObject healthBar;
    private HealthBar bar;
    int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Die()
    {
    }

    private void Start()
    {
        bar = healthBar.GetComponent<HealthBar>();
        bar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        bar.SetHealth(currentHealth);
        if (currentHealth <= 0) Die();
    }
}
