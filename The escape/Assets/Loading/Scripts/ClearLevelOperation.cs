using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
// ReSharper disable CommentTypo

public class ClearLevelOperation : ILoadingOperation
{
	private readonly string _sceneName;
	public string Description => "Exit to the main menu...";

	public ClearLevelOperation(string sceneName)
	{
		_sceneName = sceneName;
	}

	public async UniTask Load(Action<float> onProgress)
	{
		// ReSharper disable once CommentTypo
		// TODO Zdes' mozhno dobavit' ochistku sceni i vigruzku igrovih objektov

		onProgress?.Invoke(0.34f);

		var loadOperation = SceneManager.LoadSceneAsync(Constants.Scenes.MainMenu, LoadSceneMode.Additive);
		while (loadOperation.isDone == false)
		{
			await UniTask.Delay(1);
		}
		onProgress?.Invoke(0.66f);

		var unloadOperation = SceneManager.UnloadSceneAsync(_sceneName);
		while (unloadOperation.isDone == false)
		{
			await UniTask.Delay(1);
		}
		onProgress?.Invoke(1f);
	}
}
