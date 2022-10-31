using UnityEngine;

public class DeviceInfoProvider
{
	public static string GetDeviceId()
	{
		return SystemInfo.deviceUniqueIdentifier;
	}
}
