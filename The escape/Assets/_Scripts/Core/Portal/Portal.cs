using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private float _minDistanceToTeleportAgain = 1f;

	private static bool s_isTeleported;

	public Portal AnotherPortal;
	public bool IsActivePortal = true;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (AnotherPortal.IsActivePortal)
		{
			if (!s_isTeleported)
			{
				collision.gameObject.transform.position = AnotherPortal.gameObject.transform.position;
				s_isTeleported = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (s_isTeleported)
		{
			if (Vector2.Distance(collision.gameObject.transform.position,
				AnotherPortal.gameObject.transform.position) >= _minDistanceToTeleportAgain)
			{
				s_isTeleported = false;
			}
		}
	}
}
