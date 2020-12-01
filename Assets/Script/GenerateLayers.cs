using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLayers : MonoBehaviour
{
    public List<Color32> colors;
    public DragAndDropItem box;


    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        foreach (Color32 c in colors) {
            DragAndDropItem it = Instantiate(box, rect, false);
            it.GetComponent<Image>().color = c;
            it.isIcon = true;
        }
    }
}
