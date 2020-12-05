using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLayers : MonoBehaviour
{
    public DragAndDropItem box;
    public float deltaX = .5f;
    public float deltaY = 1f;

    private enum UpDown { Down = -1, Start = 0, Up = 1 };
    private string content;

    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        foreach (KeyValuePair<int, Color32> item in LayersConstants.LAYER_COLORS)
        {
            DragAndDropItem it = Instantiate(box, rect, false);

            it.GetComponent<Image>().color = item.Value;
            RectTransform r = it.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(rect.rect.width - 20, 50);
            
            it.GetComponentInChildren<Text>().text = LayersConstants.LAYER_NAMES[item.Key];

            it.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);

            it.name = LayersConstants.LAYER_NAMES[item.Key];
            it.isIcon = true;
        }
    }
}
