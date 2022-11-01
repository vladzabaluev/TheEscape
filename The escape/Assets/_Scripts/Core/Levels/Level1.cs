// ReSharper disable CommentTypo

public class Level1 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level1.ToString();

	public void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		// TODO Realizovat' pauzu dlya objektov
	}
}
