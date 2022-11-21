using System;
using Cysharp.Threading.Tasks;

public class ConfigOperation : ILoadingOperation
{
	public string Description => "Configuration loading...";

	private readonly JsonSaveSystem _saveSystem;

	public ConfigOperation()
	{
		_saveSystem = new JsonSaveSystem();
	}

	public async UniTask Load(Action<float> onProgress)
	{
		float loadingTime = UnityEngine.Random.Range(1.5f, 2.5f);
		const int steps = 3;

		ProjectContext.Instance.AppInfo = _saveSystem.Load();
		onProgress?.Invoke(1 / loadingTime);

		for (int i = 2; i <= steps; i++)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(loadingTime / steps));
			onProgress?.Invoke(i / loadingTime);
		}

		onProgress?.Invoke(1f);
	}
}
