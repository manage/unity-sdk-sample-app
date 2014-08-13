using System.Runtime.InteropServices;
using System;
using System.ComponentModel;
using System.Collections;
using UnityEngine;


public class AppSponsorPluginAndroid {

#if UNITY_ANDROID
	private static Hashtable popupAds = new Hashtable();

	public static void CreatePopupAd(string instanceId, string zoneId)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginFactoryClass = new AndroidJavaClass("com.appsponsor.unity.AppSponsorPluginFactory");
		AndroidJavaObject pluginInstance = pluginFactoryClass.CallStatic<AndroidJavaObject>("createPopupAdPluginInstance", new object[3] {activity, zoneId, instanceId});
		AppSponsorPluginAndroid.RegisterInstance(instanceId, pluginInstance);
	}
	
	public static void CreateRewardedAd(string instanceId, string zoneId, string uid)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginFactoryClass = new AndroidJavaClass("com.appsponsor.unity.AppSponsorPluginFactory");
		AndroidJavaObject pluginInstance = pluginFactoryClass.CallStatic<AndroidJavaObject>("createRewardedAdPluginInstance", new object[4] {activity, zoneId, uid, instanceId});
		AppSponsorPluginAndroid.RegisterInstance(instanceId, pluginInstance);
	}
	
	public static void Load(string instanceId)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("load");
	}

	public static void LoadAndPresentAd(string instanceId)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("loadAndPresentAd");
	}
	
	public static void LoadAndPresentAdWithCustomTimeout(string instanceId, float timeoutSeconds)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		string timeout = "" + timeoutSeconds;
		pluginInstance.Call("loadAndPresentAdWithTimeout", new object[1] {timeout});
	}

	public static void PresentAd(string instanceId)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("presentAd");
	}
	
	public static bool IsReady(string instanceId)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		return pluginInstance.Call<Boolean>("isReady");
	}
	
	public static int RewardedAdStatus(string instanceId)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		return pluginInstance.Call<int>("rewardedAdStatus");
	}

	private static void RegisterInstance(string instanceId, AndroidJavaObject instance)
	{
		popupAds[instanceId] = instance;
	}

	private static void UnRegisterInstance(string instanceId)
	{
		popupAds.Remove(instanceId);
	}

	private static AndroidJavaObject ResolveInstance(string instanceId)
	{
		AndroidJavaObject instance = (AndroidJavaObject)popupAds[instanceId];
		if(instance != null) return instance;

		// TODO: Throw exception if resolved instance is null. Will allow to avoid null checks in calling code
		return null;
	}
	
	public static void EnableLocation(string instanceId)
	{
		// Android API does not support this
	}

	public static void Delete(string instanceId)
	{
		UnRegisterInstance(instanceId);
	}
	
	public static void SetExtras(string instanceId, string extrasJson)
	{
		// Android API does not support this
	}
		
	public static void SetCountry(string instanceId, string country)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setCountry", new object[1] {country});
	}

	public static void SetRegion(string instanceId, string region)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setRegion", new object[1] {region});
	}
	
	public static void SetMetro(string instanceId, string metro)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setMetro", new object[1] {metro});
	}
	
	public static void SetCity(string instanceId, string city)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setCity", new object[1] {city});
	}
	
	public static void SetZip(string instanceId, string zip)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setZip", new object[1] {zip});
	}
	
	public static void SetGender(string instanceId, string gender)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setGender", new object[1] {gender});
	}
	
	public static void SetYob(string instanceId, string yob)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setYob", new object[1] {yob});
	}
	
	public static void SetUCountry(string instanceId, string country)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setUCountry", new object[1] {country});
	}
	
	public static void SetUCity(string instanceId, string city)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setUCity", new object[1] {city});
	}
	
	public static void SetUZip(string instanceId, string zip)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setUZip", new object[1] {zip});
	}
	
	public static void SetLongitude(string instanceId, string longitude)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setLongitude", new object[1] {longitude});
	}

	public static void SetLatitude(string instanceId, string latitude)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setLatitude", new object[1] {latitude});
	}

	public static void SetUID(string instanceId, string uid)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setPubUserID", new object[1] {uid});
	}

	public static void SetKeywords(string instanceId, string words)
	{
		AndroidJavaObject pluginInstance = AppSponsorPluginAndroid.ResolveInstance(instanceId);
		pluginInstance.Call("setKeywords", new object[1] {words});
	}
#endif
}