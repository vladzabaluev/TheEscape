using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Canvas _mainMenuCanvas;
	[SerializeField] private Button _selectLevels;
	[SerializeField] private Button _settings;
	[SerializeField] private Button _exit;

	[SerializeField] private Canvas _settingsCanvas;
	[SerializeField] private Button _selectLanguage;
	[SerializeField] private Button _musicSettings;
	[SerializeField] private Button _soundsSettings;
	[SerializeField] private Button _settingsReturn;

	[SerializeField] private Canvas _selectLevelCanvas;
	[SerializeField] private Button _levelsReturn;

	private void Start()
	{
		_mainMenuCanvas.enabled = true;
		_settingsCanvas.enabled = false;
		_selectLevelCanvas.enabled = false;

		_selectLevels.onClick.AddListener(OnSelectLevelsBtnClick);
		_settings.onClick.AddListener(OnSettingsBtnClick);
		_exit.onClick.AddListener(OnExitBtnClick);
		_selectLanguage.onClick.AddListener(OnSelectLanguageBtnClick);
		_musicSettings.onClick.AddListener(OnMusicSettingsBtnClick);
		_soundsSettings.onClick.AddListener(OnSoundSettingsBtnClick);
		_settingsReturn.onClick.AddListener(OnReturnBtnClick);
		_levelsReturn.onClick.AddListener(OnReturnBtnClick);
	}

	private void OnSelectLevelsBtnClick()
	{
		_selectLevelCanvas.enabled = true;
		_mainMenuCanvas.enabled = false;
		_settingsCanvas.enabled = false;
	}

	private void OnSettingsBtnClick()
	{
		_settingsCanvas.enabled = true;
		_mainMenuCanvas.enabled = false;
		_selectLevelCanvas.enabled = false;
	}

	private void OnReturnBtnClick()
	{
		_mainMenuCanvas.enabled = true;
		_settingsCanvas.enabled = false;
		_selectLevelCanvas.enabled = false;
	}

	private void OnSelectLanguageBtnClick()
	{

	}
	private void OnSoundSettingsBtnClick()
	{

	}

	private void OnMusicSettingsBtnClick()
	{

	}

	private void OnExitBtnClick()
	{

	}
}
