using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Graph
{
    public static DragAndDropItem root;

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

    public static List<DragAndDropItem> CollectNode()
    {
        List<DragAndDropItem> items = new List<DragAndDropItem>();

        DragAndDropItem currNode = root;

        while (currNode != null)
        {
            items.Add(currNode);
            currNode = currNode.children;
        }

        return items;
    }
}
