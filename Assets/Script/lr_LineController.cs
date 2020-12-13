using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class lr_LineController : MonoBehaviour
{
    public UILineRenderer lr;
    public RectTransform rect;

    private Transform[] points;

    private void Awake()
    {
        // lr = GetComponent<LineRenderer>();
        lr = GetComponent<UILineRenderer>();
        lr.Points = new Vector2[2];
        lr.LineThickness = 2;

        points = null;
    }

    // Start is called before the first frame update
    void Start()
    {
       /// lr.useWorldSpace = false;
        rect = this.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, .5f);
        rect.anchorMax = new Vector2(0.5f, .5f);
        rect.SetParent(ContentSpace.instance.transform);
        print("Hoan Thanh");
    }

    private void _UpdateLine()
    {
        if (points == null || points.Length == 0)
            return;

        for (int i = 0; i < points.Length; i++)
        {
            // Vector3 vec = new Vector3(points[i].position.x, points[i].position.y);
            // lr.SetPosition(i, vec);
            lr.Points[i] = points[i].position;
        }
    }


    public void SetUpLine(Transform[] points)
    {
        // lr.positionCount = points.Length;
        this.points = points;
        _UpdateLine();
    }

    public void UpdatePosition(Transform point, bool isFrom = false)
    {
        int index = isFrom ? 0 : 1;
        this.points[index] = point;

        gameObject.transform.SetParent(ContentSpace.instance.transform);
        rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, rect.anchoredPosition3D.y, -10f);

        this._UpdateLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(points[0].position);
            print(points[1].position);
        }
    }





}
