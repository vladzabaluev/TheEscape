// ReSharper disable CommentTypo
public class Level5 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level5.ToString();
	public override int LevelIndex => (int)Constants.Scenes.Level5;

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
