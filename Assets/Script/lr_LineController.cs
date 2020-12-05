﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{ 
    private LineRenderer lr;
    private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }


    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
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

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 vec = new Vector3(points[i].transform.localPosition.x, points[i].transform.localPosition.y);
            lr.SetPosition(i, vec);
        }
    }
}
