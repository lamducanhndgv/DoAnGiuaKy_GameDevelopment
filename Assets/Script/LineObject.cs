using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineObject 
{
    /// LOP MIEU TA 1 LINE : BAO GOM DIEM DAU, DIEM CUOI, ID DIEM DAU, ID DIEM CUOI
    private DragAndDropItem start;
    private DragAndDropItem end;

    private int idStart;
    private int idEnd;
    public LineObject() {
        start = end= null;
        idEnd = idStart = -1;
    }
    public LineObject(DragAndDropItem start, DragAndDropItem  end, int idStart, int idEnd)
    {
        this.start = start;
        this.end = end;
        this.idStart = idStart;
        this.idEnd = idEnd;
    }

    public DragAndDropItem Start { get => start; set => start = value; }
    public DragAndDropItem End { get => end; set => end = value; }
    public int IdStart { get => idStart; set => idStart = value; }
    public int IdEnd { get => idEnd; set => idEnd = value; }


    public void setEnd(DragAndDropItem rect, int id) {
        end = rect;
        idEnd = id;
    }
    public void setStart(DragAndDropItem rect, int id)
    {
        start = rect;
        idStart = id;
    }

    public string getName() {
        return LineManager.instance._MakeKeyString(start.id, end.id);
    }
}
