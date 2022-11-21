using System.IO;
using UnityEngine;

public class JsonSaveSystem
{
	private readonly string _filePath;

	public JsonSaveSystem()
	{
		_filePath = Path.Combine(Application.persistentDataPath, "PlayerSaves.json");
		if (File.Exists(_filePath))
			return;

		FileStream fileStream = File.Create(_filePath);
		fileStream.Dispose();
		AppInfoContainer newData = new()
		{
			UnlockedLevelsCount = 1
		};
		Save(newData);
	}

	public void Save(AppInfoContainer data)
	{
		string json = JsonUtility.ToJson(data);
		using var writer = new StreamWriter(_filePath);
		writer.WriteLine(json);
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
