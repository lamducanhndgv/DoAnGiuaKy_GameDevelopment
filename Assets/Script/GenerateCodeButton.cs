using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenerateCodeButton : MonoBehaviour
{
    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            //if(Graph.IsCyclic())
            //{
            //    Debug.Log("co chu trinh");
            //}
            //else
            //{
            //    Debug.Log("khong co chu trinh");
            //}
        });
    }
}
