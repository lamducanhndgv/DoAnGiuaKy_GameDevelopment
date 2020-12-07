using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject prev;
    private Dictionary<string, GameObject> LineLookUp;


    public bool connectStateClick;
    public static StateManager Instance { get; private set;}


    private void Awake()
    {
        Instance = this;
        this.connectStateClick = false;
        this.LineLookUp = new Dictionary<string, GameObject>();
    }

    private string _CreateKey(GameObject toObj)
    {
        DragAndDropItem from = prev.GetComponent<DragAndDropItem>();
        DragAndDropItem to = toObj.GetComponent<DragAndDropItem>();

        return this._MakeKeyString(from.id, to.id);
    }

    private string _MakeKeyString(int a, int b)
    {
        if (b > a)
            return $"{b}-{a}";
        return $"{a}-{b}";
    }

    private GameObject _ConstructLine()
    {
        GameObject line = new GameObject("newline");
        line.AddComponent<LineRenderer>();
        line.AddComponent<lr_LineController>();
        line.AddComponent<RectTransform>();

        // Set localScale 
        RectTransform rec = line.GetComponent<RectTransform>();
        rec.localScale = new Vector3(1, 1, 1);

        // Set width of line
        line.GetComponent<LineRenderer>().startWidth = 0.1f;
        line.GetComponent<LineRenderer>().endWidth = 0.1f;

        line.transform.SetParent(ContentSpace.instance.transform);
        rec.anchoredPosition3D = new Vector3(rec.anchoredPosition3D.x, rec.anchoredPosition3D.y, -10f);

        return line;
    }

    /* 
     * Public functions
     */

    public void SetLastPoint(GameObject from)
    {
        prev = from;
    }

    public void CreateLine(GameObject to)
    {
        GameObject line = this._ConstructLine();

        line.GetComponent<lr_LineController>().SetUpLine(new Transform[]{prev.transform, to.transform});

        string key = _CreateKey(to);
            
        this.LineLookUp[key] = line;
    }

    public void RemoveLine(DragAndDropItem layer)
    {
        List<string> keys = new List<string>();

        if (layer.ancestor != null)
            keys.Add(this._MakeKeyString(layer.id, layer.ancestor.id));

        if (layer.children != null)
            keys.Add(this._MakeKeyString(layer.id, layer.children.id));

        foreach(string key in keys)
        {
            Destroy(this.LineLookUp[key]);
            this.LineLookUp.Remove(key);
        }
    }

    public void Cancel()
    {
        prev = null;
        connectStateClick = false;
    }
    

    
}
