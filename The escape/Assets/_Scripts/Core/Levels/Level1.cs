using UnityEngine;
// ReSharper disable CommentTypo

public class Level1 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level1.ToString();

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
			Debug.Log("Level 1 is paused");
		else
			Debug.Log("Level 1 is unpaused");

		// TODO Realizovat' pauzu dlya objektov
	}
}
