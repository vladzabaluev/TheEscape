using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MenuLoadingOperation : ILoadingOperation
{
	public string Description => "Loading main menu...";

	public async UniTask Load(Action<float> onProgress)
	{
		onProgress?.Invoke(0.5f);
		var loadOperation = SceneManager.LoadSceneAsync(Constants.Scenes.MainMenu.ToString(), LoadSceneMode.Additive);
		while (loadOperation.isDone == false)
		{
			await UniTask.Delay(1);
		}

		onProgress?.Invoke(1f);
	}
}
