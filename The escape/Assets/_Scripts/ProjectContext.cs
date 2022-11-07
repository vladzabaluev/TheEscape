using UnityEngine;

public class ProjectContext : MonoBehaviour
{
	public AppInfoContainer AppInfo;
	public LoadingScreenProvider LoadingScreenProvider { get; private set; }
	public PauseManager PauseManager { get; private set; }
	public static ProjectContext Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(this);
	}

	public void Initialize()
	{
		LoadingScreenProvider = new LoadingScreenProvider();
		PauseManager = new PauseManager();
		AppInfo = new AppInfoContainer();
	}
}
