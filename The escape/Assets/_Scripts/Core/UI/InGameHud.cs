using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
	[SerializeField] private Slider _healthBarSlider;
	[SerializeField] private Button _pauseButton;
	[SerializeField] private PlayerHealth _playerHealth;

	private bool _isPaused;
	private JsonSaveSystem _saveSystem;

	public event Action<string> LoadNextLevel;
	public event Action ReloadLevel;
	public event Action QuitGame;

	private void Awake()
	{
		_saveSystem = new JsonSaveSystem();

		//_pauseButton.onClick.AddListener(OnPauseClicked);
		_pauseButton.onClick.AddListener(OnQuitButtonClicked);

		_healthBarSlider.maxValue = _playerHealth.MaxHealth;

		_playerHealth.HealthChanged += OnUpdatePlayerHealth;
		_playerHealth.PlayerDied += OnPlayerDied;
		OnUpdatePlayerHealth();
	}

	private void OnUpdatePlayerHealth()
	{
		_healthBarSlider.value = _playerHealth.Health;
	}

	private void OnPauseClicked()
	{
		_isPaused = !_isPaused;
		ProjectContext.Instance.PauseManager.SetPaused(_isPaused);
	}

	private async void OnQuitButtonClicked()
	{
		OnPauseClicked();
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Exit", "Do you want to go to the main menu?", "Yes", "No");
		OnPauseClicked();

		if (isConfirmed)
			QuitGame?.Invoke();
	}

	private async void OnPlayerDied()
	{
		OnPauseClicked();
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Game Over!", "Do you want retry level?", "Yes", "Exit to menu");
		OnPauseClicked();

		if (isConfirmed)
			ReloadLevel?.Invoke();
		else
			QuitGame?.Invoke();
	}

	public async void LevelComplete(string sceneName)
	{
		OnPauseClicked();
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Level Completed!", "Do you want to go to the next level?", "Yes", "Exit to menu");
		OnPauseClicked();

		ProjectContext.Instance.AppInfo.UnlockedLevelsCount++;
		_saveSystem.Save(ProjectContext.Instance.AppInfo);

		if (isConfirmed)
			LoadNextLevel?.Invoke(sceneName);
		else
			QuitGame?.Invoke();
	}
}
