using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

public class SplineObject : MonoBehaviour
{
    public string SplineName;

    public int Unknown1;
    public int Unknown2;

    public GameObject SplineSegmentPrefab;
    public List<SplineSegmentObject> splineSegmentObjects = new List<SplineSegmentObject>();

    public void LoadSpline(SplineJsonHandler.SplineJson spline)
    {
        SplineName= spline.SplineName;
        Unknown1 = spline.Unknown1;
        Unknown2 = spline.Unknown2;

        for (int i = 0; i < spline.Segments.Count; i++)
        {
            var TempGameobject = Instantiate(SplineSegmentPrefab, transform);
            TempGameobject.GetComponent<SplineSegmentObject>().LoadSplineSegment(spline.Segments[i]);

            splineSegmentObjects.Add(TempGameobject.GetComponent<SplineSegmentObject>());
        }
    }

    public SplineJsonHandler.SplineJson GenerateSpline()
    {
        SplineJsonHandler.SplineJson spline = new SplineJsonHandler.SplineJson();

        spline.SplineName = SplineName;
        spline.Unknown1 = Unknown1;
        spline.Unknown2 = Unknown2;
        spline.SegmentCount = splineSegmentObjects.Count;
        spline.Segments = new List<SplineJsonHandler.SegmentJson>();

        for (int i = 0; i < splineSegmentObjects.Count; i++)
        {
            spline.Segments.Add(splineSegmentObjects[i].GenerateSplineSegment());
        }

        return spline;
    }

}
