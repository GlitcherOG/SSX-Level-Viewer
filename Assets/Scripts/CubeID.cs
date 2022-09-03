using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeID : MonoBehaviour
{
    void OnDrawGizmos()
    {
        if (Vector3.Distance(this.transform.position, Camera.current.transform.position) < 10000f)
        {
#if UNITY_EDITOR
            Handles.Label(transform.position, gameObject.name);
#endif
        }
    }

}
