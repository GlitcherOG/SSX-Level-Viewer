using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZRotation : MonoBehaviour
{
    public bool Active;
    public XYZAxis axis;
    public XYZMovmentController Controller;
    public Vector3 origin;
    public Vector3 point2;
    public float Rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Active)
        {
            if (axis == XYZAxis.XAxis)
            {
                //X-Axis
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    point2 = hit.point;
                    Rotation = Vector2.SignedAngle(new Vector2(transform.InverseTransformPoint(origin).x, transform.InverseTransformPoint(origin).z), new Vector2(transform.InverseTransformPoint(point2).x, transform.InverseTransformPoint(point2).z));
                    Controller.transform.Rotate(Rotation, 0, 0);
                    origin = hit.point;
                }
            }

            if (axis == XYZAxis.YAxis)
            {
                //Y-Axis
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    point2 = hit.point;
                    Rotation = Vector2.SignedAngle(new Vector2(transform.InverseTransformPoint(origin).x, transform.InverseTransformPoint(origin).z), new Vector2(transform.InverseTransformPoint(point2).x, transform.InverseTransformPoint(point2).z));
                    Controller.transform.Rotate(0, -Rotation, 0);
                    origin = hit.point;
                }
            }

            if (axis == XYZAxis.ZAxis)
            {
                //Z-Axis
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    point2 = hit.point;
                    Rotation = Vector2.SignedAngle(new Vector2(transform.InverseTransformPoint(origin).x, transform.InverseTransformPoint(origin).z), new Vector2(transform.InverseTransformPoint(point2).x, transform.InverseTransformPoint(point2).z));
                    Controller.transform.Rotate(0, 0, Rotation);
                    origin = hit.point;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetUnactive();
            }
        }
    }

    public void SetActive()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 6;
        if (Physics.Raycast(ray, out hit, 10000, layerMask))
        {
            origin = hit.point;
        }
        Active = true;
    }

    public void SetUnactive()
    {
        Active = false;
    }

    public enum XYZAxis
    {
        XAxis,
        YAxis,
        ZAxis
    }
}
