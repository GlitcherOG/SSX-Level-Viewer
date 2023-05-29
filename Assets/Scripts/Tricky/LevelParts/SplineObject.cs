using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

public class SplineObject : MonoBehaviour
{
    public string SplineName;

    public GameObject SplineSegmentPrefab;
    public List<SplineSegmentObject> splineSegmentObjects = new List<SplineSegmentObject>();

    public void LoadSpline(SplineJsonHandler.SplineJson spline)
    {
        SplineName= spline.SplineName;

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
        spline.Segments = new List<SplineJsonHandler.SegmentJson>();

        for (int i = 0; i < splineSegmentObjects.Count; i++)
        {
            spline.Segments.Add(splineSegmentObjects[i].GenerateSplineSegment());
        }

        return spline;
    }

}
