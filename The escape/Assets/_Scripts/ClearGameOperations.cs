using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ClearGameOperations : ILoadingOperation
{
	public string Description => "Clearing...";

	public async Task Load(Action<float> onProgress)
	{
		onProgress?.Invoke(0.3f);

		var loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
		while (loadOp.isDone == false)
		{
			await Task.Delay(1);
		}
		onProgress?.Invoke(0.6f);

		var unloadOp = SceneManager.UnloadSceneAsync("Level");
		while (unloadOp.isDone == false)
		{
			await Task.Delay(1);
		}
		onProgress?.Invoke(1f);
	}
}
