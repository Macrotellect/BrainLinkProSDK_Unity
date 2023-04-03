using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class ImageCell : MonoBehaviour
{
    public Text nameContent;
    public Text identifierOrAddressTitle;
    public Text identifierOrAddresContent;
    public Text risiContent;
    public List<Image> risiImages;

    public void Start()
    {
#if UNITY_IPHONE
        identifierOrAddressTitle.text = "identifier";
#else
        identifierOrAddressTitle.text = "mac";
#endif
    }
    

    public void SetContent(string name, string identifierOrAddress, string risi)
    {
        nameContent.text = name;
        identifierOrAddresContent.text = identifierOrAddress;
        int risiVaule = int.Parse(risi);
        risiContent.text = risi + "";
        int index = 0;
        if (risiVaule<= -90) {
            //没有
            index = 0;
        }
        else  if (risiVaule<= -72) {
            //1
            index = 1;
        }
        else  if (risiVaule<= -54) {
            //2
            index = 2;
        }
    
        else  if (risiVaule<= -36) {
            //3
            index = 3;
        }
        else  if (risiVaule<= -18) {
            //4
            index = 4;
        }else{
            //5
            index = 5;
        }
        
        for (int i = 0; i < risiImages.Count; i++) {
            Image image = risiImages[i];
            if (i < index) {
                image.gameObject.SetActive(true);
            }
            else
            {
                image.gameObject.SetActive(false);
            }
        }
    }

 
}
