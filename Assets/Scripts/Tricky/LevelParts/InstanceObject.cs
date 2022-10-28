using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

public class InstanceObject : MonoBehaviour
{
    public string InstanceName;

    public Vector3 rotation;
    public Vector3 scale;
    public Vector3 InstancePosition;

    public Vector4 Unknown5; //Something to do with lighting
    public Vector4 Unknown6; //Lighting Continued?
    public Vector4 Unknown7; 
    public Vector4 Unknown8;
    public Vector4 Unknown9; //Some Lighting Thing
    public Vector4 Unknown10;
    public Vector4 Unknown11;
    public Vector4 RGBA;

    public int ModelID;
    public int PrevInstance; //Next Connected Model 
    public int NextInstance; //Prev Connected Model

    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;

    public int UnknownInt26;
    public int UnknownInt27;
    public int UnknownInt28;
    public int ModelID2;
    public int UnknownInt30;
    public int UnknownInt31;
    public int UnknownInt32;

    public Vector3 Oldrotation;
    public Vector3 Oldscale;
    public Vector3 Oldposition;
    public List<GameObject> meshes;
    public List<MeshCollider> colliders;

    public void LoadInstance(InstanceJsonHandler.InstanceJson instance)
    {
        InstanceName = instance.InstanceName;

        rotation = JsonUtil.ArrayToQuaternion(instance.Rotation).eulerAngles;
        scale = JsonUtil.ArrayToVector3(instance.Scale);
        InstancePosition = JsonUtil.ArrayToVector3(instance.Location);

        Unknown5 = JsonUtil.ArrayToVector4(instance.Unknown5);
        Unknown6 = JsonUtil.ArrayToVector4(instance.Unknown6);
        Unknown7 = JsonUtil.ArrayToVector4(instance.Unknown7);
        Unknown8 = JsonUtil.ArrayToVector4(instance.Unknown8);
        Unknown9 = JsonUtil.ArrayToVector4(instance.Unknown9);
        Unknown10 = JsonUtil.ArrayToVector4(instance.Unknown10);
        Unknown11 = JsonUtil.ArrayToVector4(instance.Unknown11);
        Unknown11 = JsonUtil.ArrayToVector4(instance.Unknown11);
        RGBA = JsonUtil.ArrayToVector4(instance.RGBA);


        ModelID = instance.ModelID;
        PrevInstance = instance.PrevInstance;
        NextInstance = instance.NextInstance;

        LowestXYZ = JsonUtil.ArrayToVector3(instance.LowestXYZ);
        HighestXYZ = JsonUtil.ArrayToVector3(instance.HighestXYZ);

        UnknownInt26 = instance.UnknownInt26;
        UnknownInt27 = instance.UnknownInt27;
        UnknownInt28 = instance.UnknownInt28;
        ModelID2 = instance.ModelID2;
        UnknownInt30 = instance.UnknownInt30;
        UnknownInt31 = instance.UnknownInt31;
        UnknownInt32 = instance.UnknownInt32;

        colliders = new List<MeshCollider>();
        meshes = new List<GameObject>();
        var modelObject = TrickyMapInterface.Instance.modelObjects[instance.ModelID];
        for (int i = 0; i < modelObject.meshes.Count; i++)
        {
            GameObject newGameObject = new GameObject();
            newGameObject.AddComponent<MeshFilter>();
            newGameObject.GetComponent<MeshFilter>().mesh = modelObject.meshes[i];
            var tempCollider = gameObject.AddComponent<MeshCollider>();
            tempCollider.sharedMesh = newGameObject.GetComponent<MeshFilter>().mesh;
            colliders.Add(tempCollider);
            newGameObject.AddComponent<MeshRenderer>();
            try
            {
                newGameObject.GetComponent<MeshRenderer>().material = ModelObject.GenerateMaterial(ModelID, i);
            }
            catch
            {
                int a = TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[ModelID].ints[0];
                Debug.LogError("Error Loading Material " + ModelID + ", " + i + ", " + a + ", " + TrickyMapInterface.Instance.materialJson.MaterialsJsons[a].TextureID);
            }
            newGameObject.transform.parent = transform;
            newGameObject.transform.localScale = new Vector3(1, 1, 1);
            newGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            meshes.Add(newGameObject);
        }

        transform.localPosition = InstancePosition * TrickyMapInterface.Scale;
        transform.localEulerAngles = rotation;
        transform.localScale = scale * TrickyMapInterface.Scale;

        Oldposition = transform.localPosition;
        Oldrotation = transform.localEulerAngles;
        Oldscale = transform.localScale;

    }

