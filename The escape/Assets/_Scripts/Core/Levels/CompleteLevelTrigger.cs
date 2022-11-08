using System;
using UnityEngine;

public class CompleteLevelTrigger : MonoBehaviour
{
	[SerializeField] private Level _level;

	private string _nextLevel;
	[SerializeField] private WinMenu _winMenu;

	private void Start()
	{
		_nextLevel = ((Constants.Scenes)((int)_level.SceneName + 1)).ToString();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent(out PlayerStats _))
		{
			_winMenu.OnLevelComplete(_nextLevel);
			_level.LevelComplete();
		}
	}
}
