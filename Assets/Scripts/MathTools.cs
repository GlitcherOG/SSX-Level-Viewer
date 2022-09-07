using SSX_Modder.FileHandlers.MapEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathTools
{
    public static Vertex3 Highest(Vertex3 current, Vector3 vector3)
    {
        Vertex3 vertex = ConversionTools.Vector3ToVertex3(vector3);
        if (vertex.X > current.X)
        {
            current.X = vertex.X;
        }
        if (vertex.Y > current.Y)
        {
            current.Y = vertex.Y;
        }
        if (vertex.Z > current.Z)
        {
            current.Z = vertex.Z;
        }
        return current;
    }

    public static Vertex3 Lowest(Vertex3 current, Vector3 vector3)
    {
        Vertex3 vertex = ConversionTools.Vector3ToVertex3(vector3);
        if (vertex.X < current.X)
        {
            current.X = vertex.X;
        }
        if (vertex.Y < current.Y)
        {
            current.Y = vertex.Y;
        }
        if (vertex.Z < current.Z)
        {
            current.Z = vertex.Z;
        }
        return current;
    }
}
