//AppSponsor Unity Plugin 1.1.8
using System;
using System.ComponentModel;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

// This macros switches between Android and iOS implementations.
#if UNITY_ANDROID
using AppSponsorMobilePlugin = AppSponsorPluginAndroid;
#elif UNITY_IPHONE
using AppSponsorMobilePlugin = AppSponsorPluginiOS;
#endif

public class AppSponsorPlugin : MonoBehaviour {
	
	// These are the ad callback events that can be hooked into.
	public event Action<string> DidFailToLoad = delegate {};
	public event Action WillAppear = delegate {};
	public event Action<string> WillDisappear = delegate {};
	public event Action DidCacheInterstitial = delegate {};
	public event Action RewardedAdFinished = delegate {};
	
	// Zone ID for current SDK instance
	private string zoneId;
	// Internal ID used to identify them
	private string instanceId;
	
	private static int counter = 0;
	
	private static string CreateInstanceId(string zoneIdParam)
	{
		string identifier = zoneIdParam + "-" + counter.ToString();
		counter++;
		return identifier;
	}
	
	// Separate initializer required because subclasses of MonoBehaviour should be instantiated using its standard constructor
	public void InitializeWithZoneId(string zoneIdParam)
	{
		zoneId = zoneIdParam;
		
		instanceId = CreateInstanceId(zoneIdParam);
		gameObject.name = instanceId;
		name = instanceId;
		Debug.Log("creating ad plugin, object name is:" + gameObject.name);
		
		AppSponsorMobilePlugin.CreatePopupAd(instanceId, zoneId);
	}
	
	// Same for rewarded ad
	public void InitializeRewardedWithZoneId(string zoneIdParam, string uid)
	{
		zoneId = zoneIdParam;
		
		instanceId = CreateInstanceId(zoneIdParam);
		gameObject.name = instanceId;
		name = instanceId;
		Debug.Log("creating rewarded ad plugin object name is:" + gameObject.name);
		
		AppSponsorMobilePlugin.CreateRewardedAd(instanceId, zoneId, uid);
	}
	
	public void EnableLocation()
	{
		AppSponsorMobilePlugin.EnableLocation(instanceId);
	}
	
	public void SetExtras(string extrasJson )
	{
		AppSponsorMobilePlugin.SetExtras(instanceId, extrasJson);
	}
	
	public void PresentAd()
	{
		AppSponsorMobilePlugin.PresentAd(instanceId);
	}
	
	// Load ad for the user.
	public void Load()
	{
		AppSponsorMobilePlugin.Load(instanceId);
	}

	// Load and display ad for the user.
	public void LoadAndPresentAd()
	{
		AppSponsorMobilePlugin.LoadAndPresentAd(instanceId);
	}

	// Load and display ad for the user. Loading will either succeed in time or fail.
	public void LoadAndPresentAd(float timeoutSeconds)
	{
		AppSponsorMobilePlugin.LoadAndPresentAdWithCustomTimeout(instanceId, timeoutSeconds);
	}

	// Whether is Ad ready to be presented. Turns to true after DidCacheInterstitial().
	public bool IsReady()
	{
		return AppSponsorMobilePlugin.IsReady(instanceId);
	}
	
	// Status of rewarded ad.
	public int RewardedAdStatus()
	{
		return AppSponsorMobilePlugin.RewardedAdStatus(instanceId);
	}
	
	// Callback section.
	// Loading failed. Error description is provided.
	public void OnDidFailToLoad(string message)
	{
		DidFailToLoad(message);
	}
	
	// Rewarded ad finished it's lifecycle. Time to reward a user.
	public void OnRewardedAdFinished(string unusedMessage)
	{
		RewardedAdFinished();
	}
	
	// Parameter has no meaning and exists to preserve correct method signature
	public void OnWillAppear(string unusedMessage)
	{
		WillAppear();
	}
	
	// Parameter may be  'clicked' or 'close'
	public void OnWillDisappear(string reason)
	{
		WillDisappear(reason);
	}
	
	// Downloading of ad content done. User may now present it.
	public void OnDidCacheInterstitial(string unusedMessage)
	{
		DidCacheInterstitial();
	}
	
	// Following methods may be used to set targeting properties.
	public void SetCountry(string country)
	{
		AppSponsorMobilePlugin.SetCountry(instanceId, country);
	}	
	
	public void SetRegion(string region)
	{
		AppSponsorMobilePlugin.SetRegion(instanceId, region);
	}
	
	public void SetMetro(string metro)
	{
		AppSponsorMobilePlugin.SetMetro(instanceId, metro);
	}
	
	public void SetCity(string city)
	{
		AppSponsorMobilePlugin.SetCity(instanceId, city);
	}
	
	public void SetZip(string zip)
	{
		AppSponsorMobilePlugin.SetZip(instanceId, zip);
	}
	
	public void SetGender(string gender)
	{
		AppSponsorMobilePlugin.SetGender(instanceId, gender);
	}
	
	public void SetYob(string yob)
	{
		AppSponsorMobilePlugin.SetYob(instanceId, yob);
	}
	
	public void SetUCountry(string country)
	{
		AppSponsorMobilePlugin.SetUCountry(instanceId, country);
	}
	
	public void SetUCity(string city)
	{
		AppSponsorMobilePlugin.SetUCity(instanceId, city);
	}
	
	public void SetUZip(string zip)
	{
		AppSponsorMobilePlugin.SetUZip(instanceId, zip);
	}
	
	public void SetLongitude(string longitude)
	{
		AppSponsorMobilePlugin.SetLongitude(instanceId, longitude);
	}
	
	public void SetLatitude(string latitude)
	{
		AppSponsorMobilePlugin.SetLatitude(instanceId, latitude);
	}
	
	public void SetUID(string uid)
	{
		AppSponsorMobilePlugin.SetUID(instanceId, uid);
	}
	
	public void SetKeywords(string words)
	{
		AppSponsorMobilePlugin.SetKeywords(instanceId, words);
	}
	
	public void OnDestroy() 
	{
		AppSponsorMobilePlugin.Delete(instanceId);
	}
	
}