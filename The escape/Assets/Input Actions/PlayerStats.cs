using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
	public event Action PlayerDied;

	public override void Die()
	{
		base.Die();
		Debug.Log("Game Over");
		PlayerDied?.Invoke();
	}
}
