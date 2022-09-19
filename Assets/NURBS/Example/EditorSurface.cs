using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSurface : MonoBehaviour
{
    NURBS.Surface surface;

    public Transform[] controlPoints;

    [Range(0,3)]
    public int degreeU = 2;
    [Range(0,3)]
    public int degreeV = 2;

    [Range(1,50)]
    public int resolutionU = 25;
    [Range(1,50)]
    public int resolutionV = 25;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().sharedMesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        //Control points
        NURBS.ControlPoint[,] cps = new NURBS.ControlPoint[2, 2];
        int c = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3 pos = controlPoints[c].transform.localPosition;
                cps[i,j] = new NURBS.ControlPoint(pos.x, pos.y, pos.z, 1);
                c++;
            }
        }

        //Setup if not yet
        if(surface == null)
            surface = new NURBS.Surface(cps, degreeU, degreeV);

        //Update degree
        surface.DegreeU(degreeU);
        surface.DegreeV(degreeV);

        //Update control points
        surface.controlPoints = cps;

        //Build mesh (reusing Mesh to save GC allocation)
        surface.BuildMesh(resolutionU, resolutionV, GetComponent<MeshFilter>().sharedMesh);
    }
}
