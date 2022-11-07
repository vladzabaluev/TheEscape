using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, IPauseHandler
{
	[SerializeField] private PlayerHealth _playerHealth;
	[SerializeField] private PauseMenu _pauseMenu;

	private JsonSaveSystem _saveSystem;
	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	public virtual Constants.Scenes SceneName => 0;

	public void Initialize()
	{
		_saveSystem = new JsonSaveSystem();
		ProjectContext.Instance.PauseManager.Register(this);

		_playerHealth.PlayerDied += OnPlayerDied;

		_pauseMenu.RestartLevel += Restart;
		_pauseMenu.ExitMenu += GoToMainMenu;
	}

	public async void LevelComplete(string sceneName)
	{
		PauseManager.SetPaused(true);
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Level Completed!", "Do you want to go to the next level?", "Yes", "Exit to menu");
		PauseManager.SetPaused(false);

		if (ProjectContext.Instance.AppInfo.UnlockedLevelsCount <= (int)SceneName)
		{
			ProjectContext.Instance.AppInfo.UnlockedLevelsCount++;
			_saveSystem.Save(ProjectContext.Instance.AppInfo);
		}

		if (isConfirmed)
			GoToNextLevel(sceneName);
		else
			GoToMainMenu();
	}

	private async void Restart()
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(SceneName.ToString()));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void GoToMainMenu()
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new ClearLevelOperation(SceneName.ToString()));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void GoToNextLevel(string sceneName)
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(sceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void OnPlayerDied()
	{
		PauseManager.SetPaused(true);
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Game Over!", "Do you want retry level?", "Yes", "Exit to menu");
		PauseManager.SetPaused(false);

		if (isConfirmed)
			Restart();
		else
			GoToMainMenu();
	}

	void IPauseHandler.SetPaused(bool isPaused) { }
}
