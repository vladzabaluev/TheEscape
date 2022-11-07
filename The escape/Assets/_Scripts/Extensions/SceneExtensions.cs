using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneExtensions
{
	public static T GetRoot<T>(this Scene scene) where T : MonoBehaviour
	{
		GameObject[] rootObjects = scene.GetRootGameObjects();

		T result = default;
		foreach (GameObject rootObject in rootObjects)
		{
			if (rootObject.TryGetComponent(out result))
			{
				break;
			}
		}

		return result;
	}
}
