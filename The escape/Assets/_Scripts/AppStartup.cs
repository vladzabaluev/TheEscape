using System.Collections.Generic;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
	private async void Start()
	{
		ProjectContext.Instance.Initialize();

		var loadingOperations = new Queue<ILoadingOperation>();
		loadingOperations.Enqueue(new ConfigOperation());
		loadingOperations.Enqueue(new MenuLoadingOperation());

		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(loadingOperations);
	}
}
