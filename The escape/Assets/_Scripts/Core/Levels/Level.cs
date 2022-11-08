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
		{ // Ёкземпл€р менеджера был найден
			Instance = this; // «адаем ссылку на экземпл€р объекта
			Initialize();
		}
		else
		{ // Ёкземпл€р объекта уже существует на сцене
			Destroy(this.gameObject); // ”дал€ем объект
		}
		//Instance = this;
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
		//PauseManager.SetPaused(true);
		//bool isConfirmed = await AlertPopup.Instance.
		//	AwaitForDecision("Level Completed!", "Do you want to go to the next level?", "Yes", "Exit to menu");
		//PauseManager.SetPaused(false);

		if (ProjectContext.Instance.AppInfo.UnlockedLevelsCount <= (int)SceneName)
		{
			ProjectContext.Instance.AppInfo.UnlockedLevelsCount++;
			_saveSystem.Save(ProjectContext.Instance.AppInfo);
		}

		//if (isConfirmed)
		//	GoToNextLevel(sceneName);
		//else
		//	GoToMainMenu();
	}

	private async void Restart()
	{
		Debug.Log("lvl restart");
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(SceneName.ToString()));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	private int a = 0;

	private async void GoToMainMenu()
	{
		Debug.Log(a++);
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

	private void OnPlayerDied()
	{
		//PauseManager.SetPaused(true);
		//bool isConfirmed = await AlertPopup.Instance.
		//	AwaitForDecision("Game Over!", "Do you want retry level?", "Yes", "Exit to menu");
		//PauseManager.SetPaused(false);
		_deathMenu.PauseGame();
		//if (isConfirmed)
		//	Restart();
		//else
		//	GoToMainMenu();
	}

	private void OnDisable()
	{
		ProjectContext.Instance.PauseManager.UnRegister(this);
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
	}
}
