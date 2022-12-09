using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Canvas _mainMenuCanvas;
	[SerializeField] private Canvas _playCanvas;
	[SerializeField] private Canvas _settingsCanvas;

	[Header(" нопки главного меню")]
	[SerializeField] private Button _play;

	[SerializeField] private Button _settings;
	[SerializeField] private Button _exit;

	[Header(" нопки настроек")]
	[SerializeField] private Button _confirmSettingsButton;

	[SerializeField] private Button _closeSettingsButton;

	private void Start()
	{
		_mainMenuCanvas.enabled = true;
		_playCanvas.enabled = false;
		_settingsCanvas.enabled = false;

		_play.onClick.AddListener(OnPlayBtnClick);
		_settings.onClick.AddListener(OnSettingsBtnClick);
		_exit.onClick.AddListener(OnExitBtnClick);

		_confirmSettingsButton.onClick.AddListener(OnSettingsClose);
		_closeSettingsButton.onClick.AddListener(OnSettingsClose);
	}

	private void OnPlayBtnClick()
	{
		_playCanvas.enabled = true;
	}

	private void OnSettingsBtnClick()
	{
		_settingsCanvas.enabled = true;
	}

	private void OnSettingsClose()
	{
		_mainMenuCanvas.enabled = true;
		_playCanvas.enabled = false;
	}

	private async void OnExitBtnClick()
	{
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Exit", "Do you want to quit to the game?", "Yes", "No");

		if (isConfirmed)
		{
			Debug.Log("Exit the game");
			Application.Quit();
		}
	}
}
