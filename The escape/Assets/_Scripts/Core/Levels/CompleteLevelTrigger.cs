using System;
using UnityEngine;

public class CompleteLevelTrigger : MonoBehaviour
{
	[SerializeField] private Level _level;

	private string _nextLevel;

	private void Start()
	{
		_nextLevel = ((Constants.Scenes)((int)_level.SceneName + 1)).ToString();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent(out PlayerHealth _))
			_level.LevelComplete(_nextLevel);
	}
}
