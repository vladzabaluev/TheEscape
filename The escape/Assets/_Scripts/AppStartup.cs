using System.Collections.Generic;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
	private JsonSaveSystem _saveSystem;

	private async void Start()
	{
		ProjectContext.Instance.Initialize();
		_saveSystem = new JsonSaveSystem();

		ProjectContext.Instance.AppInfo = _saveSystem.Load();

		var loadingOperations = new Queue<ILoadingOperation>();
		loadingOperations.Enqueue(new ConfigOperation());
		loadingOperations.Enqueue(new MenuLoadingOperation());

		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(loadingOperations);
	}
}
