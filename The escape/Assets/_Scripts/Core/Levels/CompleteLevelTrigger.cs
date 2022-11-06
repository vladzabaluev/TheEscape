using UnityEngine;

public class CompleteLevelTrigger : MonoBehaviour
{
	[SerializeField] private Level _level;
	[SerializeField] private string _nextLevel;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent(out PlayerHealth _))
			_level.LevelComplete(_nextLevel);
	}
}
