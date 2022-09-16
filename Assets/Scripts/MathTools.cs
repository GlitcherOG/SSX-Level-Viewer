using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathTools
{
    public static Vector3 Highest(Vector3 current, Vector3 vector3)
    {
        Vector3 vertex = vector3;
        if (vertex.x > current.x)
        {
            current.x = vertex.x;
        }
        if (vertex.y > current.y)
        {
            current.y = vertex.y;
        }
        if (vertex.z > current.z)
        {
            current.z = vertex.z;
        }
        return current;
    }

    public static Vector3 Lowest(Vector3 current, Vector3 vector3)
    {
        Vector3 vertex = vector3;
        if (vertex.x < current.x)
        {
            current.x = vertex.x;
        }
        if (vertex.y < current.y)
        {
            current.y = vertex.y;
        }
        if (vertex.z < current.z)
        {
            current.z = vertex.z;
        }
        return current;
    }
}
