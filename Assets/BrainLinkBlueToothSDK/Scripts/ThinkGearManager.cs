using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


[System.Serializable]
public class ThinkGearManager : MonoBehaviour
{
    public static ThinkGearManager instance;
    //FOR DEMO MODE
    public bool isDemo = false;
    public bool isShowBattery = false;
    private int hardwareversion;
    public Sprite signal0,signal1, signal2,signal3,signal4;
    public Image image_signal;
    public GameObject signal_battery;
    public Image filled_battary;
    private int Raw = 0;
    public int PoorSignal = 200;
    private int Attention = 0;
    private int Meditation = 0;
    private int Blink = 0;
    private float Delta = 0.0f;
    private float Theta = 0.0f;
    private float LowAlpha = 0.0f;
    private float HighAlpha = 0.0f;
    private float LowBeta = 0.0f;
    private float HighBeta = 0.0f;
    private float LowGamma = 0.0f;
    private float HighGamma = 0.0f;
    private string Hardwareversion4_0;
    private int heartRate = 0;
    private float temperature = 0.0f;
    private string HRV = "";
    private float xvalue4_0 = 0.0f;
    private float yvalue4_0 = 0.0f;
    private float zvalue4_0 = 0.0f;
    private int BatteryCapacity4_0 = 0;
    private int ap = 0;
    private int grind = 0;
    [Range(0, 100)]
    public int batteryCapacity4_0 = 0;
    [Range(-2000, 2000)]
    public int demo_raw;

    public Toggle toggle_ap;
    public Toggle toggle_circle;
    public int  isCircleOn = 0;//角度值
    public int  isApOn = 0;//喜好度
#if UNITY_ANDROID
	[HideInInspector]
	public bool bAndroidHeadsetConnected;
#endif

#if UNITY_IPHONE

    //control the accessory connect state..
    //*** because ios now only support one headset?
    [HideInInspector]
    public bool bIOSHeadsetConnected;
#endif



    public void Awake()
    {
        Debug.unityLogger.logEnabled = true;
        DontDestroyOnLoad(this);
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        instance = this;

        signal_battery.SetActive(false);

#if UNITY_IOS && !UNITY_EDITOR
        UnityThinkGear.Connect();
#elif UNITY_ANDROID && !UNITY_EDITOR
        UnityThinkGear.SetBLLinstenner("ThinkGearManager");
        UnityThinkGear.ConnectBluetooth();
#endif
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        toggle_ap.isOn = false;
        toggle_circle.isOn = false;
        StartCoroutine("ShowBatteryCapacity");
        StartCoroutine("UpdateSignal");
    }

    void OnGUI()
    {
        if (isDemo)
        {
            PoorSignal = 0;
#if UNITY_IPHONE
            bIOSHeadsetConnected = true;
#endif

#if UNITY_ANDROID
			bAndroidHeadsetConnected = true;
#endif
            if (GUILayout.Button("0"))
            {
                Attention = 0;
                Meditation = 0;
            }
            if (GUILayout.Button("10"))
            {
                Attention = 10;
                Meditation = 10;
            }
            if (GUILayout.Button("20"))
            {
                Attention = 20;
                Meditation = 20;
            }
            if (GUILayout.Button("30"))
            {
                Attention = 30;
                Meditation = 30;
            }
            if (GUILayout.Button("40"))
            {
                Attention = 40;
                Meditation = 40;
            }
            if (GUILayout.Button("50"))
            {
                Attention = 50;
                Meditation = 50;
            }
            if (GUILayout.Button("60"))
            {
                Attention = 60;
                Meditation = 60;
            }
            if (GUILayout.Button("70"))
            {
                Attention = 70;
                Meditation = 70;
            }
            if (GUILayout.Button("80"))
            {
                Attention = 80;
                Meditation = 80;
            }
            if (GUILayout.Button("90"))
            {
                Attention = 90;
                Meditation = 90;
            }
            if (GUILayout.Button("100"))
            {
                Attention = 100;
                Meditation = 100;
            }
            Raw = demo_raw;
        }
    }
    /// <summary>
    /// ��������ǳ����˳����Զ�ִ��
    /// </summary>
    public void OnApplicationQuit()
    {
        GC.Collect();
        instance = null;
    }

