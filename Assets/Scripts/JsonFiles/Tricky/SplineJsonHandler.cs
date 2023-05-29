using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SSXMultiTool.JsonFiles.Tricky
{
    [Serializable]
    public class SplineJsonHandler
    {
        public List<SplineJson> Splines = new List<SplineJson>();

        public void CreateJson(string path)
        {
            var serializer = JsonUtility.ToJson(this);
            File.WriteAllText(path, serializer);
        }

        public static SplineJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonUtility.FromJson<SplineJsonHandler>(stream);
                return container;
            }
            else
            {
                return new SplineJsonHandler();
            }
        }

        [Serializable]
        public struct SplineJson
        {
            public string SplineName;

            public List<SegmentJson> Segments;

        }
        [Serializable]
        public struct SegmentJson
        {
            public float[] Point4;
            public float[] Point3;
            public float[] Point2;
            public float[] Point1;
            public float U0;
            public float U1;
            public float U2;
            public float U3;
        }
    }
}
