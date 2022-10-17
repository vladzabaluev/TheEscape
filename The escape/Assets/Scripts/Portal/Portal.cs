using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private static bool isTeleported = false;
    public bool IsActivePortal = true;
    public GameObject PointTeleport;

    public Portal nextPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsActivePortal)
        {
            if (!isTeleported)
            {
                collision.gameObject.transform.position = PointTeleport.transform.position;
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
                isTeleported = false;
            }
        }
    }
}