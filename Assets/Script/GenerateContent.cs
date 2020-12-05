using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateContent : MonoBehaviour
{
    public GameObject gameObject;
    public string layerName;
    static public Dictionary<string, string[]> listParamsEachString = new Dictionary<string, string[]>()
    {
        {"Conv2D",new string[]{"Parasm1", "Params2"} }
    };
    // Start is called before the first frame update
    void Start()
    {
        layerName = "Conv2D";
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
