using UnityEngine;

public class ProjectContext : MonoBehaviour
{
	public AppInfoContainer AppInfo;
	public LoadingScreenProvider LoadingScreenProvider { get; private set; }
	public PauseManager PauseManager { get; private set; }
	public static ProjectContext Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{ // Ёкземпл€р менеджера был найден
			Instance = this; // «адаем ссылку на экземпл€р объекта
			Initialize();
		}
		else
		{ // Ёкземпл€р объекта уже существует на сцене
			Destroy(this.gameObject); // ”дал€ем объект
		}
		//Instance = this;
		DontDestroyOnLoad(this);
	}

	public void Initialize()
	{
		LoadingScreenProvider = new LoadingScreenProvider();
		PauseManager = new PauseManager();
		AppInfo = new AppInfoContainer();
	}
}
