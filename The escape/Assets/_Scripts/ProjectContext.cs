using UnityEngine;

public class ProjectContext : MonoBehaviour
{
	public AppInfoContainer AppInfo { get; set; }
	public LoadingScreenProvider LoadingScreenProvider { get; private set; }
	public PauseManager PauseManager { get; private set; }
	public static ProjectContext Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Initialize();
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);
	}

	public void Initialize()
	{
		LoadingScreenProvider = new LoadingScreenProvider();
		PauseManager = new PauseManager();
		AppInfo = new AppInfoContainer();
	}
}
