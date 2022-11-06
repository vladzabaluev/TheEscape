using UnityEngine;
// ReSharper disable CommentTypo

public class Level4 : Level, IPauseHandler
{
	public override string SceneName => Constants.Scenes.Level4.ToString();

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 4 is paused");
		else
			Debug.Log("Level 4 is unpaused");

		// Realizovat' pauzu dlya objektov4
	}
}
