using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathTools
{
    public static Vector3 FixYandZ(Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.z, vector3.y);
    }

    public static Vector3 FixXandZ(Vector3 vector3)
    {
        return new Vector3(vector3.z, vector3.y, vector3.x);
    }
}
