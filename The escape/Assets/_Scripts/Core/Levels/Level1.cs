using UnityEngine;
// ReSharper disable CommentTypo

public class Level1 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.Level1;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 1 is paused");
		else
			Debug.Log("Level 1 is unpaused");

		// TODO Realizovat' pauzu dlya objektov
	}
}
