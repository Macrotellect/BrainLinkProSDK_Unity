using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class UnityThinkGear
{

#if UNITY_IOS

    [DllImport("__Internal")]
    public static extern void SetWhiteList(string whiteList);
    
    [DllImport("__Internal")]
    public static extern void Scan();
    
    [DllImport("__Internal")]
    public static extern void ConnectDevice(string identifier);
    
    // [DllImport("__Internal")]
    // public static extern void DisConnect();
    
    [DllImport("__Internal")]
    public static extern void SendSettings(string send);
 
#endif


#if UNITY_ANDROID
    private static AndroidJavaObject jo = new AndroidJavaClass("com.macrotellect.unityforandroidsdk.UnitySDK").CallStatic<AndroidJavaObject>("getInstance");
#endif

    /// <summary>
    /// 开启监听
    /// </summary>
    /// <param name="objectName">接收回调挂载方法的物体名</param>
    public static void SetBLLinstenner(string objectName)
    {
#if UNITY_ANDROID
            Debug.Log("unity===SetBLLinstenner");
        //jo = new AndroidJavaClass("com.macrotellect.unityforandroidsdk.UnitySDK").CallStatic<AndroidJavaObject>("getInstance");
        jo.Call("setBleListener", objectName);
#endif
    }
    
    /*
     *  scan
     */
    public static void start()
    {
#if UNITY_ANDROID
        Debug.Log("unity=====start");
        jo.Call("start");
#endif
    }
    /*
     *  scan
     */
    public static void setWhiteList(string whiteList)
    {
#if UNITY_ANDROID
        Debug.Log("unity=====start");
        jo.Call("setWhiteList",whiteList);
#endif
    }
    
    /*
     *  connet
     */
    public static void connectDevice(string address)
    {
#if UNITY_ANDROID
        Debug.Log("unity=====connectDevice=="+address);
        jo.Call("connectDevice",address);
#endif
    }
    
//     /*
//      *  disconnet
//      */
//     public static void close()
//     {
// #if UNITY_ANDROID
//         Debug.Log("unity=====close");
//         jo.Call("close");
// #endif
//     }
    
    public static void sendSettings(string send)
    {
#if UNITY_ANDROID
        Debug.Log("unity=====sendSettings");
        jo.Call("sendSettings",send);
#endif
    }
}
