using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
// ReSharper disable CommentTypo

public class LevelLoadingOperation : ILoadingOperation
{
	private readonly int _levelIndex;
	public string Description => $"Loading {_levelIndex} level..." ;

	public LevelLoadingOperation(int levelIndex)
	{
		_levelIndex = levelIndex;
	}

	public async UniTask Load(Action<float> onProgress)
	{
		onProgress?.Invoke(0.4f);
		var loadOperation = SceneManager.LoadSceneAsync(Constants.Scenes.Level1.ToString(), LoadSceneMode.Single);

		while (loadOperation.isDone == false)
		{
			await UniTask.Delay(1);
		}
		onProgress?.Invoke(0.6f);

		// TODO Realizovat' zagruzku urovnya po indeksu

		var scene = SceneManager.GetSceneByName(Constants.Scenes.Level1.ToString());
		var level = scene.GetRoot<Level1>();
		onProgress?.Invoke(0.8f);
		level.Initialize();
		onProgress?.Invoke(1.0f);
	}
}
