using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalStore : MonoBehaviour
{
    public List<Portal> portalList = new List<Portal>();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < portalList.Count; i++)
        {
            if (i == portalList.Count - 1)
                portalList[i].anotherPortal = portalList[0];
            else
                portalList[i].anotherPortal = portalList[i + 1];
            portalList[i].gameObject.SetActive(false);
        }
    }
}