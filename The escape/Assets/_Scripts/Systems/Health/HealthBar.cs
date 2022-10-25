using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	[SerializeField] private PlayerHealthBN _playerHealthBN;

	private void Start()
	{
		_slider = _slider == null ? GetComponent<Slider>() : _slider;
		if (_slider == null)
			Debug.LogError("Slider not set to Health Bar");
		
		if (_playerHealthBN == null)
			Debug.LogError("Player Health Component not set to Health Bar");
		
		_slider.maxValue = _playerHealthBN.MaxHealth;

		_playerHealthBN.OnHealthChangedCallback += UpdateBarHUD;
		UpdateBarHUD();
	}

	private void UpdateBarHUD()
	{
		_slider.value = _playerHealthBN.Health;
	}
}
