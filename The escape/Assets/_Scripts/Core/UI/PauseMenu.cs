using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject _content;
	[SerializeField] private GameObject _settings;
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _lastCheckpointButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _confirmSettingsButton;

	private Canvas _pauseMenu;
	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	public event Action RestartLevel;
	public event Action ExitMenu;

	private void Start()
	{
		_pauseMenu = GetComponent<Canvas>();

		_continueButton.onClick.AddListener(OnContinueClick);
		_restartButton.onClick.AddListener(OnRestartClick);
		_lastCheckpointButton.onClick.AddListener(OnLastCheckpointClick);
		_settingsButton.onClick.AddListener(OnSettingsClick);
		_exitButton.onClick.AddListener(OnExitMenuClicked);
		_confirmSettingsButton.onClick.AddListener(OnConfirmClicked);

		_pauseMenu.enabled = false;
		_content.SetActive(true);
		_settings.SetActive(false);
	}

	public void Show(bool isShown)
	{
		_pauseMenu.enabled = isShown;
	}

	private void OnContinueClick()
	{
		PauseManager.SetPaused(false);
		Show(false);
	}

	private void OnRestartClick()
	{
		RestartLevel?.Invoke();
	}

	private void OnLastCheckpointClick()
	{
		Debug.Log("Restart from last Checkpoint");
	}

	private void OnSettingsClick()
	{
		_content.SetActive(false);
		_settings.SetActive(true);
	}

	private void OnConfirmClicked()
	{
		_content.SetActive(true);
		_settings.SetActive(false);
	}

	private async void OnExitMenuClicked()
	{
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Exit", "Do you want to go to the main menu?", "Yes", "No");
		PauseManager.SetPaused(false);

		_pauseMenu.enabled = false;
		if (isConfirmed)
			ExitMenu?.Invoke();
	}
}
