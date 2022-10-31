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
		var operations = new Queue<ILoadingOperation>();
		int.TryParse(_levelNumber.text, out int indexLevel);
		operations.Enqueue(new LevelLoadingOperation(indexLevel));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}
}
