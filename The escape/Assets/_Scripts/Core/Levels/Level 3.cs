// ReSharper disable CommentTypo
public class Level3 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level3.ToString();

	public void Initialize()
	{
		ProjectContext.Instance.PauseManager.Register(this);

		InGameHud.QuitGame += GoToMainMenu;
		InGameHud.ReloadLevel += Reload;
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		// Realizovat' pauzu dlya objektov3
	}
}
