using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
	private Button _levelSelect;
	private TextMeshProUGUI _levelNumber;

	private void Start()
	{
		_levelSelect = GetComponent<Button>();
		_levelNumber = GetComponentInChildren<TextMeshProUGUI>();

		_levelSelect.onClick.AddListener(OnLevelSelectedClick);
	}

	private void OnLevelSelectedClick()
	{
		int.TryParse(_levelNumber.text, out int indexLevel);
		SceneManager.LoadScene(indexLevel + 1, LoadSceneMode.Single);
	}
}
