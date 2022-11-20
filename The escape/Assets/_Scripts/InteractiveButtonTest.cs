using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractiveButtonTest : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private TextMeshProUGUI _textMeshPro;

	private PlayerInputActions _playerInputActions;
	private InputAction _touchInput;
	private InputAction _touchPosition;

	private Vector2 _startTouchPosition;
	private Vector2 _endTouchPosition;
	private Camera _mainCamera;

	private float _level;

	private void Awake()
	{
		_playerInputActions = new PlayerInputActions();
		_mainCamera = Camera.main;

		_button.onClick.AddListener(OnClick);

		string[] subStrings = _textMeshPro.text.Split(' ');
		_level = float.Parse(subStrings[2]);
	}

	private void OnEnable()
	{
		_playerInputActions.UI.Enable();
		_playerInputActions.GameProcess.Enable();

		_touchInput = _playerInputActions.GameProcess.TouchInput;
		_touchPosition = _playerInputActions.GameProcess.TouchPosition;

		_touchInput.performed += SaveStartTouchPosition;
		_touchInput.canceled += SaveEndTouchPosition;

		_touchInput.Enable();
		_touchPosition.Enable();
	}

	private void SaveEndTouchPosition(InputAction.CallbackContext obj)
	{

	}

	private void SaveStartTouchPosition(InputAction.CallbackContext obj)
	{
		Debug.Log("Start");
		//_startTouchPosition = _touchPosition.ReadValue<Vector2>();
		//Debug.Log(_startTouchPosition);
	}

	private void OnDisable()
	{
		_playerInputActions.UI.Disable();
		_playerInputActions.GameProcess.Disable();
		_touchInput.Disable();
		_touchPosition.Disable();
	}

	/*private void SaveStartTouchPosition(InputAction.CallbackContext obj)
	{
		_startTouchPosition = _touchPosition.ReadValue<Vector2>();
		Debug.Log(_startTouchPosition);
	}

	private void SaveEndTouchPosition(InputAction.CallbackContext obj)
	{
		_endTouchPosition = _touchPosition.ReadValue<Vector2>();
		Debug.Log(_endTouchPosition);
	}*/

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
