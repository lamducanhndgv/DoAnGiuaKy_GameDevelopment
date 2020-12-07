using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLayers : MonoBehaviour
{
    public static GenerateLayers Instance { get; private set;}

    public DragAndDropItem box;
    public float deltaX = .5f;
    public float deltaY = 1f;
    

    private enum UpDown { Down = -1, Start = 0, Up = 1 };
    private string content;
    private Vector2 MenuSize;
    private RectTransform rect;
    private void Awake()
    {
        Instance = this;
        rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        foreach (KeyValuePair<int, Color32> item in LayersConstants.LAYER_COLORS)
        {   
            if(item.Key != LayersConstants.INPUT)
                CreateBox(item, this.rect);
        }
    }

    public DragAndDropItem CreateBox(KeyValuePair<int, Color32> item, RectTransform rect)
    {
        DragAndDropItem it = Instantiate(box, rect, false);

        it.GetComponent<Image>().color = item.Value;

        RectTransform r = it.GetComponent<RectTransform>();
        
        r.sizeDelta = GetMenuSize();

        it.GetComponentInChildren<Text>().text = LayersConstants.LAYER_NAMES[item.Key];

        it.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);

        it.name = LayersConstants.LAYER_NAMES[item.Key];

        it.isIcon = true;

        return it;
    }

    public Vector2 GetMenuSize()
    {
        if (MenuSize == Vector2.zero)
            MenuSize = new Vector2(rect.rect.width - 20, 50);
        return MenuSize;
    }
}
