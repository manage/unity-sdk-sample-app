//
//  VXLClient.h
//  VXLClient
//
//  Created by Russell D'Sa on 5/21/13.
//  Copyright (c) 2013 Voxel. All rights reserved.
//

#ifndef VXL_NO_NAMESPACE
#import "VXLNamespace.h"
#import "VXLNamespacedDependencies.h"
#endif

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "VXLInterstitialViewController.h"
#import "VXLCampaign.h"

FOUNDATION_EXPORT NSString * const VXLClientOnlineNotification;
FOUNDATION_EXPORT NSString * const VXLClientOfflineNotification;
FOUNDATION_EXPORT NSString * const VXLAdPrefetchSuccessNotification;
FOUNDATION_EXPORT NSString * const VXLAdPrefetchFailNotification;

FOUNDATION_EXPORT NSString * const VXLAdPrefetchSuccessNotificationUserInfoCampaignKey;
FOUNDATION_EXPORT NSString * const VXLAdPrefetchSuccessNotificationUserInfoCampaignIdKey;

@protocol VXLClientDelegate <NSObject>

@optional
- (void)didGoOffline;
- (void)didComeOnline;

- (void)didPreloadCampaign:(VXLCampaign *)campaign withId:(NSUInteger)campaignId;
- (void)didFailToPreloadCampaignWithId:(NSUInteger)campaignId;

@end

@interface VXLClient : NSObject

@property (weak  , nonatomic) id<VXLClientDelegate> delegate;
@property (assign, readonly)  BOOL                  didRegister;
@property (assign, readonly)  BOOL                  isConnectable;
@property (assign, readonly)  BOOL                  isOnline; //online and latency tests have passed or been disabled
@property (assign, nonatomic) BOOL                  isDebug;
@property (copy, nonatomic)   NSString *            debugTag;

+ (VXLClient *)client;
+ (NSString *)clientVersion;

/**
    register with Voxel backend
    @param token access token
    @param secret secret string
 */
- (void)registerWithToken:(NSString *)token secret:(NSString *)secret;

/**
    asynchronously preload a campaign with a particular campaignId
 */
- (void)preloadCampaignWithId:(NSUInteger)campaignId;

@end
