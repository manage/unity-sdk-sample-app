//
//  VXLInterstitialDelegate.h
//  CVMClient
//
//  Created by Russell D'Sa on 4/11/14.
//  Copyright (c) 2014 CloudVM. All rights reserved.
//

#import <Foundation/Foundation.h>

@class VXLInterstitialViewController;

@protocol VXLInterstitialDelegate <NSObject>

@optional
- (void)willPresentInterstitial:(VXLInterstitialViewController *)interstitial;
- (void)didPresentInterstitial:(VXLInterstitialViewController *)interstitial;
- (void)willDismissInterstitial:(VXLInterstitialViewController *)interstitial withError:(NSError *)error;
- (void)didDismissInterstitial;
- (void)didFailToPresentInterstitial:(VXLInterstitialViewController *)interstitial forCampaignWithId:(NSUInteger)campaignId;

@end
