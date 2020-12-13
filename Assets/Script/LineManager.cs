using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;


public class LineManager : MonoBehaviour
{
    private UILineRenderer lineMana = null;
    [SerializeField] private List<LineObject> listLine = new List<LineObject>();
    [SerializeField] private List<Vector2> pointlist;

    private Dictionary<string, int> lineLookUp = new Dictionary<string, int>();

    private bool firstClick = true;
    public List<LineObject> ListLine { get => listLine; set => listLine = value; }
    public bool FirstClick { get => firstClick; private set => firstClick = value; }
    public static LineManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
        lineMana = GetComponent<UILineRenderer>();
        lineMana.enabled = false;
    }

    public void createLine(DragAndDropItem d) {
        if (firstClick)
        {
            LineHelper.first = d;

            ListLine.Add(new LineObject());
            ListLine[ListLine.Count - 1].setStart(d, d.id);
        }
        else {
            LineHelper.second = d;

            if (!LineHelper.IsOk())
            {
                Debug.Log("Line existed");
                StateManager.Instance.Cancel();
                LineHelper.Cancel();
                return;
            }
            ListLine[ListLine.Count - 1].setEnd(d, d.id);

            List<Vector2> p = new List<Vector2>(lineMana.Points);
            p.Add(ListLine[ListLine.Count - 1].Start.Rect.anchoredPosition);
            p.Add(ListLine[ListLine.Count - 1].End.Rect.anchoredPosition);
            
            if(!lineMana.enabled)
                lineMana.enabled = true;
            
            lineMana.Points = p.ToArray();

            this.lineLookUp[ListLine[ListLine.Count - 1].getName()] = ListLine.Count - 1;

            LineHelper.Connect();
        }
        firstClick = !firstClick;
    }

    public void RemoveLine(DragAndDropItem d)
    {
        string key = "";
        int index = -1;

        pointlist = new List<Vector2>();

        if (d.id != 1 && d.ancestor != null)
        {
            key = this._MakeKeyString(d.id, d.ancestor.id);
            index = 2 * this.lineLookUp[key];

            this.listLine.RemoveAt(this.lineLookUp[key]);
        }

        if (d.children != null)
        {
            key = this._MakeKeyString(d.id, d.children.id);
            index = 2 * this.lineLookUp[key];

            this.listLine.RemoveAt(this.lineLookUp[key]);
        }

        this._UpdateLineLookUp();

        foreach(var line in this.listLine)
        {
            pointlist.Add(line.Start.Rect.anchoredPosition);
            pointlist.Add(line.End.Rect.anchoredPosition);
        }
        
        lineMana.Points = pointlist.ToArray();

        if (lineMana.Points.Length == 0)
        {
            lineMana.enabled = false;
        }
    }


    public string _MakeKeyString(int a, int b)
    {
        if (b > a)
            return $"{b}-{a}";
        return $"{a}-{b}";
    }
    public void UpdateLine(DragAndDropItem d) { 
        string key = "";
        int index = -1;

        pointlist = new List<Vector2>(lineMana.Points);

        if (d.id != 1 && d.ancestor != null)
        {
            key = this._MakeKeyString(d.id, d.ancestor.id);
            index = 2 * this.lineLookUp[key];
            pointlist[index] = d.Rect.anchoredPosition;            
        }

        if (d.children != null)
        {
            key = this._MakeKeyString(d.id, d.children.id);
            index = 2 * this.lineLookUp[key] + 1;
            pointlist[index] = d.Rect.anchoredPosition;
        }

        lineMana.Points = pointlist.ToArray();
        
    }

    private void _UpdateLineLookUp()
    {
        for(int i = 0; i < this.listLine.Count; i++)
        {
            this.lineLookUp[this.listLine[i].getName()] = i;
        }
    }

}
