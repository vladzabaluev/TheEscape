using UnityEngine;

public class Level5 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.Level5;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 5 is paused");
		else
			Debug.Log("Level 5 is unpaused");
	}
}
