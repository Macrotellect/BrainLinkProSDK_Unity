using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{

    public Text text_data, text_data1;
    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    
    private void Start()
    {
        StartCoroutine("ShowData");
    }

    void XMessageCallBack(string str)
    {
        Debug.Log("------------- str = " + str);
    }

        IEnumerator ShowData()
    {
        while (true)
        {
            text_data.text = "PoorSignal = " + ThinkGearManager.instance.GetWave_quality().ToString() + "\n"
                + "Attention = " + ThinkGearManager.instance.GetAttention().ToString() + "\n"
                + "Meditation = " + ThinkGearManager.instance.GetMeditation().ToString() + "\n"
                + "Raw = " + ThinkGearManager.instance.GetRaw().ToString() + "\n"
                + "Theta = " + ThinkGearManager.instance.GetTheta().ToString() + "\n"
                + "Delta = " + ThinkGearManager.instance.GetDelta().ToString() + "\n"
                + "LowAlpha = " + ThinkGearManager.instance.GetLowAlpha().ToString() + "\n"
                + "HighAlpha = " + ThinkGearManager.instance.GetHighAlpha().ToString() + "\n"
                + "LowBeta = " + ThinkGearManager.instance.GetLowBeta().ToString() + "\n"
                + "HighBeta = " + ThinkGearManager.instance.GetHighBeta().ToString() + "\n"
                + "LowGamma = " + ThinkGearManager.instance.GetLowGamma().ToString() + "\n"
                + "HighGamma = " + ThinkGearManager.instance.GetHighGamma().ToString() + "\n"
                + "眨眼 = " + ThinkGearManager.instance.GetBlink().ToString() + "\n";

            if (ThinkGearManager.instance.GetBatteryCapacity() > 0)//4.0蓝牙模块
            {
                text_data1.text = "蓝牙4.0模块固件版本 = " + ThinkGearManager.instance.GetHardwareversion4_0().ToString() + "\n"
                    + "蓝牙4.0模块陀螺仪x轴 = " + ThinkGearManager.instance.Getxvalue4_0().ToString() + "\n"
                    + "蓝牙4.0模块陀螺仪y轴 = " + ThinkGearManager.instance.Getyvalue4_0().ToString() + "\n"
                    + "蓝牙4.0模块陀螺仪z轴 = " + ThinkGearManager.instance.Getzvalue4_0().ToString() + "\n"
                    + "蓝牙4.0模块电量 = " + ThinkGearManager.instance.GetBatteryCapacity().ToString() + "\n"
                    + "喜好度 = " + ThinkGearManager.instance.GetAp4_0().ToString() + "\n"
                    + "咬牙 = " + ThinkGearManager.instance.GetGrind4_0().ToString() + "\n"
                    + "心率 = " + ThinkGearManager.instance.GetHeartRate().ToString() + "\n"
                    + "额温 = " + ThinkGearManager.instance.GetTemperature().ToString() + "\n";
            }

            yield return null;
        }
    }
}
