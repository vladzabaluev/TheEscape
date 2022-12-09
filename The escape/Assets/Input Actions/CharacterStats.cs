using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int MaxHealth;
	public int Damage;

	public HealthBar bar;

	public int CurrentHealth;

	private void Awake()
	{
		CurrentHealth = MaxHealth;
	}

	protected virtual void Die() { }

	protected virtual void Start()
	{
		bar.SetMaxHealth(MaxHealth);
	}

	public virtual void TakeDamage(int damage)
	{
		CurrentHealth -= damage;

		bar.SetHealth(CurrentHealth);
		if (CurrentHealth <= 0)
			Die();
	}
}
