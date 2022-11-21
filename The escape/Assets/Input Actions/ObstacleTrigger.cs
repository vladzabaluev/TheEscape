using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out CharacterStats characterStats))
		{
			characterStats.TakeDamage(characterStats.MaxHealth);
		}
	}
}
