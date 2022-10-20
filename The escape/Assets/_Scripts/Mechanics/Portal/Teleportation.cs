using UnityEngine;

namespace _Scripts.Mechanics.Portal
{
    public class Teleportation : MonoBehaviour
    {
        private static bool isTeleported = false;
        public bool IsActivePortal = true;
        public GameObject PointTeleport;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsActivePortal)
            {
                if (!isTeleported)
                {
                    collision.gameObject.transform.position = PointTeleport.gameObject.transform.position;
                    isTeleported = true;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsActivePortal)
            {
                if (isTeleported)
                {
                    if (Vector2.Distance(collision.gameObject.transform.position, PointTeleport.gameObject.transform.position) >= 1)
                    {
                        isTeleported = false;
                    }
                }
            }
        }
    }
}
