using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private float _attackDamage;
	[SerializeField] private PlayerHealthBN _playerHealthBN;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out _playerHealthBN))
			_playerHealthBN.TakeDamage(_attackDamage);
	}
}
