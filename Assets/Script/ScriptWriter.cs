using SFB;
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

    private const string TAB = "    ";
    private const char ENDL = '\n';

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

        writer.WriteLine(this.header());
        writer.WriteLine(this.init(layers));
        writer.WriteLine(this.forward(layers));

        writer.Close();
    }

    private string header()
    {
        return "import torch.nn as nn\n" + "class Net(nn.Module):";
    }

    private string init(List<DragAndDropItem> layers)
    {
        string result = line("def __init__(self):", 1);
        result += line("super(Net, self).__init__()", 2);

        foreach (DragAndDropItem layer in layers)
        {
            if (layer.id == 1)
                continue;
            string content = definition(layer.GetLayerName().ToLower() + '_' + layer.id) + '=' + layer.GetStringLayer();
            result += line(content, 2);
        }
        return result;
    }

    private string forward(List<DragAndDropItem> layers)
    {
        string result = line("def forward(self, x):", 1);

        foreach (DragAndDropItem layer in layers)
        {
            if (layer.id == 1)
                continue;
            string content = "x = " + definition(layer.GetLayerName().ToLower() + '_' + layer.id) + "(x)";
            result += line(content, 2);
        }

        result += line("return x", 2);
        return result;
    }

    private string line(string content, int tabs = 0, bool endl = true)
    {
        string result = "";
        while ( tabs-- > 0)
            result += TAB;
        return result + content + (endl ? ENDL : ' ');
    }

    private string definition(string name)
    {
        return $"self.{name}";
    }
}
