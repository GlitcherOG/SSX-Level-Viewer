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

    public Vector3 Unknown5; //Something to do with lighting
    public Vector3 Unknown6; //Lighting Continued?
    public Vector3 Unknown7; 
    public Vector3 Unknown8;
    public Vector3 Unknown9; //Some Lighting Thing
    public Vector3 Unknown10;
    public Vector3 Unknown11;
    public Vector4 RGBA;

    public int UnknownInt13;
    public int UnknownInt14;
    public int UnknownInt15;
    public int UnknownInt16;
    public int ModelID;
    public int PrevInstance; //Next Connected Model 
    public int NextInstance; //Prev Connected Model

    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;

    public int UnknownInt23;
    public int UnknownInt24;
    public int UnknownInt25;
    public int UnknownInt26;
    public int UnknownInt27;
    public int UnknownInt28;
    public int ModelID2;
    public int UnknownInt30;
    public int UnknownInt31;
    public int UnknownInt32;

    Vector3 oldPos;
    Vector3 oldRot;
    Vector3 oldScale;

    public void LoadInstance(InstanceJsonHandler.InstanceJson instance)
    {
        InstanceName = instance.InstanceName;

        rotation = JsonUtil.ArrayToQuaternion(instance.Rotation).eulerAngles;
        scale = MathTools.FixYandZ(JsonUtil.ArrayToVector3(instance.Scale));
        InstancePosition = MathTools.FixYandZ(JsonUtil.ArrayToVector3(instance.Location));

        Unknown5 = JsonUtil.ArrayToVector3(instance.Unknown5);
        Unknown6 = JsonUtil.ArrayToVector3(instance.Unknown6);
        Unknown7 = JsonUtil.ArrayToVector3(instance.Unknown7);
        Unknown8 = JsonUtil.ArrayToVector3(instance.Unknown8);
        Unknown9 = JsonUtil.ArrayToVector3(instance.Unknown9);
        Unknown10 = JsonUtil.ArrayToVector3(instance.Unknown10);
        Unknown11 = JsonUtil.ArrayToVector3(instance.Unknown11);
        Unknown11 = JsonUtil.ArrayToVector3(instance.Unknown11);
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


        transform.position = InstancePosition * TrickyMapInterface.Scale;
        transform.eulerAngles = rotation;
        //transform.localScale = matrix4X4.lossyScale * TrickyMapInterface.Scale;

        oldPos = transform.position;
        oldRot = rotation;
        oldScale = scale;
    }

    //public Instance GenerateInstance()
    //{
    //    Instance TempInstance = new Instance();
    //    UpdateMatrix();
    //    TempInstance.MatrixCol1 = ConversionTools.Vector3ToVertex3(matrix4X4.GetColumn(0), 0f);
    //    TempInstance.MatrixCol2 = ConversionTools.Vector3ToVertex3(matrix4X4.GetColumn(2), 0f);
    //    TempInstance.MatrixCol3 = ConversionTools.Vector3ToVertex3(matrix4X4.GetColumn(1), 0f);
    //    TempInstance.InstancePosition = ConversionTools.Vector3ToVertex3(matrix4X4.GetColumn(3), 1f);
    //    TempInstance.Unknown5 = ConversionTools.Vector3ToVertex3(Unknown5, 0f);
    //    TempInstance.Unknown6 = ConversionTools.Vector3ToVertex3(Unknown6, 0f);
    //    TempInstance.Unknown7 = ConversionTools.Vector3ToVertex3(Unknown7, 0f);
    //    TempInstance.Unknown8 = ConversionTools.Vector3ToVertex3(Unknown8, 1f);
    //    TempInstance.Unknown9 = ConversionTools.Vector3ToVertex3(Unknown9, 0f);
    //    TempInstance.Unknown10 = ConversionTools.Vector3ToVertex3(Unknown10, 0f);
    //    TempInstance.Unknown11 = ConversionTools.Vector3ToVertex3(Unknown11, 0f);
    //    TempInstance.RGBA = ConversionTools.Vector4ToVertex3(RGBA);

    //    TempInstance.ModelID = ModelID;
    //    TempInstance.PrevInstance = PrevInstance;
    //    TempInstance.NextInstance = NextInstance;

    //    TempInstance.LowestXYZ = ConversionTools.Vector3ToVertex3(LowestXYZ);
    //    TempInstance.HighestXYZ = ConversionTools.Vector3ToVertex3(HighestXYZ);

    //    TempInstance.UnknownInt26 = UnknownInt26;
    //    TempInstance.UnknownInt27 = UnknownInt27;
    //    TempInstance.UnknownInt28 = UnknownInt28;
    //    TempInstance.ModelID2 = ModelID2;
    //    TempInstance.UnknownInt30 = UnknownInt30;
    //    TempInstance.UnknownInt31 = UnknownInt31;
    //    TempInstance.UnknownInt32 = UnknownInt32;
    //    return TempInstance;
    //}

    private void Update()
    {
        if(oldPos!=transform.position)
        {
            oldPos = transform.position;
            InstancePosition = transform.position / TrickyMapInterface.Scale;
        }
        //if(oldScale!=transform.localScale)
        //{
        //    oldScale = transform.localScale;
        //    scale = transform.localScale/TrickyMapInterface.Scale;
        //}
        if(oldRot!=transform.eulerAngles)
        {
            oldRot = transform.eulerAngles;
            rotation = transform.eulerAngles;
        }
    }
}
