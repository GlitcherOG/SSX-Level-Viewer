﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SSXMultiTool.JsonFiles.Tricky
{
    [Serializable]
    public class ParticleInstanceJsonHandler
    {
        public List<ParticleJson> Particles = new List<ParticleJson>();

        public void CreateJson(string path)
        {
            var serializer = JsonUtility.ToJson(this);
            File.WriteAllText(path, serializer);
        }

        public static ParticleInstanceJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonUtility.FromJson<ParticleInstanceJsonHandler>(stream);
                return container;
            }
            else
            {
                return new ParticleInstanceJsonHandler();
            }
        }

        [Serializable]
        public struct ParticleJson
        {
            public string ParticleName;

            public float[] Location;
            public float[] Rotation;
            public float[] Scale;

            public int UnknownInt1;
            public Vector3 LowestXYZ;
            public Vector3 HighestXYZ;
            public int UnknownInt8;
            public int UnknownInt9;
            public int UnknownInt10;
            public int UnknownInt11;
            public int UnknownInt12;
        }
    }
}
