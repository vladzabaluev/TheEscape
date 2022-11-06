using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
	[SerializeField] private Button[] _levels;
	private JsonSaveSystem _saveSystem;

	private void Start()
	{
		int unlockedLevels = ProjectContext.Instance.AppInfo.UnlockedLevelsCount;

		for (int i = 0; i < _levels.Length; i++)
		{
			if (i + 1 > unlockedLevels)
			{
				_levels[i].interactable = false;
			}
		}
	}

	public async void Select(string sceneName)
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(sceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}
}
