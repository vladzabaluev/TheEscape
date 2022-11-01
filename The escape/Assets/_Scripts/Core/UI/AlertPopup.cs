using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class AlertPopup : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private TextMeshProUGUI _message;
	[SerializeField] private TextMeshProUGUI _acceptText;
	[SerializeField] private TextMeshProUGUI _cancelText;
	[SerializeField] private Button _okButton;
	[SerializeField] private Button _cancelButton;

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
	}

	public async Task<bool> AwaitForDecision(string title, string message, string acceptText, string cancelText)
	{
		_title.text = title;
		_message.text = message;
		_acceptText.text = acceptText;
		_cancelText.text = cancelText;
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
		_taskCompletion.SetResult(false);
	}
}
