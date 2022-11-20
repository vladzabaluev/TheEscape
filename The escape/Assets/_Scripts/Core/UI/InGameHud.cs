using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
	[SerializeField] private Button _pauseButton;
	[SerializeField] private PauseMenu _pauseMenu;

	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	private void Awake()
	{
		_pauseButton.onClick.AddListener(OnPauseClicked);

		//	_healthBarSlider.maxValue = _playerHealth.MaxHealth;

		//	_playerHealth.HealthChanged += OnUpdatePlayerHealth;
		OnUpdatePlayerHealth();
	}

	private void OnUpdatePlayerHealth()
	{
		//_healthBarSlider.value = _playerHealth.Health;
	}

	private void OnPauseClicked()
	{
		_pauseMenu.PauseGame();
	}
}
