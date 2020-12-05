using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLayers : MonoBehaviour
{
    Dictionary<string, Color32> styleDictionary = new Dictionary<string, Color32>();

    public DragAndDropItem box;
    public float deltaX = .5f;
    public float deltaY = 1f;

    private enum UpDown { Down = -1, Start = 0, Up = 1 };
    private Text text;
    private string content;

    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        styleDictionary.Add("Fully Connected", new Color32(0, 169, 157, 255));
        styleDictionary.Add("Convolutional 1D", new Color32(237, 199, 10, 255));
        styleDictionary.Add("Convolutional 2D", new Color32(0, 113, 188, 255));
        styleDictionary.Add("Recurrent", new Color32(158, 0, 93, 255));
        styleDictionary.Add("Drop-out", new Color32(27, 20, 100, 255));
        styleDictionary.Add("Maxpool 2D", new Color32(184, 184, 184, 255));
        styleDictionary.Add("Relu", new Color32(251, 176, 59, 255));
    }
    private void Start()
    {
        foreach (KeyValuePair<string, Color32> item in styleDictionary)
        {
            DragAndDropItem it = Instantiate(box, rect, false);
            it.GetComponent<Image>().color = item.Value;
            RectTransform r = it.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(rect.rect.width - 20, 50);
            it.isIcon = true;
            it.GetComponentInChildren<Text>().text = item.Key;
            it.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        }
    }
}
