using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptWriter : MonoBehaviour
{
    public static ScriptWriter Instance { get; private set; }

    private  ExtensionFilter[] extensions = new []{
        new ExtensionFilter("Python script", "py"),
        new ExtensionFilter("All Files", "*" ),
    };

    private void Awake()
    {
        Instance = this;
    }

    public void WriteToFile(List<DragAndDropItem> layers)
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", ".", extensions, false);

        string filepath;

        if (paths.Length > 0)
            filepath = paths[0];
        else
            filepath = "./model.py";
        
        StreamWriter writer = new StreamWriter(filepath, false);
       
        foreach (DragAndDropItem layer in layers)
        {
            writer.WriteLine(layer.id);
        }

        writer.Close();
    }
}
