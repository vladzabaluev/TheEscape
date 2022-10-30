using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float _attackDamage;
	[SerializeField] private float _defaultAttackCooldown;
	[SerializeField] private PlayerHealthBN _playerHealthBN;

	private float _attackCooldown;

	private void Start()
	{
		_attackCooldown = _defaultAttackCooldown;
	}

	private void Update()
	{
		_attackCooldown -= Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision2D)
	{
		if (collision2D.gameObject.TryGetComponent(out _playerHealthBN) && _attackCooldown <= 0)
		{
			_playerHealthBN.TakeDamage(_attackDamage);
			_attackCooldown = _defaultAttackCooldown;
		}
	}
}
