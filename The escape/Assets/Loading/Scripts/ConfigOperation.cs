using System;
using Cysharp.Threading.Tasks;

// TODO Implement config loading.
public class ConfigOperation : ILoadingOperation
{
	public string Description => "Configuration loading...";

	private AppInfoContainer _appInfoContainer;

	public ConfigOperation()
	{
		
	}

	public async UniTask Load(Action<float> onProgress)
	{
		float loadingTime = UnityEngine.Random.Range(1.5f, 2.5f);
		const int steps = 4;

		for (int i = 1; i <= steps; i++)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(loadingTime / steps));
			onProgress?.Invoke(i / loadingTime);
		}

		onProgress?.Invoke(1f);
	}
}
