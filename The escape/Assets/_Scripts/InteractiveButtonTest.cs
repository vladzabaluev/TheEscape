using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveButtonTest : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private TextMeshProUGUI _textMeshPro;

	private PlayerInputActions _playerInputActions;
	private float _level;

	private void Awake()
	{
		_playerInputActions = new PlayerInputActions();

		_button.onClick.AddListener(OnClick);

		string[] subStrings = _textMeshPro.text.Split(' ');
		_level = float.Parse(subStrings[2]);
	}

	private void OnEnable()
	{
		_playerInputActions.UI.Enable();
	}

	private void OnDisable()
	{
		_playerInputActions.UI.Disable();
	}

	private void OnClick()
	{
		var screenPosition = _playerInputActions.UI.Point.ReadValue<Vector2>();
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPosition, null, out var localPoint);

		if (localPoint.x >= 0)
			Increase();
		else
			Decrease();
	}

	private void Increase()
	{
		_level += 5f;
		_level = Mathf.Clamp(_level, 0f, 100f);
		_textMeshPro.text = $"< Music: {_level} >";
	}

	private void Decrease()
	{
		_level -= 5f;
		_level = Mathf.Clamp(_level, 0f, 100f);
		_textMeshPro.text = $"< Music: {_level} >";
	}
}