    IEnumerator UpdateSignal()
    {
        while (true)
        {
#if UNITY_IPHONE
        if (!bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
            if (!bAndroidHeadsetConnected)
#endif
            {
                signal_battery.SetActive(false);
                image_signal.sprite = signal0;
            }
            else
            {
                if (!isDemo)
                {
#if UNITY_EDITOR
                    PoorSignal = 200;
#endif
                    if (PoorSignal == 200)
                    {
                        image_signal.sprite = signal1;
                    }
                    else if (PoorSignal >= 100)
                    {
                        image_signal.sprite = signal2;
                    }
                    else if (PoorSignal > 0)
                    {
                        image_signal.sprite = signal3;
                    }
                    else
                    {
                        image_signal.sprite = signal4;
                    }

                    if (BatteryCapacity4_0 > 0)
                    {
                        signal_battery.SetActive(true);
                        StopCoroutine("ShowBatteryCapacity");
                        StartCoroutine("ShowBatteryCapacity");
                    }
                    else
                    {
                        signal_battery.SetActive(false);
                        StopCoroutine("ShowBatteryCapacity");
                    }
                }
                else
                {
                    if (isShowBattery)
                    {
                        signal_battery.SetActive(true);
                    }
                    else
                        signal_battery.SetActive(false);

                    image_signal.sprite = signal4;
                }
            }
            yield return null;
        }
    }

    IEnumerator ShowBatteryCapacity()
    {
        while (true)
        {
#if UNITY_IPHONE
            if (bIOSHeadsetConnected)
#elif UNITY_ANDROID
            if(bAndroidHeadsetConnected)
#endif
            {
                filled_battary.fillAmount = GetBatteryCapacity() / 100.0f;
                if (GetBatteryCapacity() > 20)
                {
                    filled_battary.color = new Color(167 / 255f, 167 / 255f, 167 / 255f);
                }
                else
                {
                    filled_battary.color = new Color(237 / 255f, 22 / 255f, 80 / 255f);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// 获取信号值（0:蓝牙连接并且佩戴成功   0～200:蓝牙连接成功，佩戴不成功    200:未连接蓝牙）
    /// </summary>
    /// <returns></returns>
	public int GetWave_quality()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if (bAndroidHeadsetConnected)
#endif
            return PoorSignal;
        else if (isDemo)
            return 0;
        else
            return 200;
    }

    public int GetAttention()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return Attention;
        else
            return 0;
    }

    public int GetMeditation()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return Meditation;
        else
            return 0;
    }
    //գ��
    public int GetBlink()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if (bAndroidHeadsetConnected)
#endif
            return Blink;
        else
            return 0;
    }

    public int GetRaw()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return Raw;
        else
            return 0;
    }

    public float GetDelta()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return Delta;
        else
            return 0;
    }

    public float GetTheta()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return Theta;
        else
            return 0;
    }

    public float GetHighAlpha()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return HighAlpha;
        else
            return 0;
    }

    public float GetHighBeta()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return HighBeta;
        else
            return 0;
    }

    public float GetHighGamma()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return HighGamma;
        else
            return 0;
    }

    public float GetLowAlpha()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return LowAlpha;
        else
            return 0;
    }

    public float GetLowBeta()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return LowBeta;
        else
            return 0;
    }

    public float GetLowGamma()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected && !isDemo)
#endif
#if UNITY_ANDROID
			if ( bAndroidHeadsetConnected)
#endif
            return LowGamma;
        else
            return 0;
    }
    /// <summary>
    /// tg.IsConnect2
    /// </summary>
    /// <returns></returns>
	public bool IsHeadsetConnected()
    {
        //Android..
#if UNITY_ANDROID
            return bAndroidHeadsetConnected;
#endif
        //IPHONE or Other.
#if UNITY_IPHONE
        return bIOSHeadsetConnected;
#endif
    }

    public string GetHardwareversion4_0()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if(bAndroidHeadsetConnected)
#endif
            return Hardwareversion4_0;
        else
            return "";
    }
    public float Getxvalue4_0()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if(bAndroidHeadsetConnected)
#endif
            return xvalue4_0;
        else
            return 0;
    }
    public float Getyvalue4_0()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if(bAndroidHeadsetConnected)
#endif
            return yvalue4_0;
        else
            return 0;
    }
    public float Getzvalue4_0()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
			if(bAndroidHeadsetConnected)
#endif
            return zvalue4_0;
        else
            return 0;
    }

    //ϲ��
    public int GetAp4_0()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
        if (bAndroidHeadsetConnected)
#endif
            return ap;
        else
            return -1;
    }
    //ҧ��
    public float GetGrind4_0()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#endif
#if UNITY_ANDROID
        if (bAndroidHeadsetConnected)
#endif
            return grind;
        else
            return -1;
    }

    public int GetBatteryCapacity()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#elif UNITY_ANDROID
        if (bAndroidHeadsetConnected)
#endif
        {
            return BatteryCapacity4_0;
        }
        else
            return 0;
    }

    public int GetHeartRate()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#elif UNITY_ANDROID
        if (bAndroidHeadsetConnected)
#endif
        {
            return heartRate;
        }
        else
            return 0;
    }

    public float GetTemperature()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#elif UNITY_ANDROID
        if (bAndroidHeadsetConnected)
