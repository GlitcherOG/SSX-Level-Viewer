﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace SSXMultiTool.JsonFiles.Tricky
{
    [Serializable]
    public class PatchesJsonHandler
    {
        public List<PatchJson> Patches = new List<PatchJson>();

        public void CreateJson(string path)
        {
            var serializer = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.None);
            File.WriteAllText(path, serializer);
        }

        public static PatchesJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<PatchesJsonHandler>(stream);
                return container;
            }
            else
            {
                return new PatchesJsonHandler();
            }
        }


        [Serializable]
        public struct PatchJson
        {
            public string PatchName;

            public float[] LightMapPoint;
            public float[,] UVPoints;
            public float[,] Points;

            public int PatchStyle;
            public bool TrickOnlyPatch;
            public string TexturePath;
            public int LightmapID;
        }
    }
}
