using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static int count = 0;

    public DragAndDropItem children;
    public DragAndDropItem ancestor;

    private RectTransform _rect;

    public bool isIcon = false;
    public string myTag = "SpaceGroup";

    public int id;

    private DragAndDropItem item;

    public lr_LineController lr;
    public RectTransform Rect { get => _rect; set => _rect = value; }


    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform r = null;

        if (!isIcon)
        {
            Rect.anchoredPosition += eventData.delta;
            r = Rect;
        }
        else { 
            item.Rect.anchoredPosition += eventData.delta;
            r = item.Rect;
        }

        if (!ok(eventData) || isIcon)
            return;

        float delta = 70f;

        if (r.anchoredPosition.y >= ContentSpace.instance.MyRect.sizeDelta.y/2 - delta)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.UP, r);

        else if (r.anchoredPosition.y <= -ContentSpace.instance.MyRect.sizeDelta.y / 2 + delta)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.DOWN, r);

        else if (r.anchoredPosition.x >= ContentSpace.instance.MyRect.sizeDelta.x/2 - delta)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.RIGHT, r);

        else if (r.anchoredPosition.x <= -ContentSpace.instance.MyRect.sizeDelta.x / 2 + delta)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.LEFT, r);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ok(eventData))
        {
            ContentSpace.instance.restoreSize();

            if (isIcon)
                Destroy(item.gameObject);
            else
            {
                StateManager.Instance.RemoveLine(this);
                Destroy(this.gameObject);
            }
                
        }
        item = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isIcon)
        {
            // Create layer to ContentSpace

            item = Instantiate(this, this.Rect, false);
            item.id = ++DragAndDropItem.count;
            item.isIcon = false;
            item.tag = myTag;
            item.transform.GetChild(0).tag = myTag;

            RectTransform rec = item.GetComponent<RectTransform>();
            rec.anchorMin = new Vector2(0.5f, .5f);
            rec.anchorMax = new Vector2(0.5f, .5f);

            rec.SetParent(ContentSpace.instance.transform);
        }
        else
        {
            this._HandleNotIconPointerDown(eventData);
        }
    }

    public void OnDestroy()
    {
        if (_checkNotNull(ancestor))
            ancestor.children = null;

        if (_checkNotNull(children))
            children.ancestor = null;
        
        Graph.node.Remove(this);
    }

    private void _HandleRightClick()
    {
        //   Debug.Log("Right mouse Button Clicked on: " + name);

        if (id != 0)
        {
            if (StateManager.Instance.connectStateClick)
            {
                LineHelper.second = this;

                if (!LineHelper.IsOk())
                {
                    Debug.Log("Line existed");
                    StateManager.Instance.Cancel();
                    LineHelper.Cancel();
                    return;
                }

                StateManager.Instance.CreateLine(gameObject);

                LineHelper.Connect();
            }

            else
            {
                LineHelper.first = this;
                StateManager.Instance.SetLastPoint(gameObject);
            }

            StateManager.Instance.connectStateClick = !StateManager.Instance.connectStateClick;
        }
    }

    

    private void _HandleNotIconPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this._HandleRightClick();
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clicked Item ID:" + this.id.ToString());
        }
    }

    private bool _checkNotNull(object o)
    {
        return o != null;
    }
    private bool ok(PointerEventData eventData) {
        bool hl = true;
        try
        {
            hl = eventData.pointerCurrentRaycast.gameObject.CompareTag(myTag);
        } catch (Exception e) {
            hl = false;
        }
        return hl;
    }
}
