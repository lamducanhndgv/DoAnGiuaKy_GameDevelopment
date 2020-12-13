﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static int count = 0;

    public DragAndDropItem children;
    public DragAndDropItem ancestor;

    [SerializeField] private Canvas canvas;

    private RectTransform _rect;
    public RectTransform Rect { get => _rect; set => _rect = value; }

    public bool isIcon = false;
    public static string myTag = "SpaceGroup";

    public int id;
    public int layerid;


    private Constants.Layer layer = null;
    private DragAndDropItem item;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

        if (layer == null)
            layer = Constants.LayerFactory.BuildLayer(this.layerid);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform r = null;

        if (!isIcon)
        {
            Rect.anchoredPosition += eventData.delta / canvas.scaleFactor ;
            r = Rect;
        }
        else { 
            item.Rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
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

        // StateManager.Instance.UpdateLine(this);
        LineManager.instance.UpdateLine(this);
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
                // StateManager.Instance.Remove(this);
                LineManager.instance.RemoveLine(this);
                Destroy(this.gameObject);
            }
                
        }
        item = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isIcon)
        {
            this._CreateLayer();
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
    }

    public string GetStringLayer()
    {
        if (layer == null)
            return "";
        return layer.ToString();
    }

    public string GetLayerName()
    {
        return layer.LayerName.Substring(3);
    }

    private void _HandleRightClick()
    {
        //   Debug.Log("Right mouse Button Clicked on: " + name);

        if (id != 0)
        {
            //if (StateManager.Instance.connectStateClick)
            //{
            //    LineHelper.second = this;

            //    if (!LineHelper.IsOk())
            //    {
            //        Debug.Log("Line existed");
            //        StateManager.Instance.Cancel();
            //        LineHelper.Cancel();
            //        return;
            //    }

            //    StateManager.Instance.CreateLine(gameObject);

            //    LineHelper.Connect();
            //}

            //else
            //{
            //    LineHelper.first = this;
            //    StateManager.Instance.SetLastPoint(gameObject);
            //}

            //StateManager.Instance.connectStateClick = !StateManager.Instance.connectStateClick;
            LineManager.instance.createLine(this);
        }
    }

    
    private void _CreateLayer()
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
        rec.anchoredPosition3D = new Vector3(rec.anchoredPosition3D.x, rec.anchoredPosition3D.y, -10f);

        StateManager.Instance.LayerLookUp[item.id.ToString()] = item.gameObject;
        
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

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                

                // Show dialog
                GameObject Popup = Instantiate(StateManager.Instance.Popup, ContentSpace.instance.MyRect, false);
                Popup.GetComponent<Popup>().layerAttr = layer;
                RectTransform r = Popup.GetComponent<RectTransform>();
                r.anchorMin = new Vector2(0.2f, 0.2f);
                r.anchorMax = new Vector2(0.8f, 0.8f);
                r.localScale = new Vector3(1f, 1f, 1f);
            }

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
        }
        catch (Exception)
        {
            hl = false;
        }
        return hl;
    }
}
