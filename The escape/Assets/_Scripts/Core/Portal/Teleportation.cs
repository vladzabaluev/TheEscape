using UnityEngine;

public class Teleportation : MonoBehaviour
{
	private static bool s_isTeleported;

	public GameObject PointTeleport;

	public bool IsActivePortal = true;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsActivePortal)
		{
			if (!s_isTeleported)
			{
				collision.gameObject.transform.position = PointTeleport.gameObject.transform.position;
				s_isTeleported = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (IsActivePortal)
		{
			if (s_isTeleported)
			{
				if (Vector2.Distance(collision.gameObject.transform.position, PointTeleport.gameObject.transform.position) >= 1)
				{
					s_isTeleported = false;
				}
			}
		}
	}
}
