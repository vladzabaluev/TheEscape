using UnityEngine;

public class Level1 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.NewLevel;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
		{
			Debug.Log("Level 1 is paused");
			Time.timeScale = 0;
		}
		else
		{
			Debug.Log("Level 1 is unpaused");
			Time.timeScale = 1;
		}
	}
}
