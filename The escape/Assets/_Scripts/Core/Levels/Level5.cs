using UnityEngine;
// ReSharper disable CommentTypo

public class Level5 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level5.ToString();

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 5 is paused");
		else
			Debug.Log("Level 5 is unpaused");

		// Realizovat' pauzu dlya objektov5
	}
}
