#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

#import "ManagePlugin.h"
#import <ManageSDK/MNGInterstitialAd.h>

@interface ManagePlugin() <CLLocationManagerDelegate>
{
    CLLocationManager *locationManager;
}

// Root view controller for Unity applications can be accessed using this
// method.
extern UIViewController *UnityGetGLViewController();
+ (ManagePlugin *)pluginSharedInstance;
- (void)popoverWillAppearForPopupAd:(MNGInterstitialAd*)ad;
- (void)popoverOfPopupAd:(MNGInterstitialAd*)ad willDisappearWithReason:(NSString*)reason;
- (void)onRewardedAdFinishedForPopupAd:(MNGInterstitialAd*)ad;
- (void)interstitialCachedForPopupAd:(MNGInterstitialAd*)ad;
- (void)popupAd:(MNGInterstitialAd*)ad didFailToLoadWithError:(NSError *)error;
@end


// TODO: this is an ugly workaround which could be fixed by refactoring AsPopupAd's delegate protocol
@interface ManagePluginDelegateAdapter:NSObject <MNGInterstitialAdDelegate>
{
}
@property (nonatomic, strong) MNGInterstitialAd *popupAd;
- (id)initWithPopupAd:(MNGInterstitialAd*)ad;
// Root view controller for Unity applications can be accessed using this method.
extern UIViewController *UnityGetGLViewController();

@end

@implementation ManagePluginDelegateAdapter

- (id)initWithPopupAd:(MNGInterstitialAd*)ad
{
    self = [super init];
    if(self)
    {
        self.popupAd = ad;
        self.popupAd.delegate = self;
    }
    return self;
}

- (void)popoverWillAppear
{
    [[ManagePlugin pluginSharedInstance] popoverWillAppearForPopupAd:self.popupAd];
}

- (void)popoverWillDisappear:(NSString*)reason
{
    [[ManagePlugin pluginSharedInstance] popoverOfPopupAd:self.popupAd willDisappearWithReason:reason];
}

- (void)didCacheInterstitial
{
    [[ManagePlugin pluginSharedInstance] interstitialCachedForPopupAd:self.popupAd];
}

- (void)popoverDidFailToLoadWithError:(NSError*)error
{
    [[ManagePlugin pluginSharedInstance] popupAd:self.popupAd didFailToLoadWithError:error];
}

- (void)onRewardedAdFinished
{
    [[ManagePlugin pluginSharedInstance] onRewardedAdFinishedForPopupAd:self.popupAd];
}

@end


@implementation ManagePlugin

#pragma mark Unity bridge

+ (ManagePlugin *)pluginSharedInstance
{
    static ManagePlugin *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[ManagePlugin alloc] init];
        sharedInstance.popupAdAdapters = [[NSMutableDictionary alloc] init];
    });
    return sharedInstance;
}

- (void)createPopupAdwithZoneId:(NSString *)zoneId instanceId:(NSString*)instanceID
{
    if (!zoneId) {
        NSLog(@"ManagePlugin: Failed because no zone ID is set.");
        return;
    }
    
    MNGInterstitialAd *popupAd = [[[MNGInterstitialAd alloc] initWithZoneId:zoneId] autorelease];
    popupAd.pf = @"1u";
    popupAd.parentController = UnityGetGLViewController();
    
    ManagePluginDelegateAdapter * adapter = [[ManagePluginDelegateAdapter alloc] initWithPopupAd:popupAd];
    [self.popupAdAdapters setObject: adapter forKey:instanceID];
}

- (void)createRewardedAd:(NSString*)zoneId andUserID:(NSString*)uid instanceId:(NSString*)instanceID
{
    if (!zoneId || !uid) {
        NSLog(@"ManagePlugin: Failed because no zone ID or UserID is set.");
        return;
    }
    
    MNGInterstitialAd *popupAd = [[[MNGInterstitialAd alloc] initRewardedAdWithZoneId:zoneId andUserID:uid] autorelease];
    popupAd.pf = @"1u";
    popupAd.parentController = UnityGetGLViewController();
    
    ManagePluginDelegateAdapter * adapter = [[ManagePluginDelegateAdapter alloc] initWithPopupAd:popupAd];
    [self.popupAdAdapters setObject: adapter forKey:instanceID];
}

/*
 * Preloads the ad.
 */
- (void)loadAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    [popupAd load];
}

- (void)loadAndPresentAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    [popupAd loadAndPresentAd];
}

