using UnityEngine;

// Example script showing how you can easily call into the ManagePlugin.
public class ManagePluginDemoScript : MonoBehaviour {
	
	ManagePlugin plugin;
	public string publicZoneIDiOS;
	public string publicZoneIDAndroid;
	public bool isRewarded;

	public string publicAdIDiOS;
	public string publicAdIDAndroid;

	public GameObject consoleLabel;
	
	public void LoadAndPresent()
	{
		plugin.LoadAndPresentAd(10000.0f);

	}

	public void Load()
	{
		plugin.Load();
	}

	public void Present()
	{
		plugin.PresentAd();
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
		plugin = gameObject.AddComponent<ManagePlugin>();

		#if UNITY_ANDROID
		if (isRewarded) plugin.InitializeRewardedWithZoneId(publicZoneIDAndroid, "user");
		else plugin.InitializeWithZoneId(publicZoneIDAndroid);
		#elif UNITY_IPHONE
		if (isRewarded) plugin.InitializeRewardedWithZoneId(publicZoneIDiOS, "user"); 
		else plugin.InitializeWithZoneId(publicZoneIDiOS);
		#endif
		// NOTE: this is needed, so ads will be shown independently of location

		plugin.SetExtras("{\"country\":\"USA\"}");

		#if UNITY_ANDROID
		if (publicAdIDAndroid != null) plugin.SetAdId(publicAdIDAndroid);
		#elif UNITY_IPHONE
		if (publicAdIDiOS != null) plugin.SetAdId(publicAdIDiOS);
		#endif

		RegisterForEvents();

	}
	
	void RegisterForEvents()
	{
		Debug.Log("Registering for Manage Events");
		plugin.DidFailToLoad += HandleDidFailToLoad;
		plugin.WillAppear += HandleWillAppear;
		plugin.WillDisappear += HandleWillDisappear;
		plugin.DidCacheInterstitial += HandleDidCacheInterstitial;
		plugin.RewardedAdFinished += HandleRewardedAdFinished;
	}
	
	void OnDestroy()
	{
		Debug.Log("Unregistering for Manage Events");
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