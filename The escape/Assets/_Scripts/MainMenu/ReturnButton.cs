using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
	private Canvas _canvas;
	private Button _button;

	private void Start()
	{
		_canvas = GetComponentInParent<Canvas>();
		_button = GetComponent<Button>();

		_button.onClick.AddListener(OnButtonClick);
	}

	private void OnButtonClick()
	{
		_canvas.enabled = false;
	}
}
