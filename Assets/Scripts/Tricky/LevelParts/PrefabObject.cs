using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
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
                if (i <= 1)
                {
                    //ChildMesh.transform.localEulerAngles += new Vector3(180, 0, 0);
                }

                if(ChildMesh.transform.localScale == new Vector3(0,0,0))
                {
                    ChildMesh.transform.localScale = new Vector3(1, 1, 1);
                }

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

            //NewPrefabObject.Position = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Position);
            //NewPrefabObject.Rotation = JsonUtil.ArrayToQuaternion(prefabJson.PrefabObjects[i].Rotation);
            //NewPrefabObject.Scale = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Scale);

            NewPrefabObject.Scale = Vector3.one;

            //OBJECT ANIMATION PUT HERE


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
        int TextureID = 0;
        if (SkyboxLoad)
        {
            TextureID = SkyboxManager.Instance.materialJson.MaterialsJsons[MaterialID].TextureID;
        }
        else
        {
            TextureID = TrickyMapInterface.Instance.materialJson.MaterialsJsons[MaterialID].TextureID;
        }
        material.SetTexture("_MainTexture", GetTexture(TextureID, SkyboxLoad));
        material.SetFloat("_OutlineWidth", 0);
        material.SetFloat("_OpacityMaskOutline", 0f);
        material.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        material.SetFloat("_NoLightMode", 1);
        return material;
    }

    public static Texture2D GetTexture(int TextureID, bool SkyboxLoad)
    {
        Texture2D texture = null;
        try
        {
            if(SkyboxLoad)
            {
                texture = SkyboxManager.Instance.textures[TextureID];
            }
            else
            {
                texture = TrickyMapInterface.Instance.textures[TextureID];
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
