using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
	[SerializeField] private float _rotationSpeed = 200f;
	[SerializeField] private float _openSpeed = 0.005f;
	[SerializeField] private float _closeSpeed = 0.01f;
	[SerializeField] private bool _isIncreasing;

	private RectTransform _rectTransform;
	private Image _image;

	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		_image = _rectTransform.GetComponent<Image>();
		_isIncreasing = true;
	}

	private void Update()
	{
		_rectTransform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
		ChangeSize();
	}

	private void ChangeSize()
	{
		float currentSize = _image.fillAmount;

		if (currentSize < .30f && _isIncreasing)
		{
			_image.fillAmount += _openSpeed;
		}
		else if (currentSize >= .30f && _isIncreasing)
		{
			_isIncreasing = false;
		}
		else if (currentSize >= .02f && !_isIncreasing)
		{
			_image.fillAmount -= _closeSpeed;
		}
		else if (currentSize < .02f && !_isIncreasing)
		{
			_isIncreasing = true;
		}
	}
}
