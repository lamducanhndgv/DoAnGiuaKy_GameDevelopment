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
            foreach(DragAndDropItem item in result)
            {
                print(item.id);
            }
        });
    }

    private List<DragAndDropItem> CollectInfo()
    {
        List<DragAndDropItem> result = new List<DragAndDropItem>();

        return result;
    }
}
