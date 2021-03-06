﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{
    public LineRenderer lr;
    private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        points = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        lr.useWorldSpace = false;
        RectTransform rec = this.GetComponent<RectTransform>();
        rec.anchorMin = new Vector2(0.5f, .5f);
        rec.anchorMax = new Vector2(0.5f, .5f);
        rec.SetParent(ContentSpace.instance.transform);
    }

    private void _UpdateLine()
    {
        if (points == null || points.Length == 0)
            return;

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 vec = new Vector3(points[i].position.x, points[i].position.y);
            lr.SetPosition(i, vec);
        }
    }


    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
        _UpdateLine();
    }

    public void UpdatePosition(Transform point, bool isFrom = false)
    {
        int index = isFrom ? 0 : 1;
        this.points[index] = point;
        this._UpdateLine();
    } 
    



    
}
