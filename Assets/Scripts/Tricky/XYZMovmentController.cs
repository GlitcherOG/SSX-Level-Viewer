using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZMovmentController : MonoBehaviour
{
    bool active;
    public GameObject Parent;
    public GameObject OldParent;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.localScale = Vector3.one * Vector3.Distance(Camera.main.transform.position, transform.position) / 150;
            Parent.transform.position = transform.position;
        }
    }

    public void SetParent(GameObject gameObject)
    {
        transform.position = gameObject.transform.position;
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
        transform.position = Parent.transform.position;
        active = true;
    }
}
