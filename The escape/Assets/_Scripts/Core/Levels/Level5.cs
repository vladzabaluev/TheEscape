// ReSharper disable CommentTypo
public class Level5 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level5.ToString();

	public void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		// Realizovat' pauzu dlya objektov5
	}
}
