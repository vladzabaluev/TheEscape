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

		switch (currentSize)
		{
			case < 0.30f when _isIncreasing:
				_image.fillAmount += _openSpeed;
				break;
			case >= 0.30f when _isIncreasing:
				_isIncreasing = false;
				break;
			case >= 0.02f when !_isIncreasing:
				_image.fillAmount -= _closeSpeed;
				break;
			case < 0.02f when !_isIncreasing:
				_isIncreasing = true;
				break;
		}
	}
}
