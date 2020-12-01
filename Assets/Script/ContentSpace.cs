using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContentSpace : MonoBehaviour
{
    public static ContentSpace instance;

    private RectTransform myRect;
    private Canvas canvas;
    private Vector2 preSize;
    private Vector2 defaultSize;


    public float delta = 120f;
    public enum DIRECTION { LEFT, RIGHT, UP, DOWN };
    public Scrollbar VerticalScrollbar;
    public Scrollbar HorizontalScrollbar;




    public RectTransform MyRect { get => myRect; set => myRect = value; }
    public Vector2 DefaultSize { get => defaultSize; set => defaultSize = value; }

    private void Awake()
    {
        instance = this;
        MyRect = GetComponent<RectTransform>();
        canvas = GameObject.FindObjectOfType<Canvas>();
    }
    private void Start()
    {
        MyRect.anchoredPosition = Vector2.zero;
        DefaultSize = MyRect.sizeDelta;
    }

    public void setSize(DIRECTION d, RectTransform re) {
        preSize = MyRect.sizeDelta;
        if (d == DIRECTION.UP) {
            re.SetParent(canvas.transform);
            MyRect.sizeDelta = new Vector2(MyRect.sizeDelta.x, MyRect.sizeDelta.y + delta);
            VerticalScrollbar.value = 1;
            re.SetParent(MyRect);

        }
        else if (d == DIRECTION.DOWN)
        {
            re.SetParent(canvas.transform);
            MyRect.sizeDelta = new Vector2(MyRect.sizeDelta.x, MyRect.sizeDelta.y + delta);
            re.SetParent(MyRect);
            VerticalScrollbar.value = 0;
        }

        else if (d == DIRECTION.LEFT)
        {
            re.SetParent(canvas.transform);
            MyRect.sizeDelta = new Vector2(MyRect.sizeDelta.x + delta, MyRect.sizeDelta.y);
            re.SetParent(MyRect);
        }
        else if (d == DIRECTION.RIGHT)
        {
            re.SetParent(canvas.transform);
            MyRect.sizeDelta = new Vector2(MyRect.sizeDelta.x + delta, MyRect.sizeDelta.y);
            re.SetParent(MyRect);
        }


       // print(MyRect.sizeDelta);
    }


    public void restoreSize() {
        MyRect.sizeDelta = preSize;
    }
    
}
