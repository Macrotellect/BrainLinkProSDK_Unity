//
//  ScannedDevice.h
//  IOS_Blue3OrBlue4Demo
//
//  Created by Macrotellect-iOSDev on 2022/10/28.
//  Copyright © 2022 Macrotellect-iOSDev. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreBluetooth/CoreBluetooth.h>

NS_ASSUME_NONNULL_BEGIN

@interface ScannedDevice : NSObject
@property (nonatomic,strong)CBPeripheral *peripheral;
@property (strong, nonatomic) NSString* name;
@property (strong, nonatomic) NSNumber* RSSI;
@end

NS_ASSUME_NONNULL_END
