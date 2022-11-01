using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	[SerializeField] protected InGameHud InGameHud;

	protected bool IsPaused => ProjectContext.Instance.PauseManager.IsPaused;

	public virtual string SceneName => null;
	public virtual int LevelIndex => 0;

	protected async void Reload()
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new LevelLoadingOperation(SceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	protected async void GoToMainMenu()
	{
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new ClearLevelOperation(SceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}
}
