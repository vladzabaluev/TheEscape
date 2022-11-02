using UnityEngine;
// ReSharper disable CommentTypo

public class Level2 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level2.ToString();

	/*
	public override void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}*/

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 2 is paused");
		else
			Debug.Log("Level 2 is unpaused");

		// Realizovat' pauzu dlya objektov2
	}
}
