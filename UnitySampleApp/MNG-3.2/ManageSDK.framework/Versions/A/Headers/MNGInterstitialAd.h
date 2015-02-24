//
//  MNGInterstitialAd.h
//  ManageSDKFramework
//
//  Created by frank wang on 3/31/14.
//  Copyright (c) 2014 manage. All rights reserved.
//
#define MNG_SDK_VERSION                 @"3.1.1"

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

/**
 * Ad disappear because user has clicked on it
 */
static NSString * const kMNGReasonUserClicked = @"clicked";
/**
 * Ad disappear because user has closed ad playing activity
 */
static NSString * const kMNGReasonUserClosed = @"close";
/**
 * Ad disappear because of some error
 */
static NSString * const kMNGError = @"error";
	
static NSString * const kMNGEnableLocationSupportKey = @"enableLocationSupport";

static NSString * const kMNGCountryKey = @"country";
static NSString * const kMNGCityKey = @"city";
static NSString * const kMNGRegionKey = @"region";
static NSString * const kMNGMetroKey = @"metro";
static NSString * const kMNGZipKey = @"zip";

static NSString * const kMNGUCountryKey = @"u_country";
static NSString * const kMNGUCityKey = @"u_city";
static NSString * const kMNGUZipKey = @"u_zip";
static NSString * const kMNGLatitudeKey = @"latitude";
static NSString * const kMNGLongitudeKey = @"longitude";

static NSString * const kMNGGenderKey = @"gender";
static NSString * const kMNGYearOfBirthKey = @"yob";
static NSString * const kMNGKeywordsKey = @"keywords";
static NSString * const kMNGPublicUidKey = @"pub_uid";
static NSString * const kMNGPlatformKey = @"pf";

static const CGFloat kMNGDefaultTimeoutSeconds = 30.0f;

static NSString * const kMNGErrorDomain = @"com.Manage.SDK";

typedef enum { MNG_MALE = 1, MNG_FEMALE = 2, MNG_OTHER = 3 } Gender;

@protocol MNGInterstitialAdDelegate <NSObject>
/**
 * Called when ad is loaded and going to be presented to user.
 */
- (void)popoverWillAppear;
/**
 * Called when ad is going to be dismissed by some reason.
 * @param reason - Ad dismiss reason.
 */
- (void)popoverWillDisappear:(NSString*)reason;
@optional
/**
 * Called once ad is loaded
 */
- (void)didCacheInterstitial;
/**
 * Called if some error has happened during the ad playing cycle
 * @param exception - Error occurred
 */
- (void)popoverDidFailToLoadWithError:(NSError*)error;
/**
 * Called once rewarded ad was completely played
 */
- (void)onRewardedAdFinished;
@end

@class MNGAdContext;
@interface MNGInterstitialAd : NSObject

@property (nonatomic, strong) UIViewController *parentController;
@property (nonatomic, weak) id<MNGInterstitialAdDelegate> delegate;

@property (nonatomic, strong) NSString *zoneId;
@property (nonatomic, strong) NSString *country;
@property (nonatomic, strong) NSString *region;
@property (nonatomic, strong) NSString *metro;
@property (nonatomic, strong) NSString *city;
@property (nonatomic, strong) NSString *zip;
@property (nonatomic, assign) Gender* gender;
@property (nonatomic, strong) NSString *yob;
@property (nonatomic, strong) NSString *u_country;
@property (nonatomic, strong) NSString *u_city;
@property (nonatomic, strong) NSString *u_zip;
@property (nonatomic, strong) NSString *keywords;
@property (nonatomic, strong) NSString *longitude;
@property (nonatomic, strong) NSString *latitude;
@property (nonatomic, strong) NSString *pub_uid; //user id within publisher app
@property (nonatomic, strong) NSString* pf;
@property (nonatomic, strong) MNGAdContext *adContext;
@property (nonatomic, strong) NSString* adId;


- (MNGInterstitialAd*)enableLocationSupport;
- (MNGInterstitialAd*)initWithZoneId:(NSString*)adId;
- (MNGInterstitialAd*)initRewardedAdWithZoneId:(NSString*)adId andUserID:(NSString*)uid;

/**
 * Asynchronously loads ad. Use {@link #isReady() isReady} method to check whether ad is loaded or not.
 */
- (void)load;

/**
 * Asynchronously loads ad. Displays it immediately upon load. 
 * During a period of {@link #kMNGDefaultTimeoutSeconds kMNGDefaultTimeoutSeconds} loading either succeedes or fails with timeout error.
 */
- (void)loadAndPresentAd;

/**
 * Asynchronously loads ad. Displays it immediately upon load.
 * During custom timeout loading either succeedes or fails with timeout error.
 * @param seconds - custom timeout
 */
- (void)loadAndPresentAdWithTimeout:(CGFloat)seconds;

/**
 * Shows ad in separate activity. Call is ignored if ad is not yet loaded.
 */
- (void)presentAd;

/**
 * @return boolean Return true if ad load is finished.
 */
- (BOOL)isReady;

/**
 * @return int percentage (0 - 100) of video ad being played
 */
- (NSInteger)rewardedAdStatus;

@end
