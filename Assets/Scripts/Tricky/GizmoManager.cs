using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GizmoManager : MonoBehaviour
{
    public GameObject GizmoPrefab;
    public GameObject GizmoObject;
    [Space(10)]

    public VectorInput Position;
    public VectorInput Rotation;
    public VectorInput Scale;

    [Space(10)]
    public TMP_InputField GridAlign;

    Vector3 OldPosition;
    Vector3 OldScale;
    Vector3 OldRotation;
    bool DisableUpdate;

    private void Update()
    {
        if(GizmoObject!=null)
        {
            if(GizmoObject.activeInHierarchy && !DisableUpdate)
            {
                if(OldPosition!= GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localPosition)
                {
                    OldPosition = GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localPosition;
                    UpdateUI();
                }
                if (OldScale != GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localScale)
                {
                    OldScale = GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localScale;
                    UpdateUI();
                }
                if (OldRotation != GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localEulerAngles)
                {
                    OldRotation = GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localEulerAngles;
                    UpdateUI();
                }
            }
        }
    }

    public void UpdateUI()
    {
        if(!DisableUpdate)
        {
            DisableUpdate = true;
            Position.SetXYZ(OldPosition / TrickyMapInterface.Scale);
            Rotation.SetXYZ(OldRotation);
            Scale.SetXYZ(OldScale / TrickyMapInterface.Scale);
            DisableUpdate = false;
        }
    }

    public void UpdatePosition()
    {
        if (!DisableUpdate)
        {
            DisableUpdate = true;
            GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localPosition = Position.GrabXYZ()*TrickyMapInterface.Scale;
            GizmoObject.GetComponent<XYZMovmentController>().SetParent(GizmoObject.GetComponent<XYZMovmentController>().Parent);
            DisableUpdate = false;
        }
    }

    public void UpdateRotation()
    {
        if (!DisableUpdate)
        {
            DisableUpdate = true;
            GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localEulerAngles = Rotation.GrabXYZ();
            GizmoObject.GetComponent<XYZMovmentController>().SetParent(GizmoObject.GetComponent<XYZMovmentController>().Parent);
            DisableUpdate = false;
        }
    }


    public void UpdateScale()
    {
        if (!DisableUpdate)
        {
            DisableUpdate = true;
            GizmoObject.GetComponent<XYZMovmentController>().Parent.transform.localScale = Scale.GrabXYZ();
            GizmoObject.GetComponent<XYZMovmentController>().SetParent(GizmoObject.GetComponent<XYZMovmentController>().Parent);
            DisableUpdate = false;
        }
    }

    public void UpdateGridAlign(string Text)
    {
        try
        {
            GridAlign.GetComponent<Image>().color = Color.white;
            GizmoObject.GetComponent<XYZMovmentController>().GridAlign = float.Parse(Text);
        }
        catch
        {
            GridAlign.GetComponent<Image>().color = Color.red;
        }
    }

}
