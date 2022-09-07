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
        LowestXYZ = ConversionTools.Vertex3ToVector3(spline.LowestXYZ);
        HighestXYZ = ConversionTools.Vertex3ToVector3(spline.HighestXYZ);

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

    public Spline GenerateSpline()
    {
        Spline spline = new Spline();
        spline.

        return spline;
    }
}
