using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int MaxHealth;
	public int Damage;

	public HealthBar bar;

	private int _currentHealth;

	private void Awake()
	{
		_currentHealth = MaxHealth;
	}

	protected virtual void Die() { }

	private void Start()
	{
		bar.SetMaxHealth(MaxHealth);
	}

	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;

		bar.SetHealth(_currentHealth);
		if (_currentHealth <= 0)
			Die();
	}
}
