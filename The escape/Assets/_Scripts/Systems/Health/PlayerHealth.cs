using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private float _health;
	[SerializeField] private float _maxHealth;

	public delegate void OnHealthChangedDelegate();
	public OnHealthChangedDelegate OnHealthChangedCallback;

	public float Health => _health;
	public float MaxHealth => _maxHealth;
	public bool IsAlive => _health > 0f;

	public void TakeDamage(float damage)
	{
		if (damage < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(damage));
		}

		_health -= damage;
		ClampHealth();

		if (!IsAlive)
		{
			Debug.Log("Player is died! Game Over!");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void ClampHealth()
	{
		_health = Mathf.Clamp(_health, 0f, _maxHealth);

		if (OnHealthChangedCallback != null)
			OnHealthChangedCallback.Invoke();
	}
}
