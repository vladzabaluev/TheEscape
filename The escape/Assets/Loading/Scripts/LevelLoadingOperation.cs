using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LevelLoadingOperation : ILoadingOperation
{
	private readonly string _sceneName;
	public string Description => $"Loading {_sceneName} level...";

	public LevelLoadingOperation(string sceneName)
	{
		_sceneName = sceneName;
	}

	public async UniTask Load(Action<float> onProgress)
	{
		onProgress?.Invoke(0.4f);

		var loadOperation = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
		while (loadOperation.isDone == false)
		{
			await UniTask.Delay(1);
		}
		onProgress?.Invoke(0.65f);

		var scene = SceneManager.GetSceneByName(_sceneName);

		var level1 = scene.GetRoot<Level1>();
		onProgress?.Invoke(0.8f);
		//level1.Initialize();

		onProgress?.Invoke(1.0f);
	}
}
