using System;
using UnityEngine;

public class CompleteLevelTrigger : MonoBehaviour
{
	[SerializeField] private InGameHud _inGameHud;
	[SerializeField] private string _nextLevel;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent(out PlayerHealth _))
			_inGameHud.LevelComplete(_nextLevel);
	}
}
