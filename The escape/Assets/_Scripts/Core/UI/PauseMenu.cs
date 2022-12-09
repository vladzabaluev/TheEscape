using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private GameObject _settings;

	[Header("Êíîïêè")]
	[SerializeField] private Button _continueButton;

	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _lastCheckpointButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _confirmSettingsButton;
	[SerializeField] private Button _closeSettingsButton;

	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	public event Action RestartLevel;

	public event Action ExitMenu;

	private void Start()
	{
		_continueButton.onClick.AddListener(OnContinueClick);
		_restartButton.onClick.AddListener(OnRestartClick);
		_lastCheckpointButton.onClick.AddListener(OnLastCheckpointClick);
		_settingsButton.onClick.AddListener(OnSettingsClick);
		_exitButton.onClick.AddListener(OnExitMenuClicked);
		_confirmSettingsButton.onClick.AddListener(OnSettingsClose);
		_closeSettingsButton.onClick.AddListener(OnSettingsClose);

		_pauseMenu.SetActive(false);
		_settings.SetActive(false);
	}

	public void PauseGame()
	{
		PauseManager.SetPaused(true);
		_pauseMenu.SetActive(true);
	}

	private void OnContinueClick()
	{
		PauseManager.SetPaused(false);
		_pauseMenu.SetActive(false);
	}

	private void OnRestartClick()
	{
		RestartLevel?.Invoke();
		_pauseMenu.SetActive(false);
	}

	private void OnLastCheckpointClick()
	{
		Debug.Log("Restart from last Checkpoint");
	}

	private void OnSettingsClick()
	{
		_pauseMenu.SetActive(false);
		_settings.SetActive(true);
	}

	private void OnSettingsClose()
	{
		_pauseMenu.SetActive(true);
		_settings.SetActive(false);
	}

	private async void OnExitMenuClicked()
	{
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Exit", "Do you want to go to the main menu?", "Yes", "No");

		if (isConfirmed)
		{
			ExitMenu?.Invoke();
			_pauseMenu.SetActive(false);
		}
	}
}
