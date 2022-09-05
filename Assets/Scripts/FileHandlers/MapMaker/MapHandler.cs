using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX_Modder.FileHandlers.MapEditor
{
    public class MapHandler
    {
        public List<LinkerItem> Models;
        public List<LinkerItem> particelModels;
        public List<LinkerItem> Patchs;
        public List<LinkerItem> InternalInstances;
        public List<LinkerItem> PlayerStarts; //Unused
        public List<LinkerItem> ParticleInstances;
        public List<LinkerItem> Splines;
        public List<LinkerItem> Lights;
        public List<LinkerItem> Materials;
        public List<LinkerItem> ContextBlocks;
        public List<LinkerItem> Cameras;
        public List<LinkerItem> Textures;
        public List<LinkerItem> Lightmaps;

        int LinePos = 23;

        public void Load(string path)
        {
            string[] Lines = File.ReadAllLines(path);

            LinePos = 23;

            Models = ReadLinkerItems(Lines);

            LinePos += 9;
            particelModels = ReadLinkerItems(Lines);

            LinePos += 9;
            Patchs = ReadLinkerItems(Lines);

            LinePos += 9;
            InternalInstances = ReadLinkerItems(Lines);

            LinePos += 9;
            PlayerStarts = ReadLinkerItems(Lines);

            LinePos += 9;
            ParticleInstances = ReadLinkerItems(Lines);

            LinePos += 9;
            Splines = ReadLinkerItems(Lines);

            LinePos += 9;
            Lights = ReadLinkerItems(Lines);

            LinePos += 9;
            Materials = ReadLinkerItems(Lines);

            LinePos += 9;
            ContextBlocks = ReadLinkerItems(Lines);

            LinePos += 9;
            Cameras = ReadLinkerItems(Lines);

            LinePos += 8;
            Textures = ReadLinkerItems(Lines);

            LinePos += 8;
            Lightmaps = ReadLinkerItems(Lines);
        }

        List<LinkerItem> ReadLinkerItems(string[] Lines)
        {
            var TempList = new List<LinkerItem>();
            while (true)
            {
                if (Lines[LinePos] == "")
                {
                    break;
                }
                var LinkerItem = new LinkerItem();
                LinkerItem.Name = Lines[LinePos].Substring(0, 82).TrimEnd(' ');
                LinkerItem.UID = Int32.Parse(Lines[LinePos].Substring(82, 10).Replace(" ", "").TrimEnd(' '));
                LinkerItem.Ref = Int32.Parse(Lines[LinePos].Substring(92, 10).Replace(" ", "").TrimEnd(' '));
                LinkerItem.Hashvalue = Lines[LinePos].Substring(102, 10).TrimEnd(' ');
                TempList.Add(LinkerItem);
                LinePos++;
            }
            return TempList;
    }
    }

    public struct LinkerItem
    {
        public string Name;
        public int UID;
        public int Ref;
        public string Hashvalue;
    }
}
