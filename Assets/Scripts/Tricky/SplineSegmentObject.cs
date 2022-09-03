using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers.MapEditor;

public class SplineSegmentObject : MonoBehaviour
{
    public Vector3 ProcessedPoint1;
    public Vector3 ProcessedPoint2;
    public Vector3 ProcessedPoint3;
    public Vector3 ProcessedPoint4;

    public Vector3 Point1;
    public Vector3 Point2;
    public Vector3 Point3;
    public Vector3 Point4;

    public LineRenderer lineRenderer;

    public void LoadSplineSegment(SplinesSegments segments)
    {

        ProcessedPoint1 = VertexToVector(segments.ControlPoint);
        ProcessedPoint2 = VertexToVector(segments.Point2);
        ProcessedPoint3 = VertexToVector(segments.Point3);
        ProcessedPoint4 = VertexToVector(segments.Point4);
        GeneratePoints();
        SetDataLineRender();
        transform.position = ProcessedPoint1 * TrickyMapInterface.Scale;
    }

    void GeneratePoints()
    {
        Point1 = ProcessedPoint1;
        Point2 = Point1 + ProcessedPoint2 / 3;
        Point3 = Point2 + (ProcessedPoint2 + ProcessedPoint3) / 3;
        Point4 = ProcessedPoint1 + ProcessedPoint2 + ProcessedPoint3 + ProcessedPoint4;
    }

    void SetDataLineRender()
    {
        lineRenderer.positionCount = 4;
        lineRenderer.SetPosition(0, (Point1 - Point1) *TrickyMapInterface.Scale);
        lineRenderer.SetPosition(1, (Point2 - Point1) *TrickyMapInterface.Scale);
        lineRenderer.SetPosition(2, (Point3 - Point1)* TrickyMapInterface.Scale);
        lineRenderer.SetPosition(3, (Point4 - Point1) * TrickyMapInterface.Scale);
    }

    Vector3 VertexToVector(Vertex3 vertex3)
    {
        return new Vector3(vertex3.X, vertex3.Z, vertex3.Y);
    }
}
