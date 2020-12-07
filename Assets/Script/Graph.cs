using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public static List<DragAndDropItem> node = new List<DragAndDropItem>();
    public static DragAndDropItem root;

    public Graph(DragAndDropItem r)
    {
        root = r;
    }

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

    private static bool isCyclicUtil(int id, bool[] isVisited, int parent)
    {
        isVisited[id] = true;
        DragAndDropItem curNode = getNodeByID(id);
        int childID = curNode.children.id;

        if (!isVisited[childID])
        {
            if (isCyclicUtil(childID, isVisited, id))
            {
                return true;
            }
        }
        else if (childID != parent) 
        {
            return true;
        }

        return false;
    }

    public static bool IsCyclic()
    {
        bool[] isVisited = createUnvisitedArray(DragAndDropItem.count + 1);

        foreach(var item in node)
        {
            isVisited[item.id] = false;
        }

        foreach (var item in node)
        {
            if(!isVisited[item.id])
            {
                if(isCyclicUtil(item.id, isVisited, -1))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void DFS(int id, bool[] isVisited)
    {
        isVisited[id] = true;
        DragAndDropItem curNode = getNodeByID(id);
        int childID = curNode.children.id;

        if(!isVisited[childID])
        {
            DFS(childID, isVisited);
        }
    }

    public static bool IsConnected()
    {
        bool[] isVisited = createUnvisitedArray(DragAndDropItem.count + 1);

        DFS(root.id, isVisited);

        int count = 0;
        foreach(var item in node)
        {
            if(isVisited[item.id])
            {
                count++;
            }
        }

        if (count == node.Count)
        {
            return true;
        }

        return false;
    }
}