- (void)loadAndPresentAdWithInstanceId:(NSString*)instanceID timeout:(NSString*)timeoutString
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    CGFloat timeout = [timeoutString floatValue];
    if (timeout == 0)
    {
        [popupAd loadAndPresentAd];
    }
    else
    {
        [popupAd loadAndPresentAdWithTimeout:timeout];
    }
}

/*
 * Presents the ad onto the current top ViewController
 * When ad is done being presented (closed), the ad is destroyed.
 * Load will need to be called to recreate the destroyed ad.
 */
- (void)presentAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    [popupAd presentAd];
}

- (BOOL)isAdReadyWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return NO;
    }
    return [popupAd isReady];
}

- (int)rewardedAdStatusForAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return 0;
    }
    return [popupAd rewardedAdStatus];
}

/*
 * This method wakes device's GPS and enables ad to listen for location updates
 * Waking GPS separately is needed because SDK itself is somehow unable to do so. Is it a bug or a feature?
 */
- (void)enableLocationForAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    
    locationManager = [[CLLocationManager alloc] init];
    locationManager.desiredAccuracy = kCLLocationAccuracyBest;
    locationManager.delegate = self;
    [locationManager startUpdatingLocation];
    
    [popupAd enableLocationSupport];
}

- (void)setAdId:(NSString*)adId forAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    popupAd.adId = adId;
}
- (void)setExtras:(NSString*)extrasJson forAdWithInstanceId:(NSString*)instanceID
{
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if (!popupAd) {
        NSLog(@"ManagePlugin: Failed because a popupAd was never created.");
        return;
    }
    // Turn the incoming JSON string into a NSDictionary.
    // TODO: rewrite this extremely inefficient code block
    NSError *error = nil;
    NSData *extrasJsonData = [extrasJson dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *extrasJsonDictionary = [NSJSONSerialization JSONObjectWithData:extrasJsonData
                                                                         options:NSJSONReadingMutableContainers
                                                                           error:&error];
    if (extrasJsonDictionary) {
        NSString* value = [extrasJsonDictionary valueForKey:@"country"];
        if (value != nil) {
            popupAd.country = value;
        }
        value = [extrasJsonDictionary valueForKey:@"region"];
        if (value != nil) {
            popupAd.region = value;
        }
        value = [extrasJsonDictionary valueForKey:@"metro"];
        if (value != nil) {
            popupAd.metro = value;
        }
        value = [extrasJsonDictionary valueForKey:@"city"];
        if (value != nil) {
            popupAd.city = value;
        }
        value = [extrasJsonDictionary valueForKey:@"zip"];
        if (value != nil) {
            popupAd.zip = value;
        }
        value = [extrasJsonDictionary valueForKey:@"gender"];
        if (value != nil) {
            Gender gender;
            if ([value caseInsensitiveCompare:@"male"] == NSOrderedSame) {
                gender = MNG_MALE;
            }
            else if ([value caseInsensitiveCompare:@"female"] == NSOrderedSame) {
                gender = MNG_FEMALE;
            }
            else if ([value caseInsensitiveCompare:@"other"] == NSOrderedSame) {
                gender = MNG_OTHER;
            }
            if (gender)
                popupAd.gender = &(gender);
        }
        value = [extrasJsonDictionary valueForKey:@"yob"];
        if (value != nil) {
            popupAd.yob = value;
        }
        value = [extrasJsonDictionary valueForKey:@"u_country"];
        if (value != nil) {
            popupAd.u_country = value;
        }
        value = [extrasJsonDictionary valueForKey:@"u_city"];
        if (value != nil) {
            popupAd.u_city = value;
        }
        value = [extrasJsonDictionary valueForKey:@"u_zip"];
        if (value != nil) {
            popupAd.u_zip = value;
        }
        value = [extrasJsonDictionary valueForKey:@"keywords"];
        if (value != nil) {
            popupAd.keywords = value;
        }
        value = [extrasJsonDictionary valueForKey:@"longitude"];
        if (value != nil) {
            popupAd.longitude = value;
        }
        value = [extrasJsonDictionary valueForKey:@"latitude"];
        if (value != nil) {
            popupAd.latitude = value;
        }
        value = [extrasJsonDictionary valueForKey:@"pub_uid"];
        if (value != nil) {
            popupAd.pub_uid = value;
        }
    } else {
        NSLog(@"ManagePlugin: Error parsing JSON for extras: %@", error);
    }
}

- (void)cleanupInstanceWithId:(NSString *)instanceID
{
    if(!instanceID) return;
    
    MNGInterstitialAd *popupAd = [[self.popupAdAdapters objectForKey:instanceID] popupAd];
    if(!popupAd) return;
    
    popupAd.delegate = nil;
    [self.popupAdAdapters removeObjectForKey:instanceID];
}

#pragma mark MNGInterstitialAdDelegate implementation

- (void)popoverWillAppearForPopupAd:(MNGInterstitialAd *)ad
{
    NSArray * keys = [self.popupAdAdapters allKeysForObject:ad.delegate];
    if(keys.count == 0) return;
    
    UnitySendMessage(strdup([keys[0] UTF8String]),
                     "OnWillAppear",
                     "");
}

- (void)popoverOfPopupAd:(MNGInterstitialAd*)ad willDisappearWithReason:(NSString*)reason
{
    NSArray * keys = [self.popupAdAdapters allKeysForObject:ad.delegate];
    if(keys.count == 0) return;
    
    
    UnitySendMessage(strdup([keys[0] UTF8String]),
                     "OnWillDisappear",
                     strdup([reason UTF8String]));
}

- (void)onRewardedAdFinishedForPopupAd:(MNGInterstitialAd*)ad
{
    NSArray * keys = [self.popupAdAdapters allKeysForObject:ad.delegate];
    if(keys.count == 0) return;
    
    UnitySendMessage(strdup([keys[0] UTF8String]),
                     "OnRewardedAdFinished",
                     "");
}

- (void)interstitialCachedForPopupAd:(MNGInterstitialAd*)ad
{
    NSArray * keys = [self.popupAdAdapters allKeysForObject:ad.delegate];
    if(keys.count == 0) return;
    
    UnitySendMessage(strdup([keys[0] UTF8String]),"OnDidCacheInterstitial","");
}

- (void)popupAd:(MNGInterstitialAd*)ad didFailToLoadWithError:(NSError *)error
{
    NSArray * keys = [self.popupAdAdapters allKeysForObject:ad.delegate];
    if(keys.count == 0) return;
    
    NSString *errorMsg =[NSString stringWithFormat:@"Failed to receive ad with error: %@", [error localizedDescription]];
    UnitySendMessage(strdup([keys[0] UTF8String]),
                     "OnDidFailToLoad",
                     strdup([errorMsg UTF8String]));
}

@end

// Helper method used to convert NSStrings into C-style strings.
// make it static to avoid name conflicts with other files using similar name
static NSString *CreateNSString(const char* string) {
    if (string) {
        return [NSString stringWithUTF8String:string];
    } else {
        return [NSString stringWithUTF8String:""];
    }
}

// Unity can only talk directly to C code so use these method calls as wrappers
// into the actual plugin logic.
extern "C" {
    
    void _CreatePopupAd(const char *instanceId, const char *zoneId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin createPopupAdwithZoneId:CreateNSString(zoneId) instanceId:CreateNSString(instanceId)];
    }
    
    void _CreateRewardedAd(const char *instanceId, const char *zoneId, const char *uid) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin createRewardedAd:CreateNSString(zoneId) andUserID:CreateNSString(uid) instanceId:CreateNSString(instanceId)];
    }
    
    void _Load(const char *instanceId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin loadAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    void _LoadAndPresentAd(const char *instanceId)
    {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin loadAndPresentAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    void _LoadAndPresentAdWithTimeout(const char *instanceId, const char *timeoutSeconds)
    {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin loadAndPresentAdWithInstanceId:CreateNSString(instanceId) timeout:CreateNSString(timeoutSeconds)];
    }
    
    void _PresentAd(const char *instanceId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin presentAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    bool _IsReady(const char *instanceId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        return [managePlugin isAdReadyWithInstanceId:CreateNSString(instanceId)];
    }
    
    int _RewardedAdStatus(const char *instanceId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        return [managePlugin rewardedAdStatusForAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    void _EnableLocation(const char *instanceId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin enableLocationForAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    void _SetExtras(const char *instanceId, const char *extrasJson) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin setExtras:CreateNSString(extrasJson) forAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    void _SetAdId(const char *instanceId, const char *extrasJson) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin setAdId:CreateNSString(extrasJson) forAdWithInstanceId:CreateNSString(instanceId)];
    }
    
    void _Delete(const char *instanceId) {
        ManagePlugin *managePlugin = [ManagePlugin pluginSharedInstance];
        [managePlugin cleanupInstanceWithId:CreateNSString(instanceId)];
    }
}