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

    //public Spline GenerateSpline(int StartPos)
    //{
    //    Spline spline = new Spline();
    //    spline.HighestXYZ = ConversionTools.Vector3ToVertex3(splineSegmentObjects[0].Point1);
    //    for (int i = 0; i < splineSegmentObjects.Count; i++)
    //    {
    //        spline.HighestXYZ = MathTools.Highest(spline.HighestXYZ, splineSegmentObjects[i].Point1);
    //        spline.HighestXYZ = MathTools.Highest(spline.HighestXYZ, splineSegmentObjects[i].Point2);
    //        spline.HighestXYZ = MathTools.Highest(spline.HighestXYZ, splineSegmentObjects[i].Point3);
    //        spline.HighestXYZ = MathTools.Highest(spline.HighestXYZ, splineSegmentObjects[i].Point4);
    //    }

    //    spline.LowestXYZ = ConversionTools.Vector3ToVertex3(splineSegmentObjects[0].Point1);
    //    for (int i = 0; i < splineSegmentObjects.Count; i++)
    //    {
    //        spline.LowestXYZ = MathTools.Lowest(spline.LowestXYZ, splineSegmentObjects[i].Point1);
    //        spline.LowestXYZ = MathTools.Lowest(spline.LowestXYZ, splineSegmentObjects[i].Point2);
    //        spline.LowestXYZ = MathTools.Lowest(spline.LowestXYZ, splineSegmentObjects[i].Point3);
    //        spline.LowestXYZ = MathTools.Lowest(spline.LowestXYZ, splineSegmentObjects[i].Point4);
    //    }

    //    spline.Unknown1 = Unknown1;
    //    spline.SplineSegmentCount = splineSegmentObjects.Count;
    //    spline.SplineSegmentPosition = StartPos;
    //    spline.Unknown2 = Unknown2;

    //    return spline;
    //}

    //public List<SplinesSegments> GetSegments(int StartPos, int ParentID)
    //{
    //    List<SplinesSegments> tempList = new List<SplinesSegments>();
    //    int a = StartPos;
    //    float PrevDistance = 0;
    //    for (int i = 0; i < splineSegmentObjects.Count; i++)
    //    {
    //        SplinesSegments segment = splineSegmentObjects[i].GenerateSplineSegment();
    //        segment.SplineParent = ParentID;
    //        if (i==0)
    //        {
    //            segment.PreviousSegment = -1;
    //        }
    //        else if(i==splineSegmentObjects.Count-1)
    //        {
    //            segment.NextSegment = -1;
    //        }
    //        else
    //        {
    //            segment.PreviousSegment = a - 1;
    //            segment.NextSegment = a + 1;
    //        }
    //        segment.PreviousSegmentsDistance = PrevDistance;
    //        PrevDistance += segment.SegmentDisatnce;

    //        a++;
    //        tempList.Add(segment);
    //    }

    //    return tempList;
    //}

}
