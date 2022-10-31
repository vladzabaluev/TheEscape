using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private float _attackDamage;
	[SerializeField] private PlayerHealth _playerHealth;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out _playerHealth))
			_playerHealth.TakeDamage(_attackDamage);
	}
}
