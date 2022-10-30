using System;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class AlertPopup : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _message;
	[SerializeField] private Button _okButton;
	[SerializeField] private Button _cancelButton;
	[SerializeField] private Button _closeButton;

	private TaskCompletionSource<bool> _taskCompletion;
	private Canvas _canvas;

	public static AlertPopup Instance { get; private set; }

	private void Awake()
	{
		_canvas = GetComponent<Canvas>();
		_canvas.enabled = false;
		Instance = this;
		_okButton.onClick.AddListener(OnAccepted);
		_cancelButton.onClick.AddListener(OnCancelled);
		_closeButton.onClick.AddListener(OnCancelled);

	}

	public async Task<bool> AwaitForDecision(string text)
	{
		_message.text = text;
		_canvas.enabled = true;

		_taskCompletion = new TaskCompletionSource<bool>();
		bool result = await _taskCompletion.Task;
		_canvas.enabled = false;

		return result;
	}

	private void OnAccepted()
	{
		_taskCompletion.SetResult(true);
	}

	private void OnCancelled()
	{
		_taskCompletion.SetResult(true);
	}
}
