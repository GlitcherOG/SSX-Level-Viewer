using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XYZMovmentController : MonoBehaviour
{
    public static XYZMovmentController Instance { get; private set; }
    public bool active;
    public GimzoMode gimzoMode;
    bool centremode;
    Vector3 oldPos;
    Vector3 oldRot;
    public GameObject Parent;
    public GameObject OldParent;

    [Space(10)]
    public GameObject Movement;
    public GameObject MovementX;
    public GameObject MovementY;
    public GameObject MovementZ;

    [Space(10)]
    public GameObject Rotation;
    public GameObject RotationX;
    public GameObject RotationY;
    public GameObject RotationZ;
    [Space(10)]
    public float GridAlign;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (active)
        {
            transform.localScale = Vector3.one * Vector3.Distance(Camera.main.transform.position, transform.position) / 150;
            if (gimzoMode == GimzoMode.Movement)
            {
                if (!centremode)
                {
                    if (oldPos != transform.position)
                    {
                        Parent.transform.position = transform.position;
                        oldPos = transform.position;
                    }
                }
                else
                {
                    if (oldPos != transform.position)
                    {
                        Parent.transform.position += transform.position - oldPos;
                        oldPos = transform.position;
                    }
                }
            }
            if (gimzoMode == GimzoMode.Rotation)
            {
                if (oldRot != transform.eulerAngles)
                {
                    Parent.transform.rotation = transform.rotation;
                    oldRot = transform.eulerAngles;
                }
            }
        }
    }

    public void DisableRotation(GameObject ThisObject)
    {
        RotationX.SetActive(false);
        RotationY.SetActive(false);
        RotationZ.SetActive(false);
        ThisObject.SetActive(true);
    }

    public void EnableRotaiton()
    {
        RotationX.SetActive(true);
        RotationY.SetActive(true);
        RotationZ.SetActive(true);
    }

    public void DisableMovement(GameObject ThisObject)
    {
        MovementX.SetActive(false);
        MovementY.SetActive(false);
        MovementZ.SetActive(false);
        ThisObject.SetActive(true);
    }

    public void EnableMovementAll()
    {
        MovementX.SetActive(true);
        MovementY.SetActive(true);
        MovementZ.SetActive(true);
    }

    public void SetParent(GameObject gameObject)
    {
        centremode = false;
        transform.position = gameObject.transform.position;
        oldPos = gameObject.transform.position;
        oldRot = gameObject.transform.eulerAngles;
        Parent = gameObject;
        OldParent = gameObject;
        RotationCheck();
        active = true;
    }


    //public void SetParentCentreMode(GameObject gameObject, Vector3 vector3)
    //{
    //    centremode = true;
    //    transform.position = vector3;
    //    oldPos = vector3;
    //    oldRot = gameObject.transform.eulerAngles;
    //    Parent = gameObject;
    //    OldParent = gameObject;
    //    RotationCheck();
    //    active = true;
    //}

    public void RemoveParent()
    {
        Parent = null;
        active = false;
    }

    public void SetOldParent()
    {
        Parent = OldParent;
        RotationCheck();
        transform.position = oldPos;
        active = true;
    }

    void RotationCheck()
    {
        if (gimzoMode == GimzoMode.Movement)
        {
            EnableMovement();
        }
        if (gimzoMode == GimzoMode.Rotation)
        {
            EnableRotation();
        }
    }

    public void EnableMovement()
    {
        gimzoMode = GimzoMode.Movement;
        transform.eulerAngles = new Vector3(0, 0, 0);
        Movement.SetActive(true);
        Rotation.SetActive(false);
    }

    public void EnableRotation()
    {
        gimzoMode = GimzoMode.Rotation;
        transform.eulerAngles = oldRot;
        Movement.SetActive(false);
        Rotation.SetActive(true);
    }

    public enum GimzoMode
    {
        Movement,
        Rotation
    }
}
