using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExampleWaveSurface : MonoBehaviour
{
    NURBS.Surface surface;
    NURBS.ControlPoint[,] cps;

    [Range(0, 1f)]
    public float u = 0;
    [Range(0, 1f)]
    public float v = 0;

    void Start()
    {
        cps = new NURBS.ControlPoint[5, 4];
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                cps[i, j] = new NURBS.ControlPoint(i - (5 - 1) / 2f, Mathf.Abs(j - 2f), j - (4 - 1) / 2f, 1);
            }
        }

        surface = new NURBS.Surface(cps, 2, 2);

        GetComponent<MeshFilter>().mesh = new Mesh();
    }

    float offset = 0;
    void Update()
    {
        //Wave effect
        for (int i = 0; i < surface.controlPoints.GetLength(1); i++)
        {
            for (int j = 0; j < surface.controlPoints.GetLength(0); j++)
            {
                surface.controlPoints[j, i].y = Mathf.Sin(2 * j + offset);
            }
        }
        offset += Time.deltaTime;



        surface.BuildMesh(25, 25, GetComponent<MeshFilter>().mesh);
    }

    void OnDrawGizmos()
    {
        if (surface != null)
        {
            Vector3 uvPt = surface.GetPoint(u, v);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.rotation * uvPt + transform.position, 0.075f);
        }

        if (cps != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var cp in cps)
            {
                Gizmos.DrawSphere(transform.rotation * new Vector3(cp.x, cp.y, cp.z) + transform.position, 0.05f);
            }
        }
    }
}
