//
//  VXLCampaign.h
//  CVMClient
//
//  Created by Russell D'Sa on 11/1/13.
//  Copyright (c) 2013 Voxel. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "VXLModel.h"

@interface VXLCampaign : VXLModel

@property (strong, readonly) NSNumber *duration;
@property (assign, readonly) BOOL isEnabled;

@end