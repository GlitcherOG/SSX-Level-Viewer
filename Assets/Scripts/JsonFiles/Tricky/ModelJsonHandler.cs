﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SSXMultiTool.JsonFiles.Tricky
{
    public class ModelJsonHandler
    {
        public List<ModelJson> ModelJsons = new List<ModelJson>();

        public void CreateJson(string path)
        {
            var serializer = JsonUtility.ToJson(this);
            File.WriteAllText(path, serializer);
        }

        public static ModelJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonUtility.FromJson<ModelJsonHandler>(stream);
                return container;
            }
            else
            {
                return new ModelJsonHandler();
            }
        }

        [Serializable]
        public struct ModelJson
        {
            public string ModelName;

            public int TotalLength;
            public int Unknown0;
            public int Unknown1;
            public int Unknown2;
            public int Unknown3; //ID
            public float Unknown4;
            public float ScaleX; //Scale X
            public float ScaleZ; //Scale Z
            public float ScaleY; //Scale Y
            public int Unknown8; //Model Data Count?
            public int Unknown9;
            public int TriStripCount; //Tristrip Count
            public int VertexCount; //Vertex Count
            public int Unknown12;
            public int Unknown13;
            public int Unknown14;
            public int Unknown15;
            public int Unknown16;
            public int Unknown17;
            public int Unknown18;

            public int UnknownLength;

            public Vector3 LowestXYZ;
            public Vector3 HighestXYZ;

            public byte[] bytes;
        }
    }
}