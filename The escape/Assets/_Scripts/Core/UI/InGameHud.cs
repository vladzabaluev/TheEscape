using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
	[SerializeField] private Slider _healthBarSlider;
	[SerializeField] private Button _pauseButton;
	[SerializeField] private PlayerHealth _playerHealth;

	private bool _isPaused;

	public event Action QuitGame;
	public event Action ReloadLevel;

	private void Awake()
	{
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
		bool isConfirmed = await AlertPopup.Instance.AwaitForDecision("Confirm Exit", "Do you want to go to the main menu?", "Yes", "No");
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
}
