using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
	public event Action PlayerDied;

	protected override void Die()
	{
		base.Die();
		Debug.Log("Game Over");
		PlayerDied?.Invoke();
	}
}
