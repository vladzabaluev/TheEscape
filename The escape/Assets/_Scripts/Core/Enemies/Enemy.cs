using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected float AttackDamage;
	protected virtual void OnCollisionEnter2D(Collision2D collision2D) { }
}
