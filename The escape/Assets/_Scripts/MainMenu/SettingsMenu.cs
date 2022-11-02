using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private Button _selectLanguage;
	[SerializeField] private Button _musicSettings;
	[SerializeField] private Button _soundsSettings;

	private void Start()
	{
		_selectLanguage.onClick.AddListener(OnSelectLanguageBtnClick);
		_musicSettings.onClick.AddListener(OnMusicSettingsBtnClick);
		_soundsSettings.onClick.AddListener(OnSoundsSettingsBtnClick);
	}

	private void OnSelectLanguageBtnClick()
	{

	}

	private void OnMusicSettingsBtnClick()
	{

	}

	private void OnSoundsSettingsBtnClick()
	{

	}
}
