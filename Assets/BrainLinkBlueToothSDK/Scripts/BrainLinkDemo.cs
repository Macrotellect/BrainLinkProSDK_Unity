using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScanDevice
{
    public string name;
    public string identyfierOrAdress;
    public string  riss;
}
public class BrainLinkDemo : MonoBehaviour
{
    public List<ScanDevice> scanDeviceList = new List<ScanDevice>();
    public UITableView _tabTableView;
    public GameObject toggles;
    public GameObject scanDevicesPanel;
    public Text text_data, text_data1;
    public GameObject obj_textTip_android; 
    public GameObject obj_textTip_ios; 
    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    
    private void Start()
    {
#if UNITY_ANDROID
        obj_textTip_android.SetActive(true);
#else
        obj_textTip_ios.SetActive(true);
#endif
        ThinkGearManager.instance.receiveScanDevice.AddListener(ReceiveScanDevice);
        toggles.SetActive(false);
        scanDevicesPanel.SetActive(false);
    }

    public void CloseScanDeviceListPanel()
    {
        scanDeviceList.Clear();
        _tabTableView.reload(scanDeviceList.Count);
        StopCoroutine("RefreshDeviceList");
        scanDevicesPanel.SetActive(false);
        toggles.SetActive(false);
    }
    
    public void ClickScan()
    {
        Debug.Log("unity=== ClickScan");
        toggles.SetActive(true);
        scanDevicesPanel.SetActive(true);
        scanDeviceList.Clear();
        
        _tabTableView.setModel(Resources.Load("ImgTableViewCell"));
        ThinkGearManager.instance.Scan();
        StartCoroutine("RefreshDeviceList");
        StartCoroutine("ShowData");
    }

    IEnumerator RefreshDeviceList()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            _tabTableView.onCellFill((GameObject item, int index) =>
            {
                ScanDevice device = scanDeviceList[index];
                item.GetComponent<ImageCell>().SetContent(device.name, device.identyfierOrAdress, device.riss);
                                
            });
            _tabTableView.onCellClick((GameObject item, int index) =>
            {
                ScanDevice device = scanDeviceList[index];
                ThinkGearManager.instance.connectDevice(device.identyfierOrAdress);
            });
            _tabTableView.reload(scanDeviceList.Count);
            Debug.Log("unity=== 1 second has passed!");
        }
    }

    public void ReceiveScanDevice(string nameAddressRiss)
    {
        string[] arr = nameAddressRiss.Split(',');
        ScanDevice newScanDevice = new ScanDevice();
        newScanDevice.name = arr[0];
        newScanDevice.identyfierOrAdress = arr[1];
        newScanDevice.riss = arr[2];
        
        if (scanDeviceList.Count == 0)
        {
            scanDeviceList.Add(newScanDevice);
        }
        else
        {
            bool isFind = false;
            foreach (var s in scanDeviceList)
            {
                ScanDevice device = s;
                if (device.identyfierOrAdress == newScanDevice.identyfierOrAdress)
                {
                    isFind = true;
                }
            }
            if(!isFind){
                scanDeviceList.Add(newScanDevice);
            }
        }

    }

    void XMessageCallBack(string str)
    {
        Debug.Log("------------- str = " + str);
    }

        IEnumerator ShowData()
    {
        while (true)
        {
            if (ThinkGearManager.instance.GetDelta() > 0)
            {
                if (scanDevicesPanel.activeSelf)
                {
                    scanDevicesPanel.SetActive(false);
                    scanDeviceList.Clear();
                    toggles.SetActive(true);
                }
            }
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
                                  + "额温 = " + ThinkGearManager.instance.GetTemperature().ToString() + "\n"
                                  + "RR间隔值 =" + ThinkGearManager.instance.GetHRV().ToString() + "\n"
                                  + "RR间隔是用来计算HRV的原始数据(单位ms)";
            }

            yield return null;
        }
    }
}
