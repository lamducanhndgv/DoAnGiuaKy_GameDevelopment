using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject prev;

    private Dictionary<string, GameObject> LineLookUp;
    
    private char[] separator = { '-' };


    public bool connectStateClick;
    public Dictionary<string, GameObject> LayerLookUp;
    public static StateManager Instance { get; private set;}
    

    private void Awake()
    {
        Instance = this;
        this.connectStateClick = false;
        this.LineLookUp = new Dictionary<string, GameObject>();
        this.LayerLookUp = new Dictionary<string, GameObject>();  
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
            return $"{a}-{b}";
        return $"{b}-{a}";
    }

    private GameObject _ConstructLine(string name="undefined")
    {
        GameObject line = new GameObject(name);
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

        return line;
    }
    private void _RemoveLine(DragAndDropItem layer)
    {
        List<string> keys = new List<string>();

        if (layer.ancestor != null)
            keys.Add(this._MakeKeyString(layer.id, layer.ancestor.id));

        if (layer.children != null)
            keys.Add(this._MakeKeyString(layer.id, layer.children.id));

        foreach (string key in keys)
        {
            Destroy(this.LineLookUp[key]);
            this.LineLookUp.Remove(key);
        }
    }


    /* 
     * Public functions
     */

    public void SetLastPoint(GameObject from)
    {
        prev = from;
    }

    public void CreateLine(GameObject to, GameObject from = null)
    {
        string key = _CreateKey(to);

        GameObject line = this._ConstructLine($"LINE({key})");
        
        if (from)
            line.GetComponent<lr_LineController>().SetUpLine(new Transform[]{from.transform, to.transform});
        else
            line.GetComponent<lr_LineController>().SetUpLine(new Transform[] { prev.transform, to.transform });

        this.LineLookUp[key] = line;
    }

    public void Remove(DragAndDropItem layer)
    {
        LayerLookUp.Remove(layer.id.ToString());

        _RemoveLine(layer);
    }


    
    public void UpdateLine()
    {

    }

    public void Cancel()
    {
        prev = null;
        connectStateClick = false;
    }
    

    
}
