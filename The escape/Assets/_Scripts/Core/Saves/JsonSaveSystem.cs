using System.IO;
using UnityEngine;

public class JsonSaveSystem
{
	private string _filePath;

	public JsonSaveSystem()
	{
		_filePath = Path.Combine(Application.streamingAssetsPath, "PlayerSaves.json");
	}

	public void Save(AppInfoContainer data)
	{
		var json = JsonUtility.ToJson(data);
		using (var writer = new StreamWriter(_filePath))
		{
			writer.WriteLine(json);
		}
	}

	public AppInfoContainer Load()
	{
		string json = "";
		using (var reader = new StreamReader(_filePath))
		{
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				json += line;
			}
		}

		if (string.IsNullOrEmpty(json))
		{
			return new AppInfoContainer();
		}

		return JsonUtility.FromJson<AppInfoContainer>(json);
	}
}