#endif
        {
            return temperature;
        }
        else
            return 0;
    }

    public string GetHRV()
    {
#if UNITY_IPHONE
        if (bIOSHeadsetConnected)
#elif UNITY_ANDROID
        if (bAndroidHeadsetConnected)
#endif
        {
            return HRV;
        }
        else
            return "";
    }
    

    bool isConnect = false;

    string deviceName = "";

    //Accessory connect state...
    void ReceiveContentState(string data)
    {
        Debug.Log("ReceiveContentState   data = " + data);
        if (data == "yes")
        {
#if UNITY_ANDROID
            PoorSignal = 200;
            bAndroidHeadsetConnected = true;
#elif UNITY_IPHONE
            PoorSignal = 200;
            bIOSHeadsetConnected = true;
#endif
        }
        else
        {

#if UNITY_IPHONE
            bIOSHeadsetConnected = false;
#elif UNITY_ANDROID
            bAndroidHeadsetConnected = false;
#endif
            isSendFirst = false;
            isApOn = 0;
            isCircleOn = 0;
            toggle_ap.isOn = false;
            toggle_circle.isOn = false;

        }
    }

    //Raw
    void ReceiveRawdata(string data)
    {
        Raw = int.Parse(data);
    }

    void SendSettings(int isApOnValue, int isCircleOnValue)
    {
        string send = "B01:" + isApOnValue + "11" + isCircleOnValue + ";";

        if (isApOnValue == 1)
        {
            toggle_ap.isOn = true;
        }
        else
        {
            toggle_ap.isOn = false;
        }
            
        if (isCircleOnValue == 1)
        {
            toggle_circle.isOn = true;
        }
        else
        {
            toggle_circle.isOn = false;
        }

#if UNITY_IOS && !UNITY_EDITOR
        UnityThinkGear.SendSettings(send+send+send+send+send);
#elif UNITY_ANDROID && !UNITY_EDITOR
         UnityThinkGear.sendSettings(send+send+send+send+send);
#endif
    }

    private bool isSendFirst = false;
    void ReceivePoorSignal(string data)
    {
        PoorSignal = int.Parse(data);
        if (!isSendFirst)
        {
            isApOn = 0;
            isCircleOn = 1;
            SendSettings(isApOn, isCircleOn);
            isSendFirst = true;
        }

    }
    
    
    void ReceiveAttention(string data)
    {
        Attention = int.Parse(data);
    }
    void ReceiveMeditation(string data)
    {
        Meditation = int.Parse(data);
    }
    void ReceiveBatteryCapacity(string data)
    {
        BatteryCapacity4_0 = int.Parse(data);
    }
    //Delta
    void ReceiveDelta(string data)
    {
        Delta = int.Parse(data);
    }
    //Theta
    void ReceiveTheta(string data)
    {
        Theta = int.Parse(data);
    }

    //LowAlpha
    void ReceiveLowAlpha(string data)
    {
        LowAlpha = int.Parse(data);
    }
    //HighAlpha
    void ReceiveHighAlpha(string data)
    {
        HighAlpha = int.Parse(data);
    }

    //LowBeta
    void ReceiveLowBeta(string data)
    {
        LowBeta = int.Parse(data);
    }

    //HighBeta
    void ReceiveHighBeta(string data)
    {
        HighBeta = int.Parse(data);
    }

    //LowGamma
    void ReceiveLowGamma(string data)
    {
        LowGamma = int.Parse(data);
    }

    //HighGamma
    void ReceiveHighGamma(string data)
    {
        HighGamma = int.Parse(data);
    }

    void ReceiveXValue(string data)
    {
        xvalue4_0 = float.Parse(data);
    }
    void ReceiveYValue(string data)
    {
        yvalue4_0 = float.Parse(data);
    }
    void ReceiveZValue(string data)
    {
        zvalue4_0 = float.Parse(data);
    }
    //眨眼
    private void ReceiveBlink(string data)
    {
        Blink = int.Parse(data);
    }
    //咬牙
    void ReceiveGrind4_0(string value)
    {
        grind = int.Parse(value);
    }
    //喜好度
    void ReceiveAp4_0(string value)
    {
        ap = int.Parse(value);
    }

    void ReceiveHardwareversion4_0(string value)
    {
        Hardwareversion4_0 = value;
    }
    //心率
    void ReceiveHeaetRate(string value)
    {
        heartRate = int.Parse(value);
    }
    //额温
    void ReceiveTemperature(string value)
    {
        temperature = float.Parse(value);
    }
    
    //
    void ReceiveHRV(string value)
    {
        HRV = value;
    }

    public void OnValueChangeAp(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("开关ap打开");
            isApOn = 1;
            SendSettings(isApOn, isCircleOn);
        }
        else
        {
            Debug.Log("开关ap关了");
            isApOn = 0;
            SendSettings(isApOn, isCircleOn);
        }
    }
    
    public void OnValueChangeCircle(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("开关circle打开");
            isCircleOn = 1;
            SendSettings(isApOn, isCircleOn);
        }
        else
        {
            Debug.Log("开关circle关了");
            isCircleOn = 0;
            SendSettings(isApOn, isCircleOn);
        }
    }

}
