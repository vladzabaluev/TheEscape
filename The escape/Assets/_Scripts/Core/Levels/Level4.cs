using UnityEngine;
// ReSharper disable CommentTypo

public class Level4 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level4.ToString();

	public override void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 4 is paused");
		else
			Debug.Log("Level 4 is unpaused");

		// Realizovat' pauzu dlya objektov4
	}
}
