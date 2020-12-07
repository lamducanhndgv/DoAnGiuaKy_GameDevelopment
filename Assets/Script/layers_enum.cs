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
        public static int RECURRENT = 4;
        public static int DROP_OUT = 5;
        public static int MAXPOOL2D = 6;
        public static int RELU = 7;

        public static Dictionary<int, string> LAYER_NAMES = new Dictionary<int, string>()
        {
            { INPUT, "Input" },
            { FULLY_CONNECTED, "Fully Connected" },
            { CONVOLUTIONAL1D, "Convolutional 1D" },
            { CONVOLUTIONAL2D, "Convolutional 2D" },
            { RECURRENT, "Recurrent" },
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
            { RECURRENT, new Color32(158, 0, 93, 255) },
            { DROP_OUT, new Color32(27, 20, 100, 255) },
            { MAXPOOL2D, new Color32(184, 184, 184, 255) },
            { RELU, new Color32(251, 176, 59, 255) }
        };
    }
}