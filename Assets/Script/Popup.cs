using System;
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
    static public Dictionary<string, string[]> listParamsEachString = new Dictionary<string, string[]>()
    {
        {"Recurrent",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Fully",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Conv1D",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Conv2D",new string[]{"In_Chanels", "Out_Chanels"} },
        {"ReLU",new string[]{ "inplace"} },
        {"MaxPool2d",new string[]{"Kernel_size", "stride"} },
        {"DropDown",new string[]{ "p: float = 0.5","inplace: bool = False" } }
    };

    public void initPopup(string layerName)
    {
        this.initInputField(layerName);

        _buttonClickOK.onClick.AddListener(() =>
        {
            Debug.Log("OK Clicked");
            GameObject.Destroy(this.gameObject);
        });
        _buttonClickCancel.onClick.AddListener(() =>
        {
            Debug.Log("Cancel Clicked");
            GameObject.Destroy(this.gameObject);
        });

    }

    private void initInputField(string layerName)
    {
        Debug.Log("ok");
        layerName = "Conv1D";
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
