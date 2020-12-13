using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Constants;

public class StateManager : MonoBehaviour
{
    private GameObject prev;

    private Dictionary<string, GameObject> LineLookUp;
    
    private char[] separator = { '-' };


    public bool connectStateClick;
    public GameObject Popup;

    public Dictionary<string, GameObject> LayerLookUp;
    public static StateManager Instance { get; private set;}
    

    private void Awake()
    {
        Instance = this;
        this.connectStateClick = false;
        this.LineLookUp = new Dictionary<string, GameObject>();
        this.LayerLookUp = new Dictionary<string, GameObject>();  
    }

    private void Start()
    {
        KeyValuePair<int, Color32> kv = new KeyValuePair<int, Color32>(
                Constants.LayersConstants.INPUT,
                Constants.LayersConstants.LAYER_COLORS[Constants.LayersConstants.INPUT]
            );

        DragAndDropItem item = GenerateLayers.Instance.CreateBox(kv, ContentSpace.instance.MyRect);

        item.id = ++DragAndDropItem.count;
        item.isIcon = false;
        item.tag = DragAndDropItem.myTag;
        item.transform.GetChild(0).tag = DragAndDropItem.myTag;

        item.ancestor = item;

        Graph.root = item;

        RectTransform rec = item.GetComponent<RectTransform>();
        rec.anchorMin = new Vector2(0.5f, .5f);
        rec.anchorMax = new Vector2(0.5f, .5f);
        rec.SetParent(ContentSpace.instance.transform);

        rec.anchoredPosition3D = new Vector3(0f, 200f, -10f);

        StateManager.Instance.LayerLookUp[item.id.ToString()] = item.gameObject;
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

    private GameObject _ConstructLine(string name="undefined")
    {
        GameObject line = new GameObject(name);

        // line.AddComponent<LineRenderer>();
        line.AddComponent<UILineRenderer>();
        line.AddComponent<lr_LineController>();

        line.AddComponent<RectTransform>();

        // Set localScale 
        RectTransform rec = line.GetComponent<RectTransform>();
        
        
        // Set width of line
        //line.GetComponent<LineRenderer>().startWidth = 0.1f;
        //line.GetComponent<LineRenderer>().endWidth = 0.1f;

        //line.GetComponent<LineRenderer>().startColor = new Color(255, 255, 255);
        //line.GetComponent<LineRenderer>().endColor = new Color(255, 255, 255);

        line.transform.SetParent(ContentSpace.instance.transform);
        rec.sizeDelta = ContentSpace.instance.MyRect.sizeDelta;
        rec.localScale = new Vector3(1, 1, 1);


        rec.anchoredPosition3D = ContentSpace.instance.MyRect.anchoredPosition3D; // new Vector3(rec.anchoredPosition3D.x, rec.anchoredPosition3D.y, -10f);
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
            line.GetComponent<lr_LineController>().SetUpLine(new Transform[]{prev.transform, to.transform });

        this.LineLookUp[key] = line;
    }

    public void Remove(DragAndDropItem layer)
    {
        LayerLookUp.Remove(layer.id.ToString());

        _RemoveLine(layer);
    }


    
    public void UpdateLine(DragAndDropItem layer)
    {
        string key;

        if (layer.id != 1 && layer.ancestor != null)
        {
            key = this._MakeKeyString(layer.id, layer.ancestor.id);
            LineLookUp[key].GetComponent<lr_LineController>().UpdatePosition(layer.gameObject.transform, true);
        }

        if (layer.children != null)
        {
            key = this._MakeKeyString(layer.id, layer.children.id);
            LineLookUp[key].GetComponent<lr_LineController>().UpdatePosition(layer.gameObject.transform, false);
        }
    }

    public void Cancel()
    {
        prev = null;
        connectStateClick = false;
    }
    

    
}
