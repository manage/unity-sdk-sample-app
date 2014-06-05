using System.Runtime.InteropServices;
using UnityEngine;

public class AppSponsorPluginiOS {
	
	// These are the interface to native implementation calls for iOS.

	[DllImport("__Internal")]
	private static extern void _CreatePopupAd(string instanceId, string zoneId);
	[DllImport("__Internal")]
	private static extern void _CreateRewardedAd(string instanceId, string zoneId, string uid);
	[DllImport("__Internal")]
	private static extern void _Load(string instanceId);
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
	
	public static void SetCountry(string instanceId, string country)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'country':'"+country+"'}");
	}	

	public static void SetRegion(string instanceId, string region)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'region':'"+region+"'}");
	}

	public static void SetMetro(string instanceId, string metro)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'metro':'"+metro+"'}");
	}

	public static void SetCity(string instanceId, string city)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'city':'"+city+"'}");
	}

	public static void SetZip(string instanceId, string zip)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'zip':'"+zip+"'}");
	}

	public static void SetGender(string instanceId, string gender)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'gender':'"+gender+"'}");
	}

	public static void SetYob(string instanceId, string yob)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'yob':'"+yob+"'}");
	}

	public  static void SetUCountry(string instanceId, string country)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'u_country':'"+country+"'}");
	}

	public static void SetUCity(string instanceId, string city)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'u_city':'"+city+"'}");
	}

	public static void SetUZip(string instanceId, string zip)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'u_zip':'"+zip+"'}");
	}

	public static void SetLongitude(string instanceId, string longitude)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'longitude':'"+longitude+"'}");
	}

	public static void SetLatitude(string instanceId, string latitude)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'latitude':'"+latitude+"'}");
	}

	public static void SetUID(string instanceId, string uid)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'pub_uid':'"+uid+"'}");
	}

	public static void SetKeywords(string instanceId, string words)
	{
		AppSponsorPluginiOS.SetExtras(instanceId, "{'keywords':'"+words+"'}");
	}
}