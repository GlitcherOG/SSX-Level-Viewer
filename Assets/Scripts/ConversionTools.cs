using SSX_Modder.FileHandlers.MapEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConversionTools
{
    //From SSX To Unity
    public static Vector3 Vertex3ToVector3(Vertex3 vertex3)
    {
        return new Vector3(vertex3.X, vertex3.Z, vertex3.Y);
    }

    //From Unity To SSX
    public static Vertex3 Vector3ToVertex3(Vector3 vector3, float w = 1f)
    {
        Vertex3 vertex3 = new Vertex3();
        vertex3.X = vector3.x;
        vertex3.Y = vector3.z;
        vertex3.Z = vector3.y;
        vertex3.W = w;
        return vertex3;
    }

    public static Vertex3 Vector2ToVertex3(Vector2 vector2, float z = 1, float w = 1f)
    {
        Vertex3 vertex3 = new Vertex3();
        vertex3.X = vector2.x;
        vertex3.Y = vector2.y;
        vertex3.Z = z;
        vertex3.W = w;
        return vertex3;
    }

    public static Vertex3 Vector4ToVertex3(Vector4 vector4)
    {
        Vertex3 vertex3 = new Vertex3();
        vertex3.X = vector4.x;
        vertex3.Y = vector4.y;
        vertex3.Z = vector4.z;
        vertex3.W = vector4.w;
        return vertex3;
    }
}
