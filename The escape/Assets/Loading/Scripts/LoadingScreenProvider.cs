using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;

public class LoadingScreenProvider : LocalAssetLoader
{
	public async UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
	{
		var loadingScreen = await Load();
		await loadingScreen.Load(loadingOperations);
		Unload();
	}

	private UniTask<LoadingScreen> Load()
	{
		return LoadInternal<LoadingScreen>("LoadingScreen");
	}

	private void Unload()
	{
		UnloadInternal();
	}
}
