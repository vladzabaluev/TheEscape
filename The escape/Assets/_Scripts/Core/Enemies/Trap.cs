using UnityEngine;

public class Trap : Enemy
{
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerHealth))
			PlayerHealth.TakeDamage(AttackDamage);
	}
}
