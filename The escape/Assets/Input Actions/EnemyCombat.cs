using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
	private EnemyStats _stats;

	private void Start()
	{
		_stats = gameObject.GetComponent<EnemyStats>();
	}

	private void OnCollisionEnter2D(Collision2D collision2D)
	{
		if (collision2D.gameObject.TryGetComponent(out PlayerStats playerStats))
		{
			Debug.Log(_stats.Damage);
			playerStats.TakeDamage(_stats.Damage);
		}
	}
}
