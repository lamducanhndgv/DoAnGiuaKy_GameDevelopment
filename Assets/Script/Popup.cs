﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup : MonoBehaviour
{
    public static Popup instance;
    [SerializeField] Button _buttonClickOK;
    [SerializeField] Button _buttonClickCancel;
    [SerializeField] GameObject ContentToRender;
    public string layerName;
    static public Dictionary<string, string[]> listParamsEachString = new Dictionary<string, string[]>()
    {
        {"Recurrent",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Fully Connected",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Convolutional 1D",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Convolutional 2D",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Relu",new string[]{ "inplace"} },
        {"MaxPool 2D",new string[]{"Kernel_size", "stride"} },
        {"Drop-out",new string[]{ "p: float = 0.5","inplace: bool = False" } }
    };
    public void Start()
    {
        initInputField(layerName);

        _buttonClickOK.onClick.AddListener(() =>
        {
            Debug.Log("OK Clicked");
            Destroy(this.gameObject);
        });
        _buttonClickCancel.onClick.AddListener(() =>
        {
            Debug.Log("Cancel Clicked");
            Destroy(this.gameObject);
        });
    }


    private void initInputField(string layerName)
    {
        layerName = "Recurrent";
        if (listParamsEachString.ContainsKey(layerName))
        {
            string[] list = listParamsEachString[layerName];
            foreach (string val in list)
            {
                GameObject it = Instantiate(gameObject, this.transform);

                it.GetComponentInChildren<Text>().text = val;
            }
        }
    }
}
