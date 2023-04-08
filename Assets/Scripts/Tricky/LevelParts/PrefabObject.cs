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



    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(PrefabName);

        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var TempPrefab = PrefabObjects[i];
            for (int a = 0; a < TempPrefab.MeshData.Count; a++)
            {
                GameObject ChildMesh = new GameObject(i + ", " + a);
                ChildMesh.transform.parent = MainObject.transform;
                ChildMesh.transform.localPosition = TempPrefab.Position;
                ChildMesh.transform.localScale = TempPrefab.Scale;
                ChildMesh.transform.localRotation = TempPrefab.Rotation;
                ChildMesh.transform.localEulerAngles += new Vector3(180, 0, 0);

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

            //OBJECT ANIMATION PUT HERE


            NewPrefabObject.MeshData = new List<MeshHeader>();
            for (int a = 0; a < prefabJson.PrefabObjects[i].MeshData.Count; a++)
            {
                var TempMesh = prefabJson.PrefabObjects[i].MeshData[a];
                var TempNewMeshData = new MeshHeader();
                TempNewMeshData.MeshPath = TempMesh.MeshPath;
                TempNewMeshData.MeshID = TempMesh.MeshID;
                TempNewMeshData.mesh = ObjImporter.ObjLoad(TrickyMapInterface.Instance.LoadPath + "\\Models\\" + TempMesh.MeshPath);
                TempNewMeshData.MaterialID = TempMesh.MaterialID;

                Debug.Log(TempMesh.MaterialID);

                TempNewMeshData.material = GenerateMaterial(TempMesh.MaterialID);

                NewPrefabObject.MeshData.Add(TempNewMeshData);
            }

            PrefabObjects.Add(NewPrefabObject);
        }
    }

    public void LoadModelsAndMesh()
    {
        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var NewPrefabObject = PrefabObjects[i];
            for (int a = 0; a < NewPrefabObject.MeshData.Count; a++)
            {
                var TempMesh = NewPrefabObject.MeshData[a];
                TempMesh.mesh = ObjImporter.ObjLoad(TrickyMapInterface.Instance.LoadPath + "\\Models\\" + TempMesh.MeshPath);
                TempMesh.material = GenerateMaterial(TempMesh.MaterialID);
                NewPrefabObject.MeshData[a] = TempMesh;
            }
            PrefabObjects[i] = NewPrefabObject;
        }
    }


    public static Material GenerateMaterial(int MaterialID)
    {
        Material material = new Material(Shader.Find("ModelShader"));
        int TextureID = TrickyMapInterface.Instance.materialJson.MaterialsJsons[MaterialID].TextureID;
        material.SetTexture("_MainTexture", GetTexture(TextureID));
        material.SetFloat("_OutlineWidth", 0);
        material.SetFloat("_OpacityMaskOutline", 0f);
        material.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        material.SetFloat("_NoLightMode", 1);
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
