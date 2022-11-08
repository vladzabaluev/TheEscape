using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
	private EnemyStats stats;

	// Start is called before the first frame update
	private void Start()
	{
		stats = gameObject.GetComponent<EnemyStats>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
		if (player != null)
		{
			Debug.Log(stats.damage);
			player.TakeDamage(stats.damage);
		}
	}
}