    public void UpdateTransform()
    {
        transform.localPosition = InstancePosition * TrickyMapInterface.Scale;
        transform.localEulerAngles = rotation;
        transform.localScale = scale * TrickyMapInterface.Scale;
    }

    public void SelectedObject()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            meshes[i].GetComponent<MeshRenderer>().material.SetFloat("_OutlineWidth", 50);
            meshes[i].GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.red);
            meshes[i].GetComponent<MeshRenderer>().material.SetFloat("_OpacityMaskOutline", 0.5f);
        }
    }

    public void UnSelectedObject()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            meshes[i].GetComponent<MeshRenderer>().material.SetFloat("_OutlineWidth", 0);
            meshes[i].GetComponent<MeshRenderer>().material.SetFloat("_OpacityMaskOutline", 0f);
            meshes[i].GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", new Color32(255,255,255,0));
        }
    }

    public InstanceJsonHandler.InstanceJson GenerateInstance()
    {
        InstanceJsonHandler.InstanceJson TempInstance = new InstanceJsonHandler.InstanceJson();
        TempInstance.InstanceName = InstanceName;

        TempInstance.Location = JsonUtil.Vector3ToArray(InstancePosition);
        TempInstance.Scale = JsonUtil.Vector3ToArray(scale);
        TempInstance.Rotation = JsonUtil.QuaternionToArray(Quaternion.Euler(rotation));

        TempInstance.Unknown5 = JsonUtil.Vector4ToArray(Unknown5);
        TempInstance.Unknown6 = JsonUtil.Vector4ToArray(Unknown6);
        TempInstance.Unknown7 = JsonUtil.Vector4ToArray(Unknown7);
        TempInstance.Unknown8 = JsonUtil.Vector4ToArray(Unknown8);
        TempInstance.Unknown9 = JsonUtil.Vector4ToArray(Unknown9);
        TempInstance.Unknown10 = JsonUtil.Vector4ToArray(Unknown10);
        TempInstance.Unknown11 = JsonUtil.Vector4ToArray(Unknown11);
        TempInstance.RGBA = JsonUtil.Vector4ToArray(RGBA);

        TempInstance.ModelID = ModelID;
        TempInstance.PrevInstance = PrevInstance;
        TempInstance.NextInstance = NextInstance;

        TempInstance.LowestXYZ = JsonUtil.Vector3ToArray(LowestXYZ);
        TempInstance.HighestXYZ = JsonUtil.Vector3ToArray(HighestXYZ);

        TempInstance.UnknownInt26 = UnknownInt26;
        TempInstance.UnknownInt27 = UnknownInt27;
        TempInstance.UnknownInt28 = UnknownInt28;
        TempInstance.ModelID2 = ModelID2;
        TempInstance.UnknownInt30 = UnknownInt30;
        TempInstance.UnknownInt31 = UnknownInt31;
        TempInstance.UnknownInt32 = UnknownInt32;
        return TempInstance;
    }

    private void Update()
    {
        if(Oldscale!=transform.localScale)
        {
            Oldscale = transform.localScale;
            scale = transform.localScale / TrickyMapInterface.Scale;
        }
        if (Oldrotation != transform.localEulerAngles)
        {
            Oldrotation = transform.eulerAngles;
            rotation = transform.localEulerAngles;
        }
        if (Oldposition != transform.localPosition)
        {
            Oldposition = transform.localPosition;
            InstancePosition = transform.localPosition / TrickyMapInterface.Scale;
        }
    }
}
