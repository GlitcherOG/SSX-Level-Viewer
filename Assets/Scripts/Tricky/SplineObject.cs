using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers.MapEditor;

public class SplineObject : MonoBehaviour
{
    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;

    public int Unknown1;
    public int SplineSegmentCount;
    public int SplineSegmentPosition;
    public int Unknown2;

    public GameObject SplineSegmentPrefab;
    public List<SplineSegmentObject> splineSegmentObjects = new List<SplineSegmentObject>();

    public void LoadSpline(Spline spline, List<SplinesSegments> splinesSegments)
    {
        LowestXYZ = VertexToVector(spline.LowestXYZ);
        HighestXYZ = VertexToVector(spline.HighestXYZ);

        Unknown1 = spline.Unknown1;
        SplineSegmentCount = splinesSegments.Count;
        SplineSegmentPosition = spline.SplineSegmentPosition;
        Unknown2 = spline.Unknown2;

        for (int i = 0; i < splinesSegments.Count; i++)
        {
            var TempGameobject = Instantiate(SplineSegmentPrefab, transform);
            TempGameobject.GetComponent<SplineSegmentObject>().LoadSplineSegment(splinesSegments[i]);

            splineSegmentObjects.Add(TempGameobject.GetComponent<SplineSegmentObject>());
        }
    }

    Vector3 VertexToVector(Vertex3 vertex3)
    {
        return new Vector3(vertex3.X, vertex3.Z, vertex3.Y);
    }
}
