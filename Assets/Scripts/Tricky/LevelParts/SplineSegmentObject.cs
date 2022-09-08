using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers.MapEditor;
using System.Linq;

public class SplineSegmentObject : MonoBehaviour
{
    public Vector3 ProcessedPoint1;
    public Vector3 ProcessedPoint2;
    public Vector3 ProcessedPoint3;
    public Vector3 ProcessedPoint4;
    [Space(10)]
    public Vector3 Point1;
    public Vector3 Point2;
    public Vector3 Point3;
    public Vector3 Point4;
    [Space(10)]
    public Vector4 ScalingPoint;
    public Vector3 NormalizedScalingPoint;
    [Space(10)]
    public int PreviousSegment;
    public int NextSegment; //Model ID Or Object Parent?
    public int SplineParent;
    [Space(10)]
    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;
    [Space(10)]
    public float SegmentDisatnce;
    public float PreviousSegmentsDistance;
    public int Unknown32;
    
    public LineRenderer lineRenderer;
    public GameObject PointPrefab;
    public List<PatchPoint> PatchPoints = new List<PatchPoint>();

    private int curveCount = 0;
    private int SEGMENT_COUNT = 50;


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void LoadSplineSegment(SplinesSegments segments)
    {
        ProcessedPoint1 = ConversionTools.Vertex3ToVector3(segments.ControlPoint);
        ProcessedPoint2 = ConversionTools.Vertex3ToVector3(segments.Point2);
        ProcessedPoint3 = ConversionTools.Vertex3ToVector3(segments.Point3);
        ProcessedPoint4 = ConversionTools.Vertex3ToVector3(segments.Point4);

        ScalingPoint = new Vector4(segments.ScalingPoint.X, segments.ScalingPoint.Y, segments.ScalingPoint.Z,segments.ScalingPoint.W);
        NormalizedScalingPoint = ScalingPoint/ScalingPoint.w;

        PreviousSegment = segments.PreviousSegment;
        NextSegment = segments.NextSegment;
        SplineParent = segments.SplineParent;

        LowestXYZ = ConversionTools.Vertex3ToVector3(segments.LowestXYZ);
        HighestXYZ = ConversionTools.Vertex3ToVector3(segments.HighestXYZ);

        SegmentDisatnce = segments.SegmentDisatnce;
        PreviousSegmentsDistance = segments.PreviousSegmentsDistance;
        Unknown32 = segments.Unknown32;

        GeneratePoints();
        SetDataLineRender(false);
    }

    float GenerateDistance()
    {
        float distance = 0;
        for (int i = 0; i < lineRenderer.positionCount-1; i++)
        {
            distance += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }
        return distance;
    }

    public SplinesSegments GenerateSplineSegment()
    {
        SplinesSegments segments = new SplinesSegments();
        ProcessPoints();
        segments.ControlPoint = ConversionTools.Vector3ToVertex3(ProcessedPoint1,1);
        segments.Point2 = ConversionTools.Vector3ToVertex3(ProcessedPoint2, 0);
        segments.Point3 = ConversionTools.Vector3ToVertex3(ProcessedPoint3, 0);
        segments.Point4 = ConversionTools.Vector3ToVertex3(ProcessedPoint4, 0);
        segments.ScalingPoint = ConversionTools.Vector4ToVertex3(ScalingPoint);

        //Parent, Next Segment and Previous Segment Generated in Spline Object

        segments.LowestXYZ = ConversionTools.Vector3ToVertex3(Point1);
        segments.LowestXYZ = MathTools.Lowest(segments.LowestXYZ, Point2);
        segments.LowestXYZ = MathTools.Lowest(segments.LowestXYZ, Point3);
        segments.LowestXYZ = MathTools.Lowest(segments.LowestXYZ, Point4);

        segments.HighestXYZ = ConversionTools.Vector3ToVertex3(Point1);
        segments.HighestXYZ = MathTools.Highest(segments.HighestXYZ, Point2);
        segments.HighestXYZ = MathTools.Highest(segments.HighestXYZ, Point3);
        segments.HighestXYZ = MathTools.Highest(segments.HighestXYZ, Point4);

        segments.SegmentDisatnce = Vector3.Distance(Point4, Point1);

        //Preveous Segment Distance Generated in Spline

        segments.Unknown32 = Unknown32;

        return segments;
    }

    void GeneratePoints()
    {
        Point1 = ProcessedPoint1;
        Point2 = Point1 + ProcessedPoint2 / 3;
        Point3 = Point2 + (ProcessedPoint2 + ProcessedPoint3) / 3;
        Point4 = ProcessedPoint1 + ProcessedPoint2 + ProcessedPoint3 + ProcessedPoint4;
    }

