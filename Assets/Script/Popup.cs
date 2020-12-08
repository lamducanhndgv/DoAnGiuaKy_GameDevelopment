using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup : MonoBehaviour
{
    public GameObject ContentRow;

    public Button _buttonClickOK;
    public Button _buttonClickCancel;
    public GameObject ContentRender;
    public Constants.Layer layerAttr;

    public void Start()
    {
        initInputField(layerAttr.LayerName);

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
        //if (listParamsEachString.ContainsKey(layerName))
        //{
        //    string[] list = listParamsEachString[layerName];

        //    foreach (string val in list)
        //    {
        //        GameObject it = Instantiate(ContentRow, ContentRender.transform,false);
        //        it.transform.SetParent(ContentRender.transform);
        //        it.GetComponentInChildren<Text>().text = val;
        //    }
        //}
        foreach (Constants.Attribute attribute in layerAttr.attributes)
        {
            GameObject it = Instantiate(ContentRow, ContentRender.transform, false);
            it.transform.SetParent(ContentRender.transform);
            it.GetComponentInChildren<Text>().text = attribute.name;
        }
    }

    public static void CreatePopUp()
    {

    }
}
