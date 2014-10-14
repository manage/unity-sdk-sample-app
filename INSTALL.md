# UNITY INTEGRATION GUIDE


### Overview
This guide provides integration instructions for the Manage Publisher SDK.  If you need support or have any questions, feel free to email us at [support@appsponsor.com](mailto:support@appsponsor.com)

Requirements and Dependencies:  

- Android version 2.3
- iOS version 6.0

### 1. Download and Install the SDK

The SDK for Unity is available once you [sign up](https://appsponsor.com/user/registration).  The Manage SDK includes everything you need to serve full screen interstitial, video, and playable ad units.

The SDK for Unity can be downloaded here: [ManageSDK-Unity-v3.1.zip](/downloads/ManageSDK-Unity-v3.1.unitypackage).

To see an example implementation, please see our [sample app](https://github.com/manage/unity-sdk-sample-app).

### 2. Import the Manage Publisher plugin into your project  

Unzip the Unity plugin zipfile into a directory.

In the Unity editor, `Import Package` as a `Custom Package`.

![](https://s3.amazonaws.com/cdn.manage.com/appsponsor/documentation/unity/unity_installing_sdk_1.png)

Select **AppSponsorUnityPlugin.unitypackage**

![](https://s3.amazonaws.com/cdn.manage.com/appsponsor/documentation/unity/unity_installing_sdk_2.png)

Click the `Import` button to bring in all the files from the plugin

![](https://s3.amazonaws.com/cdn.manage.com/appsponsor/documentation/unity/unity_installing_sdk_3.png)

**NOTE: Please test on devices only. Ads will not show up in the Unity Editor or XCode simulators.**

### 3. Add Android support

Please update **Plugins/Android/AndroidManifest.xml** to add the necessary Manage Publisher activities and permissions as explained below:

```
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.READ_PHONE_STATE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />

<activity android:name="com.appsponsor.appsponsorsdk.activity.PopupAdActivity" android:launchMode="singleTop" android:theme="@android:style/Theme.Translucent">
</activity>

<activity android:name="com.appsponsor.appsponsorsdk.activity.VideoAdActivity" android:screenOrientation="landscape" android:launchMode="singleTop" android:theme="@android:style/Theme.NoTitleBar.Fullscreen" android:configChanges="keyboardHidden|orientation|screenSize">
</activity>
```

### 4. Add iOS support

Please download our iOS SDK from the [iOS install guide](/html/iOS-SDK-v3.1-Guide.html).  Follow the steps in that guide to `add` *AppSponsorSDK.embeddedframework* into your project. Then add/set these frameworks:

*  libz.dylib
*  libstdc++.dylib
*  AudioToolbox.framework
*  QuartzCore.framework
*  OpenGLES.framework
*  Security.framework
*  CFNetwork.framework
*  Accelerate.framework
*  SystemConfiguration.framework
*  CoreMotion.framework
*  CoreGraphics.framework
*  UIKit.framework
*  Foundation.framework
*  AdSupport.framework
*  CoreLocation.framework
*  CoreFoundation.framework
*  StoreKit.framework 

It should look similar to this:

![](https://s3.amazonaws.com/cdn.manage.com/appsponsor/documentation/unity/unity_installing_sdk_4.png)


### 5. Adding AppSponsor

Declare the `AppSponsorPlugin` component class:

```
public class MyDemoApp : MonoBehaviour {
    
    AppSponsorPlugin plugin;

    /* Flag if using Rewarded Ad */
    public bool isRewarded;
    
    ...
}
```

Add the `InitializeWithZoneId` and/or `InitializeRewardedWithZoneId` component class to game object during the `Start()` method:

```
void Start() {

    plugin = gameObject.AddComponent<AppSponsorPlugin>();

    #if UNITY_ANDROID
        if (isRewarded) 
            plugin.InitializeRewardedWithZoneId("YOUR REWARDED ANDROID ZONE ID", "YOUR PUBLISHER USER ID");
        else 
            plugin.InitializeWithZoneId("YOUR ANDROID ZONE ID");
    #elif UNITY_IPHONE
        if (isRewarded) 
            plugin.InitializeRewardedWithZoneId("YOUR REWARDED IOS ZONE ID", "YOUR PUBLISHER USER ID"); 
        else 
            plugin.InitializeWithZoneId("YOUR IOS ZONE ID");
    #endif
}

/* Register event listeners. See 'Listeners - Delegates' below */
RegisterEvents();

```

#### 5.1. Improve Ad Targeting by setting non PII user data:

```
plugin.SetCity("");
plugin.SetUCity("");
plugin.SetCountry("");
plugin.SetRegion("")
plugin.SetMetro("")
plugin.SetZip("")
plugin.SetLongitude("")
plugin.SetLatitude("")
plugin.SetGender("");
plugin.SetYob("")
plugin.SetKeywords("");
```

If you would like to pre cache your ads follow steps in section 5.2.  Otherwise, if you would like to load ads synchronously for immediate presentation of an ad follow steps in section 5.3:

#### 5.2 Pre-Cached Ads 
   
  Pre-cache ad:

```
plugin.Load();

```

To show ad:

```
if (plugin.IsReady()) {
    plugin.PresentAd();   
}
```

#### 5.3 Load and Present Ad Synchronously

To load and show ad:

```
plugin.LoadAndPresentAd();

```

##### Select your Platform

Note: Go to `File > Build Settings` and change the Platform accordingly:

![](https://s3.amazonaws.com/cdn.manage.com/appsponsor/documentation/unity/unity_installing_sdk_5.png)

### 6. Optional Steps

#### 6.1 Register Events Listeners

These are the listeners that you can implement to control your mobile application flow.

```
public void RegisterEvents() {
    AppSponsorPlugin.DidClickInterstitial += HandleDidClickInterstitial; 
    AppSponsorPlugin.DidCloseInterstitial += HandleDidCloseInterstitial; 
    AppSponsorPlugin.DidFailToLoad += HandleDidFailToLoad; 
    AppSponsorPlugin.WillAppear += HandleWillAppear; 
    AppSponsorPlugin.WillDisappear += HandleWillDisappear; 
    AppSponsorPlugin.DidCacheInterstitial += HandleDidCacheInterstitial; 
}

public void HandleDidFailToLoad(string message) {
    Debug.Log("DidFailToLoad, message:" + message);
}

public void HandleWillAppear() {
    Debug.Log("WillAppear");
}

public void HandleWillDisappear(string reason) {
    Debug.Log("WillDisappear, message: " + reason);
}
    
public void HandleRewardedAdFinished() {
    Debug.Log("RewardedAdFinished");
}

public void HandleDidCacheInterstitial(){
    Debug.Log("DidCacheInterstitial");
    
    if (plugin.IsReady()) {
            Debug.Log("Presenting Popup Ad");
            plugin.PresentAd();
    }
    else {
        Debug.Log("Popup Ad is not ready");
    }
}
```

#### 6.1 Unregister Events Listeners

When your view is completed, you can unregister any events in the `OnDestroy()` method:

```
void OnDestroy(){
    Debug.Log("Un-Registering AppSponsor Events");
    plugin.DidFailToLoad -= HandleDidFailToLoad;
    plugin.WillAppear -= HandleWillAppear;
    plugin.WillDisappear -= HandleWillDisappear;
    plugin.DidCacheInterstitial -= HandleDidCacheInterstitial;
    plugin.RewardedAdFinished -= HandleRewardedAdFinished;
}

```
