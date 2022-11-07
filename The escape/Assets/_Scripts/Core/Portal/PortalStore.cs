using System.Collections.Generic;
using UnityEngine;

public class PortalStore : MonoBehaviour
{
	public List<Portal> _portalList = new();

	private void Start()
	{
		for (var i = 0; i < _portalList.Count; i++)
		{
			if (i == _portalList.Count - 1)
			{
				_portalList[i].AnotherPortal = _portalList[0];
			}
			else
			{
				_portalList[i].AnotherPortal = _portalList[i + 1];
			}

			_portalList[i].gameObject.SetActive(false);
		}
	}
}
