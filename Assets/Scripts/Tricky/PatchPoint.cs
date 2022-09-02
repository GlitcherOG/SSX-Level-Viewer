using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatchPoint : MonoBehaviour
{
    public int ID;
    public bool DisableUpdate;
    public Vector3 OldPosition;
    public PatchObject PatchObject;
    public UnityAction<int> unityEvent;

    private void Start()
    {
        OldPosition = transform.position;
    }

    void Update()
    {
        if (!DisableUpdate)
        {
            transform.localScale = Vector3.one * Vector3.Distance(Camera.main.transform.position, transform.position) / 25;
            if (OldPosition != transform.position)
            {
                OldPosition = transform.position;
                unityEvent.Invoke(ID);
            }
        }
    }
}
