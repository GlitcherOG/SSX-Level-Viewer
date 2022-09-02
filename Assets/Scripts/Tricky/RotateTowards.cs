using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    Vector3 vector3 = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        vector3=Vector3.RotateTowards(transform.rotation.eulerAngles, Camera.main.transform.eulerAngles, 1000f, 10000f);
        vector3.x = 0f;
        vector3.z = 0f;
        this.transform.eulerAngles = vector3;
    }
}
