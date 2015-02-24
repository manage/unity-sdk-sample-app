//
//  VXLInterstitialEventsDelegate.h
//  CVMClient
//
//  Created by Russell D'Sa on 4/11/14.
//  Copyright (c) 2014 CloudVM. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef enum : NSUInteger {
    VXLInterstitialEventClose = 0,
    VXLInterstitialEventPlay,
    VXLInterstitialEventTimeFinished,
    VXLInterstitialEventInstall,
    VXLInterstitialEventOther
} VXLInterstitialEvent;

@protocol VXLInterstitialEventsDelegate <NSObject>

@required
- (void)userWillInstall;

@optional
#pragma mark - Lifecycle Callbacks
- (void)userWillViewPreroll;
- (void)userWillDismissPreroll:(VXLInterstitialEvent)event;

- (void)userWillPlay;
- (void)sessionDidStart;
- (void)sessionDidFailStart;

//called every second during gamplay
- (void)userHasPlayedFor:(NSUInteger)timeElapsed outOf:(NSUInteger)timeLimit; //seconds

- (void)userDidPlay:(VXLInterstitialEvent)event for:(NSInteger)time; //seconds
- (void)userWillViewPostroll;
- (void)userWillDismissPostroll:(VXLInterstitialEvent)event;

@end
