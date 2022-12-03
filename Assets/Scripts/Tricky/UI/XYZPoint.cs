using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZPoint : MonoBehaviour
{
    public bool Active;
    public GameObject Parent;
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
                    Vector3 NewPosition = Parent.transform.position + transform.right * (hit.point.x - HitPoint.x);
                    if (XYZMovmentController.Instance.GridAlign!=0)
                    {
                        NewPosition.x = RoundToNearest(NewPosition.x, XYZMovmentController.Instance.GridAlign*TrickyMapInterface.Scale);
                    }
                    Parent.transform.position = NewPosition;
                }
                HitPoint = hit.point;
            }
            if (axis == XYZAxis.YAxis)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    Vector3 NewPosition = Parent.transform.position + transform.up * (hit.point.y - HitPoint.y);
                    if (XYZMovmentController.Instance.GridAlign != 0)
                    {
                        NewPosition.y = RoundToNearest(NewPosition.y, XYZMovmentController.Instance.GridAlign * TrickyMapInterface.Scale);
                    }
                    Parent.transform.position = NewPosition;
                }
                HitPoint = hit.point;
            }
            if (axis == XYZAxis.ZAxis)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layerMask = 1 << 6;
                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    Vector3 NewPosition = Parent.transform.position + transform.forward * (hit.point.z - HitPoint.z);
                    if (XYZMovmentController.Instance.GridAlign != 0)
                    {
                        NewPosition.z = RoundToNearest(NewPosition.z, XYZMovmentController.Instance.GridAlign * TrickyMapInterface.Scale);
                    }
                    Parent.transform.position = NewPosition;
                }
                HitPoint = hit.point;
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetUnactive();
            }
        }
    }


    float RoundToNearest(float Input, float RoundingNumber)
    {
        return Mathf.RoundToInt(Input / RoundingNumber) * RoundingNumber;
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
        Parent.GetComponent<XYZMovmentController>().DisableMovement(this.gameObject);
        Active = true;
    }

    public void SetUnactive()
    {
        Parent.GetComponent<XYZMovmentController>().EnableMovementAll();
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
