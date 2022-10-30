using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
	[SerializeField] private Slider _healthBarSlider;
	[SerializeField] private Button _pauseButton;
	[SerializeField] private PlayerHealth _playerHealth;

	private bool _isPaused;

	public event Action<bool> PauseClicked;

	private void Awake()
	{
		_pauseButton.onClick.AddListener(OnPauseClicked);

		_healthBarSlider.maxValue = _playerHealth.MaxHealth;

		_playerHealth.OnHealthChangedCallback += UpdatePlayerHealth;
		UpdatePlayerHealth();
	}

	private void OnPauseClicked()
	{
		_isPaused = !_isPaused;
		PauseClicked?.Invoke(_isPaused);
	}

	private void UpdatePlayerHealth()
	{
		_healthBarSlider.value = _playerHealth.Health;
	}
}
