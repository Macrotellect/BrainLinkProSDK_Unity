//
//  Blue4Manager.h
//  NewneuroMonitor
//
//  Created by 方亮 on 2018/7/25.
//  Copyright © 2018年 Macrotellect-iOSDev. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HZLBlueData.h"
#import "ScannedDevice.h"

typedef NS_ENUM(NSUInteger,BluePermission_state)
{
    Blue4_PoweredOn  =   0,        //蓝牙开关打开
    Blue4_PoweredOff,              //蓝牙开关关
    Blue4_Unauthorized,            //蓝牙权限未授权
    Blue4_UnknownOrUnsupported     //不支持蓝牙或未知
};

typedef void(^EndScanBlue4Block)(void);
typedef void(^Blue4DataBlock)(HZLBlueData *blueData,BlueType conBT,BOOL isFalseCon);
typedef void(^Blue4Connect)(NSString *markKey);
typedef void(^Blue4DisConnect)(NSString *markKey);

@interface Blue4Manager : NSObject

@property (nonatomic,copy) EndScanBlue4Block endScanBlue4Block;
//蓝牙连接成功的回调
@property (nonatomic,copy) Blue4Connect blueConBlock;
//蓝牙断开回调
@property (nonatomic,copy) Blue4DisConnect blueDisBlock;

// 蓝牙设备按照连接顺序依次为 A B C D E F
/*使用如上方式，比如有6个数据回调( hzlblueDataBlock_A,hzlblueDataBlock_B .....)，是为了保证数据的独立性，各个设备间的数据可以同时接受，互不影响
蓝牙4.0设备最多可以连接6个，可以连接6个但是连接成功比较难*/
 /*如果要使用单连接，ableDeviceSum传入参数为1，只调用hzlblueDataBlock_A即可*/
//各个设备的数据回调
@property (nonatomic,copy)Blue4DataBlock  hzlblueDataBlock_A;
@property (nonatomic,copy)Blue4DataBlock  hzlblueDataBlock_B;
@property (nonatomic,copy)Blue4DataBlock  hzlblueDataBlock_C;
@property (nonatomic,copy)Blue4DataBlock  hzlblueDataBlock_D;
@property (nonatomic,copy)Blue4DataBlock  hzlblueDataBlock_E;
@property (nonatomic,copy)Blue4DataBlock  hzlblueDataBlock_F;
//各个设备连接状态
@property (nonatomic,assign)BOOL  connected_A;
@property (nonatomic,assign)BOOL  connected_B;
@property (nonatomic,assign)BOOL  connected_C;
@property (nonatomic,assign)BOOL  connected_D;
@property (nonatomic,assign)BOOL  connected_E;
@property (nonatomic,assign)BOOL  connected_F;

/*! @brief 是否打印log  默认不打印
 */
+ (void)logEnable:(BOOL)enable;

/*! @brief 初始化(单例)
 */
+ (instancetype)shareInstance;

/*! @brief 连接配置
    appSoleCode: app唯一码
    defaultBlueNames:默认的可连接蓝牙名称数组
    ableDeviceSum: 可以连接的蓝牙设备个数
    result: 返回可以连接的设备名
 */
-(void)configureBlueNamesWithAppSoleCode:(NSString *)appSoleCode defaultBlueNames:(NSArray *)defaultBlueNames ableDeviceSum:(int)ableDeviceSum result:(void (^)(NSArray *))result;

/*! @brief 连接配置 
   blueNames: 可以连接的设备名(4.0指设备名称如BrainLink_Pro)
   ableDeviceSum: 可以连接的蓝牙设备个数
*/
-(void)configureBlueNames:(NSArray *)blueNames  ableDeviceSum:(int)ableDeviceSum;

/*! @brief 连接蓝牙设备设置
 */
-(void)bluePermission:(void (^)(BluePermission_state, NSString *))bluePermission_block;
-(void)connectBlue4WithIsAuto:(BOOL)isAuto;


/*! @brief 断开蓝牙设备
 */
-(void)disConnectBlue4;


/*! @brief 手动测试假连接（假连接定义：当signal = 0的时候，attention和medition的连续10个值不变，认为是假连接，断开当前设备的蓝牙连接，再次自动连接）
 */

/*! @brief 扫描蓝牙4.0
 scanTime:扫描时间 0表示一直扫描
*/
-(void)scanBlue4WithScannedWithScanTime:(int)scanTime  blue4DeviceBlock:(void (^)(ScannedDevice *))scannedBlue4Device;

/*! @brief 停止扫描4.0
*/
-(void)stopBlue4;

/*! @brief 手动连接蓝牙4.0
*/
-(void)manuallyConnetBlue4ForScannedDevice:(ScannedDevice*)scannedDevice;

/*! @brief 手动断开蓝牙4.0
*/
-(void)manuallyDisConnetBlue4ForScannedDevice:(ScannedDevice*)scannedDevice;


-(void)testAFalseCon:(BOOL)isTest;//手动测试A设备假连接
-(void)setTestToZero;//取消所有手动测试假连接
-(void)writeToProAWithCode:(NSData *)code;
@end
