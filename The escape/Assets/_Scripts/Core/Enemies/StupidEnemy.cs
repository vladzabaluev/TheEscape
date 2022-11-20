using UnityEngine;

public class StupidEnemy : Enemy
{
	[SerializeField] private float _defaultAttackCooldown;

	private float _attackCooldown;

	private void Start()
	{
		_attackCooldown = _defaultAttackCooldown;
	}

	private void Update()
	{
		_attackCooldown -= Time.deltaTime;
	}

	protected override void OnCollisionEnter2D(Collision2D collision2D)
	{
		if (collision2D.gameObject.TryGetComponent(out PlayerHealth playerHealth) && _attackCooldown <= 0)
		{
			playerHealth.TakeDamage(AttackDamage);
			_attackCooldown = _defaultAttackCooldown;
		}
	}
}
