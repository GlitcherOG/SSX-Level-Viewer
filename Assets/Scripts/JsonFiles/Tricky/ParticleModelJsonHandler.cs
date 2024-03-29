﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SSXMultiTool.JsonFiles.Tricky
{
    public class ParticleModelJsonHandler
    {
        public List<ParticleModelJson> ParticleModels = new List<ParticleModelJson>();

        public void CreateJson(string path)
        {
            var serializer = JsonUtility.ToJson(this);
            File.WriteAllText(path, serializer);
        }

        public static ParticleModelJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonUtility.FromJson<ParticleModelJsonHandler>(stream);
                return container;
            }
            else
            {
                return new ParticleModelJsonHandler();
            }
        }

        [Serializable]
        public struct ParticleModelJson
        {
            public string ParticleModelName;
            public List<ParticleObjectHeader> ParticleObjectHeaders;
        }
        [Serializable]
        public struct ParticleObjectHeader
        {
            public ParticleObject ParticleObject;
        }
        [Serializable]
        public struct ParticleObject
        {
            public Vector3 LowestXYZ;
            public Vector3 HighestXYZ;
            public int U1;

            public List<AnimationFrames> AnimationFrames;
        }
        [Serializable]
        public struct AnimationFrames
        {
            public Vector3 Position;
            public Vector3 Rotation;
            public float Unknown;
        }
    }
}
