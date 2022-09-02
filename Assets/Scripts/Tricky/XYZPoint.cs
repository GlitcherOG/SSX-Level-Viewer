using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZPoint : MonoBehaviour
{
    public bool Active;
    public XYZAxis axis = XYZAxis.XAxis;
    public Vector2 InputPoint;
    public Vector3 OldPoint;
    public Vector3 HitPoint;

    public BoxCollider dragBox1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (axis == XYZAxis.XAxis)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    transform.parent.position = OldPoint + new Vector3(hit.point.x - HitPoint.x,0, 0);
                }
            }
            if (axis == XYZAxis.YAxis)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit,10000, layerMask))
                {
                    transform.parent.position = OldPoint + new Vector3(0,hit.point.y - HitPoint.y,0);
                }
            }
            if (axis == XYZAxis.ZAxis)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    transform.parent.position = OldPoint + new Vector3(0,0, hit.point.z - HitPoint.z);
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
        InputPoint = Input.mousePosition;
        OldPoint = transform.parent.position;
        dragBox1.enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            HitPoint = hit.point;
        }
        Active = true;
    }

    public void SetUnactive()
    {
        dragBox1.enabled = false;
        Active = false;
    }

    public enum XYZAxis
    {
        XAxis,
        YAxis,
        ZAxis
    }
}
