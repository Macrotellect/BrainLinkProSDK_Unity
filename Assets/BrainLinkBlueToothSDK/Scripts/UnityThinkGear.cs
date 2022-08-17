using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class UnityThinkGear
{

#if UNITY_IOS

    [DllImport("__Internal")]
    public static extern void Connect();
    
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
     * 与SetBLLinstenner（）相对应，当开启监听之后执行
     */
    public static void ConnectBluetooth()
    {
#if UNITY_ANDROID
        Debug.Log("unity=====ConnectBluetooth");
        jo.Call("start");
#endif
    }
    public static void sendSettings(string send)
    {
#if UNITY_ANDROID
        Debug.Log("unity=====sendSettings");
        jo.Call("sendSettings",send);
#endif
    }
}
