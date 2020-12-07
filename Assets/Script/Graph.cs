using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Graph
{
    public static List<DragAndDropItem> node = new List<DragAndDropItem>();
    public static DragAndDropItem root;

    private static DragAndDropItem getNodeByID(int id)
    {
        foreach(var item in node)
        {
            if (item.id == id) return item;
        }

        return null;
    }

    private static bool[] createUnvisitedArray(int size)
    {
        bool[] returnArray = new bool[size];
        for(int i=0; i<size; i++)
        {
            returnArray[i] = false;
        }

        return returnArray;
    }

    public static bool IsCyclic()
    {
        bool[] isVisited = createUnvisitedArray(DragAndDropItem.count + 1);

        DragAndDropItem currNode = root;

        while(currNode != null)
        {
            if(isVisited[currNode.id])
            {
                return true;
            }
            isVisited[currNode.id] = true;
            currNode = currNode.children;
        }

        return false;
    }
}
