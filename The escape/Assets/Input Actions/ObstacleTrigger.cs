using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<CharacterStats>(out CharacterStats characterStats))
		{
			characterStats.TakeDamage(characterStats.maxHealth);
		}
	}
}
