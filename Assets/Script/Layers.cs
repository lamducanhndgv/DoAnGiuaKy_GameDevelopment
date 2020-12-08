using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    static class LayersConstants
    {
        public static int INPUT = 0;
        public static int FULLY_CONNECTED = 1;
        public static int CONVOLUTIONAL1D = 2;
        public static int CONVOLUTIONAL2D = 3;
        public static int FLATTEN = 4;
        public static int DROP_OUT = 5;
        public static int MAXPOOL2D = 6;
        public static int RELU = 7;

        public static Dictionary<int, string> LAYER_NAMES = new Dictionary<int, string>()
        {
            { INPUT, "Input" },
            { FULLY_CONNECTED, "Fully Connected" },
            { CONVOLUTIONAL1D, "Convolutional 1D" },
            { CONVOLUTIONAL2D, "Convolutional 2D" },
            { FLATTEN, "Flatten" },
            { DROP_OUT, "Drop-out" },
            { MAXPOOL2D, "Maxpool 2D" },
            { RELU, "Relu" }
        };

        public static Dictionary<int, Color32> LAYER_COLORS = new Dictionary<int, Color32>()
        {
            { INPUT, new Color32(57, 181, 74, 255) },
            { FULLY_CONNECTED, new Color32(0, 169, 157, 255) },
            { CONVOLUTIONAL1D, new Color32(237, 199, 10, 255) },
            { CONVOLUTIONAL2D, new Color32(0, 113, 188, 255) },
            { FLATTEN, new Color32(158, 0, 93, 255) },
            { DROP_OUT, new Color32(27, 20, 100, 255) },
            { MAXPOOL2D, new Color32(184, 184, 184, 255) },
            { RELU, new Color32(251, 176, 59, 255) }
        };
    }

    public class Attribute
    { 
        public string name { get; }
        public string default_value { get; }
        public string data_type { get; }
        public string value { get; set; }
        public Attribute(string name, string default_value, string data_type)
        {
            this.name = name;
            this.default_value = default_value;
            this.data_type = data_type;
            this.value = null;
        }

        override public string ToString()
        {
            return $"{name}={(this.value == null || this.value == "" ? this.default_value : this.value)}";
        }
    }

    public class Layer
    {
        public string LayerName;

        public List<Attribute> attributes = new List<Attribute>();

        override public string ToString()
        {
            string AttributeString = "";

            int n = this.attributes.Count;

            for (int i = 0; i < n; i++)
            {
                AttributeString += this.attributes[i].ToString();
                if (i < n - 1)
                    AttributeString += ',';
            }
            return  $"{LayerName}({AttributeString})";
        }

        public void SetValueAttributes(string[] values)
        {
            int n = this.attributes.Count;
            for (int i = 0; i < n; i++)
            {
                this.attributes[i].value = values[i];
            }
        }
    }
    // Class inherited
    public class Conv1d : Layer
    {
        public Conv1d()
        {
            this.LayerName = "nn.Conv1d";

            this.attributes.Add(
                new Attribute(
                    "in_channels",
                    null,
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "out_channels",
                    null,
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "kernel_size",
                    null,
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "stride",
                    "1",
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "padding",
                    "0",
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "bias",
                    "True",
                    "bool"
            ));
        }
    }
    public class Conv2d : Layer
    {
        public Conv2d()
        {
            this.LayerName = "nn.Conv2d";

            this.attributes.Add(
                new Attribute(
                    "in_channels",
                    null,
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "out_channels",
                    null,
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "kernel_size",
                    null,
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "stride",
                    "1",
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "padding",
                    "0",
                    "int"
            ));
            this.attributes.Add(
                new Attribute(
                    "bias",
                    "True",
                    "bool"
            ));
        }
    }
    public class Dropout : Layer
    {
        public Dropout()
        {
            this.LayerName = "nn.Dropout";

            this.attributes.Add(
                new Attribute(
                    "p",
                    "0.5",
                    "float"
            ));
        }
    }
    public class MaxPool2d : Layer
    {
        public MaxPool2d()
        {
            this.LayerName = "nn.MaxPool2d";

            this.attributes.Add(
                new Attribute(
                    "kernel_size",
                    null,
                    "float"
            ));
            this.attributes.Add(
             new Attribute(
                 "stride",
                 "1",
                 "int"
         ));
            this.attributes.Add(
                new Attribute(
                    "padding",
                    "0",
                    "int"
            ));
        }
    }
    public class ReLU : Layer
    {
        public ReLU()
        {
            this.LayerName = "nn.ReLU";

            this.attributes.Add(
                new Attribute(
                    "inplace",
                    "False",
                    "bool"
            ));
        }
    }
    public class Linear : Layer
    {
        public Linear()
        {
            this.LayerName = "nn.Linear";

            this.attributes.Add(
                new Attribute(
                    "in_features",
                    null,
                    "int"
            ));
            this.attributes.Add(
              new Attribute(
                  "out_features",
                  null,
                  "int"
          ));
            this.attributes.Add(
             new Attribute(
                 "bias",
                 "True",
                 "bool"
         ));
        }
    }
    public class Flatten: Layer
    {
        public Flatten()
        {
            this.LayerName = "nn.Flatten";
            this.attributes.Add(
                    new Attribute(
                        "start_dim",
                        "1",
                        "int"
            ));
            this.attributes.Add(
                    new Attribute(
                        "end_dim",
                        "-1",
                        "int"
            ));
        }
    }

    public class LayerFactory
    {
        public static Layer BuildLayer(int layerid)
        {
            switch (layerid)
            {
                case 1:
                    return new Linear();
                case 2:
                    return new Conv1d();
                case 3:
                    return new Conv2d();
                case 4:
                    return new Flatten();
                case 5:
                    return new Dropout();
                case 6:
                    return new MaxPool2d();
                case 7:
                    return new ReLU();
            }
            return null;
        }
    }
}