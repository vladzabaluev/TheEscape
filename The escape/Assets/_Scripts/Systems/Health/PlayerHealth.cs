using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public Image Bar;

	public float Fill;
	private float _lastDamageTime;

	private void Start()
	{
		Fill = 1f;
		_lastDamageTime = Time.time;
	}

	private void Update()
	{
		if (Fill<=0f)
		{
			Debug.Log("Game Over");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Obstacle")
		{
			Debug.Log("Game Over");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else if (collision.gameObject.tag == "Enemy" && Time.time > (_lastDamageTime + 2))
		{
			Fill -= 0.34f;
			Bar.fillAmount = Fill;
			_lastDamageTime = Time.time;
		}
	}
}
