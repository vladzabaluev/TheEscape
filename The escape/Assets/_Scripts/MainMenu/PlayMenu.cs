using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
	[SerializeField] private Canvas _selectLevelCanvas;
	[SerializeField] private Button _continueGame;
	[SerializeField] private Button _newGame;

	private void Start()
	{
		_selectLevelCanvas.enabled = false;

		_continueGame.onClick.AddListener(OnContinueBtnClick);
		_newGame.onClick.AddListener(OnNewGameBtnClick);
	}

	private void OnContinueBtnClick()
	{
		_selectLevelCanvas.enabled = true;
	}

	private void OnNewGameBtnClick()
	{

	}
}
