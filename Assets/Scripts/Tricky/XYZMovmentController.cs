using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZMovmentController : MonoBehaviour
{
    bool active;
    bool centremode;
    Vector3 oldPos;
    public GameObject Parent;
    public GameObject OldParent;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.localScale = Vector3.one * Vector3.Distance(Camera.main.transform.position, transform.position) / 150;
            if (!centremode)
            {
                if (oldPos != transform.position)
                {
                    Parent.transform.position = transform.position;
                    oldPos=transform.position;
                }
            }
            else
            {
                if (oldPos != transform.position)
                {
                    Parent.transform.position += transform.position-oldPos;
                    oldPos = transform.position;
                }
            }
        }
    }

    public void SetParent(GameObject gameObject)
    {
        centremode = false;
        transform.position = gameObject.transform.position;
        oldPos = transform.position;
        Parent = gameObject;
        OldParent = gameObject;
        active = true;
    }

    public void SetParentCentreMode(GameObject gameObject, Vector3 vector3)
    {
        centremode = true;
        transform.position = vector3;
        oldPos = vector3;
        Parent = gameObject;
        OldParent = gameObject;
        active = true;
    }

    public void RemoveParent()
    {
        Parent = null;
        active = false;
    }

    public void SetOldParent()
    {
        Parent = OldParent;
        transform.position = oldPos;
        active = true;
    }
}
