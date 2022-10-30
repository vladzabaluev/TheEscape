using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] private InGameHud _inGameHud;

	public string SceneName => "Level";

	private void Start()
	{
		_inGameHud.PauseClicked += OnPauseClicked;
	}

	private void OnPauseClicked(bool isPaused)
	{
		Time.timeScale = isPaused ? 0f : 1f;
	}

	private void GoToMainMenu()
	{
		/*
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new ClearGameOperations(this));
		LoadingScreen.Instance.Load(operations);
		*/
	}
}
