using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCylinderSurface : MonoBehaviour
{
    NURBS.Surface surface;
    NURBS.ControlPoint[,] cps;

    void Start()
    {
        cps = new NURBS.ControlPoint[9, 2];
        for (int j = 0; j < 2; j++)
        {
            cps[8, j] = new NURBS.ControlPoint(1, 0, j, 1);
            cps[7, j] = new NURBS.ControlPoint(1, 1, j, 0.7071067f);
            cps[6, j] = new NURBS.ControlPoint(0, 1, j, 1);
            cps[5, j] = new NURBS.ControlPoint(-1, 1, j, 0.7071067f);
            cps[4, j] = new NURBS.ControlPoint(-1, 0, j, 1);
            cps[3, j] = new NURBS.ControlPoint(-1, -1, j, 0.7071067f);
            cps[2, j] = new NURBS.ControlPoint(0, -1, j, 1);
            cps[1, j] = new NURBS.ControlPoint(1, -1, j, 0.7071067f);
            cps[0, j] = new NURBS.ControlPoint(1, 0, j, 1);
        }

        float[] knotsU = new float[9 + 2 + 1];
        knotsU[0] = 0;
        knotsU[1] = 0;
        knotsU[2] = 0;
        knotsU[3] = 1 / 2f;
        knotsU[4] = 1 / 2f;
        knotsU[5] = 1;
        knotsU[6] = 1;
        knotsU[7] = 3 / 2f;
        knotsU[8] = 3 / 2f;
        knotsU[9] = 2;
        knotsU[10] = 2;
        knotsU[11] = 2;

        float[] knotsV = NURBS.Surface.GenerateKnots(1, 2, true, true);

        surface = new NURBS.Surface(cps, 2, 1, knotsU, knotsV);

        GetComponent<MeshFilter>().mesh = surface.BuildMesh(50, 50);
    }
}
