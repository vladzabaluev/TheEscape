using System.Collections.Generic;
using UnityEngine;
// ReSharper disable CommentTypo

public class Level1 : MonoBehaviour, IPauseHandler
{
	[SerializeField] private InGameHud _inGameHud;

	private bool IsPaused => ProjectContext.Instance.PauseManager.IsPaused;

	public string SceneName => Constants.Scenes.Level1;

	public void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		_inGameHud.QuitGame += GoToMainMenu;
	}

	private async void GoToMainMenu()
	{
		Debug.Log("Activate Quit");
		var operations = new Queue<ILoadingOperation>();
		operations.Enqueue(new ClearLevelOperation(SceneName));
		await ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		// TODO Realizovat' pauzu dlya objektov
	}
}
