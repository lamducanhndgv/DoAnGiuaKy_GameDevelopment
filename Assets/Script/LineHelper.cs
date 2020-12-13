using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineHelper
{
    public static DragAndDropItem first;
    public static DragAndDropItem second;

    public static void Connect()
    {
        first.ancestor = second;
        second.children = first;

        Debug.Log("connected: " + second.id + " -> " + first.id);

        Cancel();
    }
    public static void Cancel()
    {
        first = null;
        second = null;
    }

    public static bool IsOk()
    {
     
        if (first.children == second || second.children == first)
            return false;
        if (first.ancestor != null || second.children != null) 
            return false;

        return true;
    }
}
