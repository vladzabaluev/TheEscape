using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, IPauseHandler
{
	[SerializeField] private PlayerStats _playerHealth;
	[SerializeField] private PauseMenu _pauseMenu;
	[SerializeField] private DeathMenu _deathMenu;
	[SerializeField] private WinMenu _winMenu;

	private JsonSaveSystem _saveSystem;
	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	private static Level Instance;
	public virtual Constants.Scenes SceneName => 0;

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			Initialize();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Initialize()
	{
		_saveSystem = new JsonSaveSystem();

		ProjectContext.Instance.PauseManager.Register(this);
		ProjectContext.Instance.PauseManager.SetPaused(false);

		_playerHealth.PlayerDied += OnPlayerDied;

		_pauseMenu.RestartLevel += Restart;
		_pauseMenu.ExitMenu += GoToMainMenu;

		_winMenu.RestartLevel += Restart;
		_winMenu.NextLevel += GoToNextLevel;
		_winMenu.ExitMenu += GoToMainMenu;

		_deathMenu.RestartLevel += Restart;
		_deathMenu.ExitMenu += GoToMainMenu;
	}

	public void LevelComplete()
	{
		if (ProjectContext.Instance.AppInfo.UnlockedLevelsCount <= (int)SceneName)
		{
			ProjectContext.Instance.AppInfo.UnlockedLevelsCount++;
			_saveSystem.Save(ProjectContext.Instance.AppInfo);
		}
	}

	private async void Restart()
	{
		PauseManager.SetPaused(false);
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(SceneName.ToString()));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void GoToMainMenu()
	{
		PauseManager.SetPaused(false);
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new ClearLevelOperation(SceneName.ToString()));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private async void GoToNextLevel(string sceneName)
	{
		PauseManager.SetPaused(false);
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(sceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private void OnPlayerDied()
	{
		_deathMenu.PauseGame();
	}

	private void OnDisable()
	{
		ProjectContext.Instance.PauseManager.UnRegister(this);
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
	}
}
