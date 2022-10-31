using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
	[SerializeField] private Slider _healthBarSlider;
	[SerializeField] private Button _pauseButton;
	[SerializeField] private PlayerHealth _playerHealth;

	private bool _isPaused;

	public event Action QuitGame;

	private void Awake()
	{
		//_pauseButton.onClick.AddListener(OnPauseClicked);
		_pauseButton.onClick.AddListener(OnQuitButtonClicked);

		_healthBarSlider.maxValue = _playerHealth.MaxHealth;

		_playerHealth.HealthChanged += UpdatePlayerHealth;
		UpdatePlayerHealth();
	}

	private void UpdatePlayerHealth()
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
		bool isConfirmed = await AlertPopup.Instance.AwaitForDecision("Are you sure to quit");
		OnPauseClicked();

		if (isConfirmed)
			QuitGame?.Invoke();
	}
}
