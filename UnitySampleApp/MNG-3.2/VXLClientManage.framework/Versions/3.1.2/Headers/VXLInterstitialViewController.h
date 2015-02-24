//
//  VXLInterstitialViewController.h
//  VXLInterstitialTest
//
//  Created by Russell D'Sa on 3/27/14.
//  Copyright (c) 2014 Voxel. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "VXLInterstitialDelegate.h"
#import "VXLInterstitialEventsDelegate.h"
#import "VXLCampaign.h"

@interface VXLInterstitialViewController : UIViewController

@property (assign, nonatomic) NSUInteger campaignId;
@property (strong, nonatomic) VXLCampaign *campaign;
@property (weak, nonatomic) id<VXLInterstitialDelegate, VXLInterstitialEventsDelegate> delegate;

+ (void)presentInterstitalForCampaignWithId:(NSUInteger)campaignId
                           inViewController:(UIViewController *)viewController
                                   delegate:(id<VXLInterstitialDelegate, VXLInterstitialEventsDelegate>)delegate;

+ (void)presentInterstitalForCampaignWithId:(NSUInteger)campaignId
                            campaignOptions:(NSDictionary *)campaignOptions
                           inViewController:(UIViewController *)viewController
                                   delegate:(id<VXLInterstitialDelegate, VXLInterstitialEventsDelegate>)delegate;

- (void)dismiss;

@end
