using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SSXMultiTool.JsonFiles.Tricky
{
    public class TextureFlipbookJsonHandler
    {
        public List<FlipbookJson> FlipbookJsons = new List<FlipbookJson>();

        public void CreateJson(string path)
        {
            var serializer = JsonUtility.ToJson(this);
            File.WriteAllText(path, serializer);
        }

        public static TextureFlipbookJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonUtility.FromJson<TextureFlipbookJsonHandler>(stream);
                return container;
            }
            else
            {
                return new TextureFlipbookJsonHandler();
            }
        }

        [Serializable]
        public struct FlipbookJson
        {
            public int ImageCount;
            public List<int> Images;
        }
    }
}
