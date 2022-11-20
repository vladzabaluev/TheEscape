using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private Slider slider;

	//public Gradient gradient;
	private Image fill;

	private void Awake()
	{
		slider = GetComponent<Slider>();
		fill = GetComponent<Image>();
	}

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
		//fill.color = gradient.Evaluate(1f);
	}

	public void SetHealth(int health)
	{
		slider.value = health;
		//fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
