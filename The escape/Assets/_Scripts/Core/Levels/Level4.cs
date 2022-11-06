using UnityEngine;

public class Level4 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.Level4;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 4 is paused");
		else
			Debug.Log("Level 4 is unpaused");
	}
}
