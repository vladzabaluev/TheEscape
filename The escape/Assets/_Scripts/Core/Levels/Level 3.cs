using UnityEngine;
// ReSharper disable CommentTypo

public class Level3 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level3.ToString();

	public override void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 3 is paused");
		else
			Debug.Log("Level 3 is unpaused");

		// Realizovat' pauzu dlya objektov3
	}
}
