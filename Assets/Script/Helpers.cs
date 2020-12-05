using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static Stack<DragAndDropItem> waitingVertices = new Stack<DragAndDropItem>();
    public static void Connect() 
    {
        DragAndDropItem parent = waitingVertices.Pop();
        DragAndDropItem child = waitingVertices.Pop();

        Debug.Log("connected: " + parent.id + " -> " + child.id);
        parent.children.Add(child);
    }
}
