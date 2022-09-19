using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDegree : MonoBehaviour
{
    NURBS.Surface surface;
    NURBS.ControlPoint[,] cps;

    void Start()
    {
        cps = new NURBS.ControlPoint[5, 2];

        cps[0,0] = new NURBS.ControlPoint(0, 0, 0, 1);
        cps[0,1] = new NURBS.ControlPoint(0, 0, 1, 1);
        cps[1,0] = new NURBS.ControlPoint(1, 0, 0, 1);
        cps[1,1] = new NURBS.ControlPoint(1, 0, 1, 1);
        cps[2,0] = new NURBS.ControlPoint(1, 1, 0, 1);
        cps[2,1] = new NURBS.ControlPoint(1, 1, 1, 1);
        cps[3,0] = new NURBS.ControlPoint(2, 0, 0, 1);
        cps[3,1] = new NURBS.ControlPoint(2, 0, 1, 1);
        cps[4,0] = new NURBS.ControlPoint(2, 1, 0, 1);
        cps[4,1] = new NURBS.ControlPoint(2, 1, 1, 1);

        surface = new NURBS.Surface(cps, 2, 1);

        surface.DegreeU(1);
        transform.Find("1").GetComponent<MeshFilter>().mesh = surface.BuildMesh(50, 50);
        surface.DegreeU(2);
        transform.Find("2").GetComponent<MeshFilter>().mesh = surface.BuildMesh(50, 50);
        surface.DegreeU(3);
        transform.Find("3").GetComponent<MeshFilter>().mesh = surface.BuildMesh(50, 50);
        surface.DegreeU(4);
        transform.Find("4").GetComponent<MeshFilter>().mesh = surface.BuildMesh(50, 50);
    }
}
