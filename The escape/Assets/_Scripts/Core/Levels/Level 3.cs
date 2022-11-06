using UnityEngine;

public class Level3 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.Level3;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 3 is paused");
		else
			Debug.Log("Level 3 is unpaused");
	}
}
