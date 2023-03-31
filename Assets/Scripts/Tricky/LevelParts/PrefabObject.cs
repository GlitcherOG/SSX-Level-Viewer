using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObject
{
    public string PrefabName;
    public int Unknown3;
    public float AnimTime;
    public List<ObjectHeader> PrefabObjects;



    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(PrefabName);

        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var TempPrefab = PrefabObjects[i];
            for (int a = 0; a < TempPrefab.MeshData.Count; a++)
            {
                GameObject ChildMesh = new GameObject();
                var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
                var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();
                TempMeshFilter.mesh = TempPrefab.MeshData[a].mesh;
                TempRenderer.material = TempPrefab.MeshData[a].material;
                ChildMesh.transform.parent = MainObject.transform;
            }
        }

        return MainObject;
    }

    public void LoadPrefab(PrefabJsonHandler.PrefabJson prefabJson)
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

            NewPrefabObject.Position = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Position);
            NewPrefabObject.Rotation = JsonUtil.ArrayToQuaternion(prefabJson.PrefabObjects[i].Rotation);
            NewPrefabObject.Scale = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Scale);

            NewPrefabObject.MeshData = new List<MeshHeader>();



        }
    }


    public static Material GenerateMaterial(int MaterialID)
    {
        Material material = new Material(Shader.Find("ModelShader"));
        int TextureID = TrickyMapInterface.Instance.materialJson.MaterialsJsons[MaterialID].TextureID;
        material.SetTexture("_MainTexture", GetTexture(TextureID));
        return material;
    }

    public static Texture2D GetTexture(int TextureID)
    {
        Texture2D texture = null;
        try
        {
            texture = TrickyMapInterface.Instance.textures[TextureID];

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
