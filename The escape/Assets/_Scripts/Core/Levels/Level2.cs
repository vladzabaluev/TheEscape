using UnityEngine;

public class Level2 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.NextLevel;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
		{
			Debug.Log("Level 2 is paused");
			Time.timeScale = 0;
		}
		else
		{
			Debug.Log("Level 2 is unpaused");
			Time.timeScale = 1;
		}
	}
}
