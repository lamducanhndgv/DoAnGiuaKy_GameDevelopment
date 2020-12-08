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
        button.onClick.AddListener(() => {
            List<DragAndDropItem> result = CollectInfo();
            ScriptWriter.Instance.WriteToFile(result);
        });
    }

    private List<DragAndDropItem> CollectInfo()
    {
        return Graph.CollectNode();
    }
}
