using System;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
	[SerializeField] private GameObject _deathMenu;
	[SerializeField] private GameObject _settings;

	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _lastCheckpointButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _confirmSettingsButton;

	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	public event Action RestartLevel;
	public event Action ExitMenu;

	private void Start()
	{
		_restartButton.onClick.AddListener(OnRestartClick);
		_lastCheckpointButton.onClick.AddListener(OnLastCheckpointClick);
		_settingsButton.onClick.AddListener(OnSettingsClick);
		_exitButton.onClick.AddListener(OnExitMenuClicked);
		_confirmSettingsButton.onClick.AddListener(OnConfirmClicked);

		_deathMenu.SetActive(false);
		_settings.SetActive(false);
	}

	public void PauseGame()
	{
		PauseManager.SetPaused(true);
		_deathMenu.SetActive(true);
	}

	private void OnRestartClick()
	{
		RestartLevel?.Invoke();
		_deathMenu.SetActive(false);
	}

	private void OnLastCheckpointClick()
	{
		Debug.Log("Restart from last Checkpoint");
	}

	private void OnSettingsClick()
	{
		_deathMenu.SetActive(false);
		_settings.SetActive(true);
	}

	private void OnConfirmClicked()
	{
		_deathMenu.SetActive(true);
		_settings.SetActive(false);
	}

	private async void OnExitMenuClicked()
	{
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Exit", "Do you want to go to the main menu?", "Yes", "No");

		if (isConfirmed)
		{
			ExitMenu?.Invoke();
			_deathMenu.SetActive(false);
		}
	}
}
