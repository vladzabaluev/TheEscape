using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
	private Animator _animator;
	private static readonly int s_lives = Animator.StringToHash("Lives");
	public event Action PlayerDied;

	protected override void Start()
	{
		base.Start();
		_animator = GetComponent<Animator>();
		_animator.SetFloat(s_lives, CurrentHealth);
	}

	public override void TakeDamage(int damage)
	{
		CurrentHealth -= damage;
		_animator.SetFloat(s_lives, CurrentHealth);

		bar.SetHealth(CurrentHealth);
		if (CurrentHealth <= 0)
			Die();
	}

	protected override void Die()
	{
		Debug.Log("Game Over");
		PlayerDied?.Invoke();
	}
}
