using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatchPoint : MonoBehaviour
{
    public int ID;
    public bool DisableUpdate;
    public Vector3 OldPosition;
    public UnityAction<int> unityEvent;

    private void Start()
    {
        OldPosition = transform.position;
    }

    void Update()
    {
        transform.localScale = Vector3.one * Vector3.Distance(Camera.main.transform.position, transform.position) / 25;
        if (!DisableUpdate)
        {
            if (OldPosition != transform.position)
            {
                OldPosition = transform.position;
                unityEvent.Invoke(ID);
            }
        }
    }

    public void ResetOldPosition()
    {
        OldPosition = transform.position;
    }

    public void Selected()
    {
        GetComponent<MeshRenderer>().material.SetFloat("_OutlineWidth", 0.5f);
    }

    public void UnSelected()
    {
        GetComponent<MeshRenderer>().material.SetFloat("_OutlineWidth", 0);
    }
}
