using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;
    public bool isIcon = false;
    public string myTag = "SpaceGroup";

    private DragAndDropItem item;
    
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
            Debug.Log("Right mouse Button Clicked on: " + name);
        }
        



        //// Di chuyen
        if (isIcon)
        {
            item = Instantiate(this, rect, false);
            item.isIcon = false;
            item.tag = myTag;
            item.transform.GetChild(0).tag = myTag;
            RectTransform rec = item.GetComponent<RectTransform>();
            rec.anchorMin = new Vector2(0.5f, .5f);
            rec.anchorMax = new Vector2(0.5f, .5f);
            rec.SetParent(ContentSpace.instance.transform);
        }
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
