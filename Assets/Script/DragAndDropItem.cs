﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static int count = 0;

    public List<int> children = new List<int>();

    private RectTransform rect;
    public bool isIcon = false;
    public string myTag = "SpaceGroup";

    public int id;

    private DragAndDropItem item;

    public lr_LineController lr;
    public RectTransform Rect { get => rect; set => rect = value; }


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

        if (!ok(eventData) || isIcon) return;

        float detal = 70f;

        if (r.anchoredPosition.y >= ContentSpace.instance.MyRect.sizeDelta.y/2 - detal)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.UP, r);
        else if (r.anchoredPosition.y <= -ContentSpace.instance.MyRect.sizeDelta.y / 2 + detal)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.DOWN, r);
        else if (r.anchoredPosition.x >= ContentSpace.instance.MyRect.sizeDelta.x/2 - detal)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.RIGHT, r);
        else if (r.anchoredPosition.x <= -ContentSpace.instance.MyRect.sizeDelta.x / 2 + detal)
            ContentSpace.instance.setSize(ContentSpace.DIRECTION.LEFT, r);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        if (!ok(eventData))
        {
            ContentSpace.instance.restoreSize();
            if (isIcon) Destroy(item.gameObject);
            else Destroy(this.gameObject);
        }
        item = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this._HandleRightClick();
        }
        

        //// Di chuyen
        if (isIcon)
        {
            item = Instantiate(this, rect, false);
            item.id = ++DragAndDropItem.count;
            item.isIcon = false;
            item.tag = myTag;
            item.transform.GetChild(0).tag = myTag;

            RectTransform rec = item.GetComponent<RectTransform>();

            rec.anchorMin = new Vector2(0.5f, .5f);
            rec.anchorMax = new Vector2(0.5f, .5f);

            rec.SetParent(ContentSpace.instance.transform);
        }
    }

    private void _HandleRightClick()
    {
        //   Debug.Log("Right mouse Button Clicked on: " + name);

        if (StateManager.Instance.connectStateClick)
        {
            // Debug.Log("Create LineRenderer");

            GameObject line = this._ConstructLine();

            StateManager.Instance.CreateLine(line, this.transform);

            //    Debug.Log("Connect to object");
        }
        else
        {
            StateManager.Instance.SetLastPoint(this.transform);
            //   Debug.Log("Activate connect 2 boxes state");
        }

        StateManager.Instance.connectStateClick = !StateManager.Instance.connectStateClick;
    }

    private GameObject _ConstructLine()
    {
        GameObject line = new GameObject("newline");

        line.AddComponent<LineRenderer>();
        line.AddComponent<lr_LineController>();
        line.AddComponent<RectTransform>();

        RectTransform rec = line.GetComponent<RectTransform>();

        rec.localScale = new Vector3(1, 1, 1);

        line.GetComponent<LineRenderer>().startWidth = 0.1f;
        line.GetComponent<LineRenderer>().endWidth = 0.1f;

        line.transform.SetParent(ContentSpace.instance.transform);
        rec.anchoredPosition3D = new Vector3(rec.anchoredPosition3D.x, rec.anchoredPosition3D.y, -10f);

        return line;
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
