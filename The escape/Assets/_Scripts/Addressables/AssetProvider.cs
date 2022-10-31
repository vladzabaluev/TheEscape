using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AssetProvider : ILoadingOperation
{
	public string Description => "Assets initialization...";

	private bool _isReady;

	public async UniTask Load(Action<float> onProgress)
	{
		var operation = Addressables.InitializeAsync();
		await operation.Task;
		_isReady = true;
	}

	public async UniTask<SceneInstance> LoadSceneAdditive(string sceneId)
	{
		await WaitUntilReady();
		var op = Addressables.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
		return await op.Task;
	}

	public async UniTask UnloadSceneAdditive(SceneInstance scene)
	{
		await WaitUntilReady();
		var op = Addressables.UnloadSceneAsync(scene);
		await op.Task;
	}

	private async UniTask WaitUntilReady()
	{
		while (_isReady == false)
		{
			await UniTask.Yield();
		}
	}
}
