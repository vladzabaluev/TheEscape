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

		switch (_sceneName)
		{
			case "Demoscene":
				var level1 = scene.GetRoot<Level1>();
				onProgress?.Invoke(0.8f);
				//level1.Initialize();
				break;

			case "NextLevel":
				var level2 = scene.GetRoot<Level2>();
				onProgress?.Invoke(0.8f);
				//level2.Initialize();
				break;

				//case "Level3":
				//	var level3 = scene.GetRoot<Level3>();
				//	onProgress?.Invoke(0.8f);
				//	level3.Initialize();
				//	break;

				//case "Level4":
				//	var level4 = scene.GetRoot<Level4>();
				//	onProgress?.Invoke(0.8f);
				//	level4.Initialize();
				//	break;

				//case "Level5":
				//	var level5 = scene.GetRoot<Level5>();
				//	onProgress?.Invoke(0.8f);
				//	level5.Initialize();
				//	break;
		}

		onProgress?.Invoke(1.0f);
	}
}
