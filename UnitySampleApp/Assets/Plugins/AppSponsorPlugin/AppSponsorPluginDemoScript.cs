using UnityEngine;

// Example script showing how you can easily call into the AppSponsorPlugin.
public class AppSponsorPluginDemoScript : MonoBehaviour {
	
	AppSponsorPlugin plugin;
	public string publicZoneIDiOS;
	public string publicZoneIDAndroid;
	public bool isRewarded;

	public GameObject consoleLabel;
	
	void OnClick()
	{
		plugin.LoadAndPresentAd(10000.0f);
	}
	
	void ConsoleLog(string text)
	{
		#if UNITY_ANDROID
		if (consoleLabel != null) consoleLabel.SendMessage("AppendText", publicZoneIDAndroid.Substring(0,5) + ":    " + text, SendMessageOptions.DontRequireReceiver); 
		#elif UNITY_IPHONE
		if (consoleLabel != null) consoleLabel.SendMessage("AppendText", publicZoneIDiOS.Substring(0,5) + "    " + text, SendMessageOptions.DontRequireReceiver); 
		#endif
		Debug.Log(text);
	}
	
	void Start()
	{
		ConsoleLog("Created plugin");
		plugin = gameObject.AddComponent<AppSponsorPlugin>();

		#if UNITY_ANDROID
		if (isRewarded) plugin.InitializeRewardedWithZoneId(publicZoneIDAndroid, "user");
		else plugin.InitializeWithZoneId(publicZoneIDAndroid);
		#elif UNITY_IPHONE
		if (isRewarded) plugin.InitializeRewardedWithZoneId(publicZoneIDiOS, "user"); 
		else plugin.InitializeWithZoneId(publicZoneIDiOS);
		#endif
		// NOTE: this is needed, so ads will be shown independently of location
		plugin.SetExtras("{\"country\":\"USA\"}");
		RegisterForEvents();

	}
	
	void RegisterForEvents()
	{
		Debug.Log("Registering for AppSponsor Events");
		plugin.DidFailToLoad += HandleDidFailToLoad;
		plugin.WillAppear += HandleWillAppear;
		plugin.WillDisappear += HandleWillDisappear;
		plugin.DidCacheInterstitial += HandleDidCacheInterstitial;
		plugin.RewardedAdFinished += HandleRewardedAdFinished;
	}
	
	void OnDestroy()
	{
		Debug.Log("Unregistering for AppSponsor Events");
		plugin.DidFailToLoad -= HandleDidFailToLoad;
		plugin.WillAppear -= HandleWillAppear;
		plugin.WillDisappear -= HandleWillDisappear;
		plugin.DidCacheInterstitial -= HandleDidCacheInterstitial;
		plugin.RewardedAdFinished -= HandleRewardedAdFinished;
	}

	public void HandleDidFailToLoad(string message)
	{
		ConsoleLog("DidFailToLoad, message:" + message);
	}

	public void HandleWillAppear()
	{
		ConsoleLog("WillAppear");
	}
	
	public void HandleWillDisappear(string reason)
	{
		ConsoleLog("WillDisappear, message: " + reason);
	}
	
	public void HandleRewardedAdFinished()
	{
		ConsoleLog("RewardedAdFinished");
	}

	public void HandleDidCacheInterstitial()
	{
		ConsoleLog("DidCacheInterstitial");
		/*if (plugin.IsReady()) 
		{
			ConsoleLog("Presenting Popup Ad");
			plugin.PresentAd();
		}
		else ConsoleLog("Popup Ad Is Not ready");*/
	}
}