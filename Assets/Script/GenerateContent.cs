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
        {"Recurrent",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Fully",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Conv1D",new string[]{"In_Chanels", "Out_Chanels"} },
        {"Conv2D",new string[]{"In_Chanels", "Out_Chanels"} },
        {"ReLU",new string[]{ "inplace"} },
        {"MaxPool2d",new string[]{"Kernel_size", "stride"} },
        {"DropDown",new string[]{ "p: float = 0.5","inplace: bool = False" } }
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

}