    void ProcessPoints()
    {
        ProcessedPoint1 = Point1;
        ProcessedPoint2 = (Point2-Point1)*3;
        ProcessedPoint3 = (Point3 - Point2) * 3 - ProcessedPoint2;
        ProcessedPoint4 = Point4 - (ProcessedPoint1 + ProcessedPoint2 + ProcessedPoint3);
    }

    public void SetDataLineRender(bool UpdateCubePoints)
    {
        transform.position = Point1 * TrickyMapInterface.Scale;
        lineRenderer.positionCount = 4;
        lineRenderer.SetPosition(0, (Point1 - Point1) * TrickyMapInterface.Scale);
        lineRenderer.SetPosition(1, (Point2 - Point1) * TrickyMapInterface.Scale);
        lineRenderer.SetPosition(2, (Point3 - Point1) * TrickyMapInterface.Scale);
        lineRenderer.SetPosition(3, (Point4 - Point1) * TrickyMapInterface.Scale);
        lineRenderer.Simplify(0.1f);
        if (UpdateCubePoints)
        {
            for (int i = 0; i < PatchPoints.Count; i++)
            {
                PatchPoints[i].DisableUpdate = true;
            }
            PatchPoints[0].transform.position = Point1 * TrickyMapInterface.Scale;
            PatchPoints[1].transform.position = Point2 * TrickyMapInterface.Scale;
            PatchPoints[2].transform.position = Point3 * TrickyMapInterface.Scale;
            PatchPoints[3].transform.position = Point4 * TrickyMapInterface.Scale;
            for (int i = 0; i < PatchPoints.Count; i++)
            {
                PatchPoints[i].ResetOldPosition();
            }
            for (int i = 0; i < PatchPoints.Count; i++)
            {
                PatchPoints[i].DisableUpdate = false;
            }
        }
        RegenerateModel();
    }

    public void RegenerateModel()
    {
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, Camera.main);
        mesh.SetIndices(mesh.GetIndices(0).Concat(mesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void SpawnCube(Vector3 Point, Color color)
    {
        GameObject gameObject = Instantiate(PointPrefab, Point * TrickyMapInterface.Scale, new Quaternion(0, 0, 0, 0));
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        gameObject.transform.parent = transform;
        gameObject.GetComponent<PatchPoint>().ID = PatchPoints.Count;
        gameObject.GetComponent<PatchPoint>().unityEvent = UpdatePointUsingCube;
        PatchPoints.Add(gameObject.GetComponent<PatchPoint>());
    }

    public void GenerateCubePoints()
    {
        PatchPoints = new List<PatchPoint>();
        SpawnCube(Point1, new Color32(202, 202, 202, 255));
        SpawnCube(Point2, Color.red);
        SpawnCube(Point3, Color.green);
        SpawnCube(Point4, Color.blue);
    }

    void DestroyCube()
    {
        for (int i = 0; i < PatchPoints.Count; i++)
        {
            Destroy(PatchPoints[i].gameObject);
        }
        PatchPoints.Clear();
    }

    void UpdatePointUsingCube(int a)
    {
        Point1 = PatchPoints[0].transform.position / TrickyMapInterface.Scale;
        Point2 = PatchPoints[1].transform.position / TrickyMapInterface.Scale;
        Point3 = PatchPoints[2].transform.position / TrickyMapInterface.Scale;
        Point4 = PatchPoints[3].transform.position / TrickyMapInterface.Scale;
        SplinePanel.instance.LoadSegment(false);
        SetDataLineRender(true);
    }

    public void SelectedObject()
    {
        GenerateCubePoints();
    }

    public void UnSelectedObject()
    {
        DestroyCube();
    }

    void DrawCurve()
    {
        curveCount = (int)4 / 3;
        for (int j = 0; j < curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                Vector3 pixel = CalculateCubicBezierPoint(t, (Point1 - Point1) * TrickyMapInterface.Scale, (Point2 - Point1) * TrickyMapInterface.Scale, (Point3 - Point1) * TrickyMapInterface.Scale, (Point4 - Point1) * TrickyMapInterface.Scale);
                lineRenderer.positionCount = ((j * SEGMENT_COUNT) + i);
                lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
            }

        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    public Vector3 GetCentrePoint()
    {
        Vector3 vector3 = (Point1+Point2+Point3+Point4) / 4;
        return vector3;
    }
}
