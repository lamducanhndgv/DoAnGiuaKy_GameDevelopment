using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineHelper
{
    public static Stack<DragAndDropItem> waitingVertices = new Stack<DragAndDropItem>();
    public static void Connect()
    {
        DragAndDropItem parent = waitingVertices.Pop();
        DragAndDropItem child = waitingVertices.Pop();

        Cancel();

        Debug.Log("connected: " + parent.id + " -> " + child.id);
        parent.children.Add(child);
    }
    public static void Cancel()
    {
        waitingVertices.Clear();
    }

    public static bool IsConnected()
    {
        DragAndDropItem item1 = waitingVertices.Pop();
        DragAndDropItem item2 = waitingVertices.Pop();

        if(item1.children.Contains(item2) || item2.children.Contains(item1))
        {
            return true;
        }
        else
        {
            waitingVertices.Push(item2);
            waitingVertices.Push(item1);

            return false;
        }
    }
}
