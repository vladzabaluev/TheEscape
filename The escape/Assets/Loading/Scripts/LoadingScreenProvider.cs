using System.Collections.Generic;
using Cysharp.Threading.Tasks;

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
