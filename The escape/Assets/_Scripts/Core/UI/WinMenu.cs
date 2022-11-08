using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
	[SerializeField] private GameObject _levelCompleteMenu;
	[SerializeField] private GameObject _settings;
	[SerializeField] private Button _nextLevelButton;
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _confirmSettingsButton;

	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	public event Action<string> NextLevel;

	public event Action RestartLevel;

	public event Action ExitMenu;

	private string nextLevelName;

	private void Start()
	{
		_nextLevelButton.onClick.AddListener(OnNextLevelClick);
		_restartButton.onClick.AddListener(OnRestartClick);

		_settingsButton.onClick.AddListener(OnSettingsClick);
		_exitButton.onClick.AddListener(OnExitMenuClicked);
		_confirmSettingsButton.onClick.AddListener(OnConfirmClicked);

		_levelCompleteMenu.SetActive(false);
		_settings.SetActive(false);
	}

	public void OnLevelComplete(string nextLevel)
	{
		Debug.Log("com");
		PauseManager.SetPaused(true);
		nextLevelName = nextLevel;
		_levelCompleteMenu.SetActive(true);
	}

	private void OnNextLevelClick()
	{
		NextLevel.Invoke(nextLevelName);

		_levelCompleteMenu.SetActive(false);
	}

	private void OnRestartClick()
	{
		RestartLevel?.Invoke();
		_levelCompleteMenu.SetActive(false);
	}

	private void OnSettingsClick()
	{
		_levelCompleteMenu.SetActive(false);
		_settings.SetActive(true);
	}

	private void OnConfirmClicked()
	{
		_levelCompleteMenu.SetActive(true);
		_settings.SetActive(false);
	}

	private async void OnExitMenuClicked()
	{
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Exit", "Do you want to go to the main menu?", "Yes", "No");
		//PauseManager.SetPaused(false);

		//_pauseMenu.SetActive(false);
		if (isConfirmed)
		{
			ExitMenu?.Invoke();
			_levelCompleteMenu.SetActive(false);
		}
	}
}
