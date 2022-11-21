using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
	[SerializeField] private Canvas _selectLevelCanvas;
	[SerializeField] private Button _continueGame;
	[SerializeField] private Button _newGame;
	[SerializeField] private LevelSelection _levelSelection;

	private JsonSaveSystem _saveSystem;

	private void Start()
	{
		_saveSystem = new JsonSaveSystem();
		_selectLevelCanvas.enabled = false;

		_continueGame.onClick.AddListener(OnContinueBtnClick);
		_newGame.onClick.AddListener(OnNewGameBtnClick);
	}

	private void OnContinueBtnClick()
	{
		_selectLevelCanvas.enabled = true;
	}

	private async void OnNewGameBtnClick()
	{
		bool isConfirmed = await AlertPopup.Instance.
			AwaitForDecision("Confirm Start New Game", "Do you want restore your in game saves and start a new game?", "Yes", "No");

		if (isConfirmed)
		{
			AppInfoContainer newData = new()
			{
				UnlockedLevelsCount = 1
			};
			ProjectContext.Instance.AppInfo = newData;
			_saveSystem.Save(newData);

			_levelSelection.UpdateAvailableLevels();
		}
	}
}
