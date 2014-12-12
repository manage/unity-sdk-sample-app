using System.Runtime.InteropServices;
using UnityEngine;

public class ManagePluginiOS {
	
	// These are the interface to native implementation calls for iOS.

	[DllImport("__Internal")]
	private static extern void _CreatePopupAd(string instanceId, string zoneId);
	[DllImport("__Internal")]
	private static extern void _CreateRewardedAd(string instanceId, string zoneId, string uid);
	[DllImport("__Internal")]
	private static extern void _Load(string instanceId);
	[DllImport("__Internal")]
	private static extern void _LoadAndPresentAd(string instanceId);
	[DllImport("__Internal")]
	private static extern void _LoadAndPresentAdWithTimeout(string instanceId, string timeoutSeconds);
	[DllImport("__Internal")]
	private static extern void _PresentAd(string instanceId);
	[DllImport("__Internal")]
	private static extern bool _IsReady(string instanceId);
	[DllImport("__Internal")]
	private static extern int  _RewardedAdStatus(string instanceId);
	[DllImport("__Internal")]
	private static extern void _EnableLocation(string instanceId);
	[DllImport("__Internal")]
	private static extern void _SetExtras(string instanceId, string extrasJson);
	[DllImport("__Internal")]
	private static extern void _SetAdId(string instanceId, string adId);
	[DllImport("__Internal")]
	private static extern void _Delete(string instanceId);

	
	public static void CreatePopupAd(string instanceId, string zoneId)
	{
		_CreatePopupAd(instanceId, zoneId);
	}
	
	public static void CreateRewardedAd(string instanceId, string zoneId, string uid)
	{
		_CreateRewardedAd(instanceId, zoneId, uid);
	}
	
	public static void Load(string instanceId)
	{
		_Load(instanceId);
	}

	public static void LoadAndPresentAd(string instanceId)
	{
		_LoadAndPresentAd(instanceId);
	}

	public static void LoadAndPresentAdWithCustomTimeout(string instanceId, float timeoutSeconds)
	{
		_LoadAndPresentAdWithTimeout(instanceId, "" + timeoutSeconds);
	}
	
	public static void PresentAd(string instanceId)
	{
		_PresentAd(instanceId);
	}
	
	public static bool IsReady(string instanceId)
	{
		return _IsReady(instanceId);
	}
	
	public static int RewardedAdStatus(string instanceId)
	{
		return _RewardedAdStatus(instanceId);
	}
	
	public static void EnableLocation(string instanceId)
	{
		_EnableLocation(instanceId);
	}

	public static void Delete(string instanceId)
	{
		_Delete(instanceId);
	}

	public static void SetExtras(string instanceId, string extrasJson)
	{
		_SetExtras(instanceId, extrasJson);
	}

	public static void SetAdId(string instanceId, string adId)
	{
		_SetAdId(instanceId, adId);
	}
	
	public static void SetCountry(string instanceId, string country)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'country':'"+country+"'}");
	}	

	public static void SetRegion(string instanceId, string region)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'region':'"+region+"'}");
	}

	public static void SetMetro(string instanceId, string metro)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'metro':'"+metro+"'}");
	}

	public static void SetCity(string instanceId, string city)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'city':'"+city+"'}");
	}

	public static void SetZip(string instanceId, string zip)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'zip':'"+zip+"'}");
	}

	public static void SetGender(string instanceId, string gender)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'gender':'"+gender+"'}");
	}

	public static void SetYob(string instanceId, string yob)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'yob':'"+yob+"'}");
	}

	public  static void SetUCountry(string instanceId, string country)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'u_country':'"+country+"'}");
	}

	public static void SetUCity(string instanceId, string city)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'u_city':'"+city+"'}");
	}

	public static void SetUZip(string instanceId, string zip)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'u_zip':'"+zip+"'}");
	}

	public static void SetLongitude(string instanceId, string longitude)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'longitude':'"+longitude+"'}");
	}

	public static void SetLatitude(string instanceId, string latitude)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'latitude':'"+latitude+"'}");
	}

	public static void SetUID(string instanceId, string uid)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'pub_uid':'"+uid+"'}");
	}

	public static void SetKeywords(string instanceId, string words)
	{
		ManagePluginiOS.SetExtras(instanceId, "{'keywords':'"+words+"'}");
	}
}