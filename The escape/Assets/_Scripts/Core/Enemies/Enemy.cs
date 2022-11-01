using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected PlayerHealth PlayerHealth;
	protected float AttackDamage => PlayerHealth.MaxHealth;

	protected virtual void OnCollisionEnter2D(Collision2D collision2D) { }
}
