using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

[System.Serializable]
public class PrefabObject
{
    public string PrefabName;
    public int Unknown3;
    public float AnimTime;
    public List<ObjectHeader> PrefabObjects;

    public GameObject GeneratePrefab(bool SkyboxLoad = false)
    {
        GameObject MainObject = new GameObject(PrefabName);
        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var TempPrefab = PrefabObjects[i];
            for (int a = 0; a < TempPrefab.MeshData.Count; a++)
            {
                GameObject ChildMesh = new GameObject(i + ", " + a);

                if(SkyboxLoad)
                {
                    ChildMesh.layer = 8;
                }

                ChildMesh.transform.parent = MainObject.transform;
                ChildMesh.transform.localPosition = TempPrefab.Position;
                ChildMesh.transform.localScale = TempPrefab.Scale;
                ChildMesh.transform.localRotation = TempPrefab.Rotation;

                var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
                var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();

                TempMeshFilter.mesh = TempPrefab.MeshData[a].mesh;
                TempRenderer.material = TempPrefab.MeshData[a].material;
            }
        }

        return MainObject;
    }

    public void LoadPrefab(PrefabJsonHandler.PrefabJson prefabJson, bool SkyboxLoad = false)
    {
        PrefabName = prefabJson.PrefabName;
        Unknown3 = prefabJson.Unknown3;
        AnimTime = prefabJson.AnimTime;
        PrefabObjects = new List<ObjectHeader>();
        for (int i = 0; i < prefabJson.PrefabObjects.Count; i++)
        {
            var NewPrefabObject = new ObjectHeader();
            NewPrefabObject.ParentID = prefabJson.PrefabObjects[i].ParentID;
            NewPrefabObject.Flags = prefabJson.PrefabObjects[i].Flags;
            NewPrefabObject.IncludeMatrix = prefabJson.PrefabObjects[i].IncludeMatrix;
            if (prefabJson.PrefabObjects[i].IncludeMatrix)
            {
                NewPrefabObject.Position = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Position);
                NewPrefabObject.Rotation = JsonUtil.ArrayToQuaternion(prefabJson.PrefabObjects[i].Rotation);
                NewPrefabObject.Scale = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Scale);
            }
            else
            {
                NewPrefabObject.Scale = Vector3.one;
            }

            //OBJECT ANIMATION PUT HERE
            NewPrefabObject.IncludeAnimation = prefabJson.PrefabObjects[i].IncludeAnimation;
            NewPrefabObject.Animation = new ObjectAnimation();
            if(NewPrefabObject.IncludeAnimation)
            {
                NewPrefabObject.Animation.U1 = prefabJson.PrefabObjects[i].Animation.U1;
                NewPrefabObject.Animation.U2 = prefabJson.PrefabObjects[i].Animation.U2;
                NewPrefabObject.Animation.U3 = prefabJson.PrefabObjects[i].Animation.U3;
                NewPrefabObject.Animation.U4 = prefabJson.PrefabObjects[i].Animation.U4;
                NewPrefabObject.Animation.U5 = prefabJson.PrefabObjects[i].Animation.U5;
                NewPrefabObject.Animation.U6 = prefabJson.PrefabObjects[i].Animation.U6;
                NewPrefabObject.Animation.AnimationAction = prefabJson.PrefabObjects[i].Animation.AnimationAction;

                NewPrefabObject.Animation.AnimationEntries = new List<AnimationEntry>();

                for (int a = 0; a < prefabJson.PrefabObjects[i].Animation.AnimationEntries.Count; a++)
                {
                    var TempEntry = new AnimationEntry();
                    TempEntry.AnimationMaths = new List<AnimationMath>();
                    
                    for (int b = 0; b < prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths.Count; b++)
                    {
                        var TempMaths = new AnimationMath();

                        TempMaths.Value1 = prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths[b].Value1;
                        TempMaths.Value2 = prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths[b].Value2;
                        TempMaths.Value3 = prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths[b].Value3;
                        TempMaths.Value4 = prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths[b].Value4;
                        TempMaths.Value5 = prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths[b].Value5;
                        TempMaths.Value6 = prefabJson.PrefabObjects[i].Animation.AnimationEntries[a].AnimationMaths[b].Value6;

                        TempEntry.AnimationMaths.Add(TempMaths);
                    }
                    NewPrefabObject.Animation.AnimationEntries.Add(TempEntry);
                }
            }

            NewPrefabObject.MeshData = new List<MeshHeader>();
            for (int a = 0; a < prefabJson.PrefabObjects[i].MeshData.Count; a++)
            {
                var TempMesh = prefabJson.PrefabObjects[i].MeshData[a];
                var TempNewMeshData = new MeshHeader();
                TempNewMeshData.MeshPath = TempMesh.MeshPath;
                TempNewMeshData.MeshID = TempMesh.MeshID;
                if(SkyboxLoad)
                {
                    TempNewMeshData.mesh = ObjImporter.ObjLoad(TrickyMapInterface.Instance.LoadPath + "\\Skybox\\Models\\" + TempMesh.MeshPath);
                }
                else
                {
                    TempNewMeshData.mesh = ObjImporter.ObjLoad(TrickyMapInterface.Instance.LoadPath + "\\Models\\" + TempMesh.MeshPath);
                }
                TempNewMeshData.MaterialID = TempMesh.MaterialID;

                TempNewMeshData.material = GenerateMaterial(TempMesh.MaterialID, SkyboxLoad);

                NewPrefabObject.MeshData.Add(TempNewMeshData);
            }

            PrefabObjects.Add(NewPrefabObject);
        }
    }

    public void LoadModelsAndMesh(bool SkyboxLoad = false)
    {
        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var NewPrefabObject = PrefabObjects[i];
            for (int a = 0; a < NewPrefabObject.MeshData.Count; a++)
            {
                var TempMesh = NewPrefabObject.MeshData[a];
                if (SkyboxLoad)
                {
                    TempMesh.mesh = ObjImporter.ObjLoad(TrickyMapInterface.Instance.LoadPath + "\\Skybox\\Models\\" + TempMesh.MeshPath);
                }
                else
                {
                    TempMesh.mesh = ObjImporter.ObjLoad(TrickyMapInterface.Instance.LoadPath + "\\Models\\" + TempMesh.MeshPath);
                }
                TempMesh.material = GenerateMaterial(TempMesh.MaterialID, SkyboxLoad);
                NewPrefabObject.MeshData[a] = TempMesh;
            }
            PrefabObjects[i] = NewPrefabObject;
        }
    }


    public static Material GenerateMaterial(int MaterialID, bool SkyboxLoad = false)
    {
        Material material = new Material(Shader.Find("ModelShader"));
        string TextureID = "";
        if (MaterialID != -1)
        {
            if (SkyboxLoad)
            {
                TextureID = SkyboxManager.Instance.materialJson.Materials[MaterialID].TexturePath;
            }
            else
            {
                TextureID = TrickyMapInterface.Instance.materialJson.Materials[MaterialID].TexturePath;
            }
        }
        material.SetTexture("_MainTexture", GetTexture(TextureID, SkyboxLoad));
        material.SetFloat("_OutlineWidth", 0);
        material.SetFloat("_OpacityMaskOutline", 0f);
        material.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        material.SetFloat("_NoLightMode", 1);
        return material;
    }

    public static Texture2D GetTexture(string TextureID, bool SkyboxLoad)
    {
        Texture2D texture = null;
        try
        {
            if(SkyboxLoad)
            {
                for (int i = 0; i < SkyboxManager.Instance.textures.Count; i++)
                {
                    if (SkyboxManager.Instance.textures[i].name.ToLower() == TextureID.ToLower())
                    {
                        texture = SkyboxManager.Instance.textures[i];
                        return texture;
                    }
                }
                texture = TrickyMapInterface.Instance.ErrorTexture;
            }
            else
            {
                for (int i = 0; i < TrickyMapInterface.Instance.textures.Count; i++)
                {
                    if (TrickyMapInterface.Instance.textures[i].name.ToLower() == TextureID.ToLower())
                    {
                        texture = TrickyMapInterface.Instance.textures[i];
                        return texture;
                    }
                }
                texture = TrickyMapInterface.Instance.ErrorTexture;
            }

        }
        catch
        {
            texture = TrickyMapInterface.Instance.ErrorTexture;
        }
        return texture;
    }

    [Serializable]
    public struct ObjectHeader
    {
        public int ParentID;
        public int Flags;

        public ObjectAnimation Animation;
        public List<MeshHeader> MeshData;

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public bool IncludeAnimation;
        public bool IncludeMatrix;
    }
    [Serializable]
    public struct MeshHeader
    {
        public string MeshPath;
        public int MeshID;
        public Mesh mesh;
        public int MaterialID;
        public Material material;
    }
    [Serializable]
    public struct ObjectAnimation
    {
        public float U1;
        public float U2;
        public float U3;
        public float U4;
        public float U5;
        public float U6;

        public int AnimationAction;
        public List<AnimationEntry> AnimationEntries;
    }
    [Serializable]
    public struct AnimationEntry
    {
        public List<AnimationMath> AnimationMaths;
    }
    [Serializable]
    public struct AnimationMath
    {
        public float Value1;
        public float Value2;
        public float Value3;
        public float Value4;
        public float Value5;
        public float Value6;
    }
}
