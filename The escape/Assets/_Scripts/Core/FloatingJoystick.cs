using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class FloatingJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	[Header("Joystick Parameters")]
	[SerializeField] private float _handleRange = 1;
	[SerializeField] private float _deadZone;
	[SerializeField] private RectTransform _background;
	[SerializeField] private RectTransform _handle;

	[Space]
	[InputControl(layout = "Vector2")]
	[SerializeField] private string _controlPath;

	[Header("Joystick Anim Parameters")]
	[SerializeField] private float _stepForAlpha = 0.01f;
	[SerializeField] private float _timeUpdateAlpha = 0.01f;

	private RectTransform _baseRect;
	private Image _imageOfBackground;
	private Image _imageOfHandle;
	private Canvas _canvas;
	private Camera _cam;

	private Vector2 _input = Vector2.zero;

	private void Start()
	{
		HandleRange = _handleRange;
		DeadZone = _deadZone;

		_baseRect = GetComponent<RectTransform>();
		_canvas = GetComponentInParent<Canvas>();
		_imageOfBackground = _background.GetComponent<Image>();
		_imageOfHandle = _handle.GetComponent<Image>();

		if (_canvas == null)
			Debug.LogError("The Joystick is not placed inside a canvas");

		var center = new Vector2(0.5f, 0.5f);
		_background.pivot = center;
		_handle.anchorMin = center;
		_handle.anchorMax = center;
		_handle.pivot = center;
		_handle.anchoredPosition = Vector2.zero;

		_background.gameObject.SetActive(false);
	}

	public float HandleRange
	{
		get => _handleRange;
		set => _handleRange = Mathf.Abs(value);
	}

	public float DeadZone
	{
		get => _deadZone;
		set => _deadZone = Mathf.Abs(value);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData == null)
			throw new System.ArgumentNullException(nameof(eventData));

		StopAllCoroutines();

		_background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
		JoystickTurningOn();
		OnDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData == null)
			throw new System.ArgumentNullException(nameof(eventData));

		_cam = null;
		if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
			_cam = _canvas.worldCamera;

		Vector2 position = RectTransformUtility.WorldToScreenPoint(_cam, _background.position);
		Vector2 radius = _background.sizeDelta / 2;
		_input = (eventData.position - position) / (radius * _canvas.scaleFactor);

		SendValueToControl(_input);

		HandleInput(_input.magnitude, _input.normalized);
		_handle.anchoredPosition = _input * radius * _handleRange;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		StartCoroutine(JoystickTurningOff());

		SendValueToControl(Vector2.zero);
		_handle.anchoredPosition = Vector2.zero;
	}

	private void HandleInput(float magnitude, Vector2 normalised)
	{
		if (magnitude > _deadZone)
		{
			if (magnitude > 1)
			{
				_input = normalised;
			}
		}
		else
		{
			_input = Vector2.zero;
		}
	}

	private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _cam, out var localPoint))
		{
			Vector2 sizeDelta = _baseRect.sizeDelta;
			Vector2 pivotOffset = _baseRect.pivot * sizeDelta;
			return localPoint - (_background.anchorMax * sizeDelta) + pivotOffset;
		}

		return Vector2.zero;
	}

	private IEnumerator JoystickTurningOff()
	{
		float alpha = 1f;

		while (_imageOfBackground.color.a >= 0f)
		{
			alpha -= _stepForAlpha;
			_imageOfHandle.color = _imageOfBackground.color = new Color(1f, 1f, 1f, alpha);

			yield return new WaitForSeconds(_timeUpdateAlpha);

			if (_imageOfBackground.color.a <= 0)
			{
				_background.gameObject.SetActive(false);
			}
		}
	}

	private void JoystickTurningOn()
	{
		_background.gameObject.SetActive(true);
		_imageOfHandle.color = _imageOfBackground.color = new Color(1f, 1f, 1f, 1f);
	}

	protected override string controlPathInternal
	{
		get => _controlPath;
		set => _controlPath = value;
	}
}
