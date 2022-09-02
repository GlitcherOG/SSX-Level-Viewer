using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class RowCollumHandler : MonoBehaviour
{
    public TMP_Text PointName;
    public TMP_InputField x;
    public TMP_InputField y;
    public TMP_InputField z;

    public UnityEvent SendUpdatePing;

    public Vector3 vector3;

    public void SetName(string NewName)
    {
        PointName.text = NewName;
    }

    public void SetXYZ(Vector3 point)
    {
        vector3=point;
        x.text = point.x.ToString();
        y.text = point.y.ToString();
        z.text = point.z.ToString();
    }
    public void SetColour(Color color)
    {
        PointName.color = color;
    }

    public Vector3 GrabXYZ()
    {
        try
        {
            Vector3 result = new Vector3();
            result.x = Convert.ToSingle(x.text);
            result.y = Convert.ToSingle(y.text);
            result.z = Convert.ToSingle(z.text);
            vector3 = result;
            return result;
        }
        catch
        {
            return vector3;
        }
    }

    public void UpdatedPoints()
    {
        SendUpdatePing.Invoke();
    }
}
