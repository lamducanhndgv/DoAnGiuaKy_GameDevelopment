using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set;}
    public bool connectStateClick;
    private Transform prev;

    private void Awake()
    {
        Instance = this;
        this.connectStateClick = false;
    }

    public void SetLastPoint(Transform p)
    {
        prev = p;
    }

    public void CreateLine(GameObject line, Transform to)
    {
        print(to);
        line.GetComponent<lr_LineController>().SetUpLine(new Transform[]{prev, to});
    }
}
