using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Level, IPauseHandler
{
	public override Constants.Scenes SceneName => Constants.Scenes.NextLevel;

	void IPauseHandler.SetPaused(bool isPaused)
	{
		if (isPaused)
			Debug.Log("Level 1 is paused");
		else
			Debug.Log("Level 1 is unpaused");

		// TODO Realizovat' pauzu dlya objektov
	}
}
