using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

public class LightingObject : MonoBehaviour
{
    public string LightName;
    public int Type;
    public int spriteRes;
    public float UnknownFloat1;
    public int UnknownInt1;
    public Vector3 Colour;
    public Vector3 Direction;
    public Vector3 Postion;
    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;
    public float UnknownFloat2;
    public int UnknownInt2;
    public float UnknownFloat3;
    public int UnknownInt3;

    public Light light;

    public void LoadLightingObject(LightJsonHandler.LightJson lightJson)
    {
        LightName = lightJson.LightName;
        Type = lightJson.Type;
        spriteRes = lightJson.spriteRes;
        UnknownFloat1 = lightJson.UnknownFloat1;
        UnknownInt1 = lightJson.UnknownInt1;
        Colour = JsonUtil.ArrayToVector3(lightJson.Colour);
        Direction = JsonUtil.ArrayToVector3(lightJson.Direction);
        Postion = JsonUtil.ArrayToVector3(lightJson.Postion);
        LowestXYZ = JsonUtil.ArrayToVector3(lightJson.LowestXYZ);
        HighestXYZ = JsonUtil.ArrayToVector3(lightJson.HighestXYZ);
        UnknownFloat2 = lightJson.UnknownFloat2;
        UnknownInt2 = lightJson.UnknownInt2;
        UnknownFloat3 = lightJson.UnknownFloat3;
        UnknownInt3 = lightJson.UnknownInt3;

        transform.localPosition = Postion*TrickyMapInterface.Scale;
        transform.localRotation = Quaternion.LookRotation(Direction, transform.up);


        if(Type==0)
        {
            light.type = LightType.Directional;
        }
        if(Type==1)
        {
            light.type = LightType.Area; //Spot
        }
        if(Type==2)
        {
            light.type = LightType.Point;
        }
        if(Type==3)
        {
            light.range = 0;
            //RenderSettings.ambientLight
        }
    }
}
