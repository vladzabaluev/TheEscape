using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
            if (!isTeleported)
            {
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
