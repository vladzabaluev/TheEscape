using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour, IPauseHandler
{
	[SerializeField] private InGameHud InGameHud;

	private bool IsPaused => ProjectContext.Instance.PauseManager.IsPaused;

	public virtual string SceneName => null;

	public virtual void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.LoadNextLevel += GoToNextLevel;
		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}

	private async void Reload()
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(SceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void GoToMainMenu()
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new ClearLevelOperation(SceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void GoToNextLevel(string sceneName)
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(sceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	void IPauseHandler.SetPaused(bool isPaused) { }
}
