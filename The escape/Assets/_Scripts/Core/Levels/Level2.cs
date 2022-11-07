using UnityEngine;

public class Level2 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.Level2;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 2 is paused");
		else
			Debug.Log("Level 2 is unpaused");
	}
}
