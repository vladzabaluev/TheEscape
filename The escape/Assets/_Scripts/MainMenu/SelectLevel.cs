using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
	private Button _levelSelect;
	private TextMeshProUGUI _levelNumber;

	private void Start()
	{
		_levelSelect = GetComponent<Button>();
		_levelNumber = GetComponentInChildren<TextMeshProUGUI>();

		_levelSelect.onClick.AddListener(OnLevelSelectedClick);
	}

	private async void OnLevelSelectedClick()
	{
		int.TryParse(_levelNumber.text, out int indexLevel);
		Constants.Scenes sceneName = (Constants.Scenes)indexLevel;

		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(sceneName.ToString()));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}
}
