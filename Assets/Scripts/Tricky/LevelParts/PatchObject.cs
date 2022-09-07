using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers.MapEditor;
using System.IO;

public class PatchObject : MonoBehaviour
{
    public string PatchName;
    public Vector4 ScalePoint; 
    [Space(10)]
    public Vector2 UVPoint1;
    public Vector2 UVPoint2;
    public Vector2 UVPoint3;
    public Vector2 UVPoint4;

    [Space(10)]
    public Vector3 RawControlPoint;
    public Vector3 RawR1C2;
    public Vector3 RawR1C3;
    public Vector3 RawR1C4;
    [Space(5)]
    public Vector3 RawR2C1;
    public Vector3 RawR2C2;
    public Vector3 RawR2C3;
    public Vector3 RawR2C4;
    [Space(5)]
    public Vector3 RawR3C1;
    public Vector3 RawR3C2;
    [Header("Due to calculation not Existing Point is blank")]
    public Vector3 RawR3C3;
    public Vector3 RawR3C4;
    [Space(5)]
    public Vector3 RawR4C1;
    public Vector3 RawR4C2;
    public Vector3 RawR4C3;
    public Vector3 RawR4C4;

    [Space(10)]
    public Vector3 LowestPoints;
    public Vector3 HighestPoints;

    [Space(10)]
    private Vector3 Point1;
    private Vector3 Point2;
    private Vector3 Point3;
    private Vector3 Point4;
    [Space(10)]
    public Vector3 OldPoint1;
    public Vector3 OldPoint2;
    public Vector3 OldPoint3;
    public Vector3 OldPoint4;

    [Space(10)]
    public int PatchStyle;
    public int Unknown2; //Lighting/Material
    public int TextureAssigment;
    public int LightmapID;
    public int Unknown4; 
    public int Unknown5; 
    public int Unknown6; 
    public Patch patch;
    [Space(10)]
    public Material MainMaterial;
    public Material NoLightMaterial;
    public Renderer Renderer;
    public GameObject PointPrefab;
    public List<PatchPoint> PatchPoints;

    [Space(10)]
    private Vector3 ControlPoint;
    private Vector3 R1C2;
    private Vector3 R1C3;
    private Vector3 R1C4;
    private Vector3 R2C1;
    private Vector3 R2C2;
    private Vector3 R2C3;
    private Vector3 R2C4;
    private Vector3 R3C1;
    private Vector3 R3C2;
    public  Vector3 R3C3;
    private Vector3 R3C4;
    private Vector3 R4C1;
    private Vector3 R4C2;
    private Vector3 R4C3;
    private Vector3 R4C4;

    Vector3 oldPosition;

    public void LoadPatch(Patch import, string NewName)
    {
        patch= import;
        PatchName = NewName;
        ScalePoint = new Vector4(import.ScalePoint.X, import.ScalePoint.Y, import.ScalePoint.Z, import.ScalePoint.W);
        UVPoint1 = new Vector2(import.UVPoint1.X, import.UVPoint1.Y);
        UVPoint2 = new Vector2(import.UVPoint2.X, import.UVPoint2.Y);
        UVPoint3 = new Vector2(import.UVPoint3.X, import.UVPoint3.Y);
        UVPoint4 = new Vector2(import.UVPoint4.X, import.UVPoint4.Y);

        R4C4 = ConversionTools.Vertex3ToVector3(import.R4C4);
        R4C3 = ConversionTools.Vertex3ToVector3(import.R4C3);
        R4C2 = ConversionTools.Vertex3ToVector3(import.R4C2);
        R4C1 = ConversionTools.Vertex3ToVector3(import.R4C1);
        R3C4 = ConversionTools.Vertex3ToVector3(import.R3C4);
        R3C3 = ConversionTools.Vertex3ToVector3(import.R3C3);
        R3C2 = ConversionTools.Vertex3ToVector3(import.R3C2); 
        R3C1 = ConversionTools.Vertex3ToVector3(import.R3C1);
        R2C4 = ConversionTools.Vertex3ToVector3(import.R2C4);
        R2C3 = ConversionTools.Vertex3ToVector3(import.R2C3);
        R2C2 = ConversionTools.Vertex3ToVector3(import.R2C2);
        R2C1 = ConversionTools.Vertex3ToVector3(import.R2C1);
        R1C4 = ConversionTools.Vertex3ToVector3(import.R1C4);
        R1C3 = ConversionTools.Vertex3ToVector3(import.R1C3);
        R1C2 = ConversionTools.Vertex3ToVector3(import.R1C2);
        ControlPoint = ConversionTools.Vertex3ToVector3(import.R1C1);

        LowestPoints = ConversionTools.Vertex3ToVector3(import.LowestXYZ);
        HighestPoints = ConversionTools.Vertex3ToVector3(import.HighestXYZ);

        Point1 = (ConversionTools.Vertex3ToVector3(import.Point1)- ControlPoint);
        Point2 = (ConversionTools.Vertex3ToVector3(import.Point2)- ControlPoint);
        Point3 = (ConversionTools.Vertex3ToVector3(import.Point3)- ControlPoint);
        Point4 = (ConversionTools.Vertex3ToVector3(import.Point4)- ControlPoint);
        OldPoint1 = ConversionTools.Vertex3ToVector3(import.Point1);
        OldPoint2 = ConversionTools.Vertex3ToVector3(import.Point2);
        OldPoint3 = ConversionTools.Vertex3ToVector3(import.Point3);
        OldPoint4 = ConversionTools.Vertex3ToVector3(import.Point4); 

        PatchStyle = import.PatchStyle;
        Unknown2 = import.Unknown2;
        TextureAssigment = import.TextureAssigment;
        LightmapID = import.LightmapID;
        Unknown4 = import.Unknown4;
        Unknown5 = import.Unknown5;
        Unknown6 = import.Unknown6;

        transform.position = ControlPoint * TrickyMapInterface.Scale;

        //LoadLowPolyMesh()
        GeneratePoints();
        LoadHighPolyMesh();
        oldPosition = transform.position;
    }

    public void GenerateCubePoints()
    {
        PatchPoints = new List<PatchPoint>();
        SpawnCube(RawControlPoint, new Color32(202, 202, 202, 255));
        SpawnCube(RawR1C2, Color.red);
        SpawnCube(RawR1C3, Color.green);
        SpawnCube(RawR1C4, Color.blue);
        SpawnCube(RawR2C1, Color.yellow);
        SpawnCube(RawR2C2, Color.grey);
        SpawnCube(RawR2C3, Color.cyan);
        SpawnCube(RawR2C4, Color.magenta);
        SpawnCube(RawR3C1, new Color32(0, 255, 85, 255));
        SpawnCube(RawR3C2, new Color32(216, 0, 255, 255));
        SpawnCube(RawR3C3, new Color32(185, 0, 255, 255));
        SpawnCube(RawR3C4, new Color32(0, 99, 119, 255));
        SpawnCube(RawR4C1, new Color32(0, 119, 49, 255));
        SpawnCube(RawR4C2, new Color32(119, 16, 0, 255));
        SpawnCube(RawR4C3, new Color32(211, 216, 45, 255));
        SpawnCube(RawR4C4, Color.black);
    }

    public Patch GeneratePatch()
    {
        ProccessPoints();
        Patch patch = new Patch();

        patch.ScalePoint = ConversionTools.Vector4ToVertex3(ScalePoint);

        patch.UVPoint1 = ConversionTools.Vector2ToVertex3(UVPoint1);
        patch.UVPoint2 = ConversionTools.Vector2ToVertex3(UVPoint2);
        patch.UVPoint3 = ConversionTools.Vector2ToVertex3(UVPoint3);
        patch.UVPoint4 = ConversionTools.Vector2ToVertex3(UVPoint4);

        patch.R1C1 = ConversionTools.Vector3ToVertex3(ControlPoint);
        patch.R1C2 = ConversionTools.Vector3ToVertex3(R1C2);
        patch.R1C3 = ConversionTools.Vector3ToVertex3(R1C3);
        patch.R1C4 = ConversionTools.Vector3ToVertex3(R1C4);
        patch.R2C1 = ConversionTools.Vector3ToVertex3(R2C1);
        patch.R2C2 = ConversionTools.Vector3ToVertex3(R2C2);
        patch.R2C3 = ConversionTools.Vector3ToVertex3(R2C3);
        patch.R2C4 = ConversionTools.Vector3ToVertex3(R2C4);
        patch.R3C1 = ConversionTools.Vector3ToVertex3(R3C1);
        patch.R3C2 = ConversionTools.Vector3ToVertex3(R3C2);
        patch.R3C3 = ConversionTools.Vector3ToVertex3(R3C3);
        patch.R3C4 = ConversionTools.Vector3ToVertex3(R3C4);
        patch.R4C1 = ConversionTools.Vector3ToVertex3(R4C1);
        patch.R4C2 = ConversionTools.Vector3ToVertex3(R4C2);
        patch.R4C3 = ConversionTools.Vector3ToVertex3(R4C3);
        patch.R4C4 = ConversionTools.Vector3ToVertex3(R4C4);

        Vertex3 HighestXYZ = ConversionTools.Vector3ToVertex3(RawControlPoint);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR1C2);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR1C3);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR1C4);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR2C1);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR2C2);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR2C3);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR2C4);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR3C1);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR3C2);
        //HighestXYZ = Highest(HighestXYZ, RawR3C3);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR3C4);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR4C1);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR4C2);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR4C3);
        HighestXYZ = MathTools.Highest(HighestXYZ, RawR4C4);

        Vertex3 LowestXYZ = ConversionTools.Vector3ToVertex3(RawControlPoint);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR1C2);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR1C3);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR1C4);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR2C1);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR2C2);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR2C3);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR2C4);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR3C1);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR3C2);
        //LowestXYZ = Lowest(LowestXYZ, RawR3C3);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR3C4);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR4C1);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR4C2);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR4C3);
        LowestXYZ = MathTools.Lowest(LowestXYZ, RawR4C4);

        patch.HighestXYZ = HighestXYZ;
        patch.LowestXYZ = LowestXYZ;

        patch.Point1 = ConversionTools.Vector3ToVertex3(RawControlPoint);
        patch.Point2 = ConversionTools.Vector3ToVertex3(RawR4C1);
        patch.Point3 = ConversionTools.Vector3ToVertex3(RawR1C4);
        patch.Point4 = ConversionTools.Vector3ToVertex3(RawR4C4);

        OldPoint1 = RawControlPoint;
        OldPoint2 = RawR4C1;
        OldPoint3 = RawR1C4;
        OldPoint4 = RawR4C4;

        patch.PatchStyle = PatchStyle;
        patch.Unknown2 = Unknown2;
        patch.TextureAssigment = TextureAssigment;
        patch.LightmapID = LightmapID;
        patch.Unknown4 = Unknown4;
        patch.Unknown5 = Unknown5;
        patch.Unknown6 = Unknown6;

        return patch;
    }

    void GeneratePoints()
    {
        //Build a 2d Array

        //Row 1
        RawControlPoint = ControlPoint;
        RawR1C2 = ControlPoint + R1C2 / 3;
        RawR1C3 = RawR1C2 + (R1C2 + R1C3) / 3;
        RawR1C4 = ControlPoint+ R1C2 + R1C3 + R1C4;

        //Row2
        RawR2C1 = ControlPoint + R2C1 / 3;
        RawR2C2 = ControlPoint + (R1C2 + R2C1 + R2C2 / 3) / 3;
        RawR2C3 = RawR2C2 + (R1C2 + R1C3+(R2C2+R2C3)/3) / 3;
        RawR2C4 = RawR1C4 + (R2C1+R2C2+R2C3+R2C4) / 3;

        //Row 3
        RawR3C1 = RawR2C1 + (R2C1 + R3C1) / 3;
        RawR3C2 = RawR2C2 + (R2C1 + R3C1 + (R2C2 + R3C2) / 3) / 3;
        //DUE TO THE MATH CALUCLATION TO WORK THIS OUT BEING UNKNOWN THIS ONE ISNT DONE
        //Vector3 NewPoint6 = (NewPoint7 + NewPoint11) / 2; 
        RawR3C4 = (R2C1 + R2C2 + R2C3 + R2C4 + R3C1 + R3C2 + R3C3 + R3C4) / 3 + RawR2C4;

        ////Row 4
        RawR4C1 = ControlPoint + R2C1 + R3C1 + R4C1;
        RawR4C2 = RawR4C1 + (R1C2 + R2C2 + R3C2 + R4C2) / 3;
        RawR4C3 = RawR4C2 + (R4C2 + R4C3 + R3C2 + R3C3 + R2C2 + R2C3 + R1C2 + R1C3) / 3;
        RawR4C4 = ControlPoint + R1C2 + R1C3 + R1C4 + R2C1 + R2C2 + R2C3 + R2C4 + R3C1 + R3C2 + R3C3 + R3C4 + R4C1 + R4C2 + R4C3 + R4C4;

    }

    public void ProccessPoints()
    {
        ControlPoint = RawControlPoint;
        R1C2 = (RawR1C2 - ControlPoint) * 3;
        R1C3 = (RawR1C3 - RawR1C2) * 3 - R1C2;
        R1C4 = RawR1C4 - (ControlPoint + R1C2 + R1C3);

        R2C1 = (RawR2C1 - ControlPoint) * 3;
        R2C2 = RawR2C2 * 9 - ControlPoint * 9 - R1C2 * 3 - R2C1 * 3;
        R2C3 = RawR2C3 * 9 - RawR2C2 * 9 - R1C2 * 3 - R1C3 * 3 - R2C2;
        R2C4 = RawR2C4 * 3 - RawR1C4 * 3 - R2C1 - R2C2 - R2C3;

        R3C1 = (RawR3C1 - RawR2C1) * 3 - R2C1;
        R3C2 = RawR3C2 * 9 - RawR2C2 * 9 - R2C1 * 3 - R3C1 * 3 - R2C2;
        //R3C3 = 0;
        R3C4 = RawR3C4 * 3 - RawR2C4 * 3 - R2C1 - R2C2 - R2C3 - R2C4 - R3C1 - R3C2 - R3C3;

        R4C1 = RawR4C1 - (ControlPoint + R2C1 + R3C1);
        R4C2 = RawR4C2 * 3 - RawR4C1 * 3 - R1C2 - R2C2 - R3C2;
        R4C3 = RawR4C3 * 3 - RawR4C2 * 3 - R1C2 - R2C2 - R3C2 - R4C2 - R1C3 - R2C3 - R3C3;
        R4C4 = RawR4C4 - (ControlPoint + R1C2 + R1C3 + R1C4 + R2C1 + R2C2 + R2C3 + R2C4 + R3C1 + R3C2 + R3C3 + R3C4 + R4C1 + R4C2 + R4C3);
    }

    void SpawnCube(Vector3 Point, Color color)
    {
        GameObject gameObject = Instantiate(PointPrefab, Point * TrickyMapInterface.Scale, new Quaternion(0, 0, 0, 0));
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        gameObject.transform.parent = transform;
        gameObject.GetComponent<PatchPoint>().ID = PatchPoints.Count;
        gameObject.GetComponent<PatchPoint>().PatchObject = this;
        gameObject.GetComponent<PatchPoint>().unityEvent = UpdatePointUsingCube;
        PatchPoints.Add(gameObject.GetComponent<PatchPoint>());
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
        RawControlPoint = PatchPoints[0].transform.position / TrickyMapInterface.Scale;
        RawR1C2 = PatchPoints[1].transform.position / TrickyMapInterface.Scale;
        RawR1C3 = PatchPoints[2].transform.position / TrickyMapInterface.Scale;
        RawR1C4 = PatchPoints[3].transform.position / TrickyMapInterface.Scale;
        RawR2C1 = PatchPoints[4].transform.position / TrickyMapInterface.Scale;
        RawR2C2 = PatchPoints[5].transform.position / TrickyMapInterface.Scale;
        RawR2C3 = PatchPoints[6].transform.position / TrickyMapInterface.Scale;
        RawR2C4 = PatchPoints[7].transform.position / TrickyMapInterface.Scale;
        RawR3C1 = PatchPoints[8].transform.position / TrickyMapInterface.Scale;
        RawR3C2 = PatchPoints[9].transform.position / TrickyMapInterface.Scale;
        RawR3C3 = PatchPoints[10].transform.position / TrickyMapInterface.Scale;
        RawR3C4 = PatchPoints[11].transform.position / TrickyMapInterface.Scale;
        RawR4C1 = PatchPoints[12].transform.position / TrickyMapInterface.Scale;
        RawR4C2 = PatchPoints[13].transform.position / TrickyMapInterface.Scale;
        RawR4C3 = PatchPoints[14].transform.position / TrickyMapInterface.Scale;
        RawR4C4 = PatchPoints[15].transform.position / TrickyMapInterface.Scale;
        UpdateMeshPoints(true);
    }


    void SpawnPoints(Vector3 vector3, string Name)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = vector3;
        gameObject.transform.parent = this.gameObject.transform;
        gameObject.name = Name;
        gameObject.AddComponent<CubeID>();
    }

    public void LoadHighPolyMesh()
    {
        List<int> ints = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> UV = new List<Vector2>();
        Mesh mesh = new Mesh();

        //Vertices
        vertices.Add((RawControlPoint - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR1C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR1C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR1C4 - RawControlPoint) * TrickyMapInterface.Scale);

        vertices.Add((RawR2C1 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR2C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR2C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR2C4 - RawControlPoint) * TrickyMapInterface.Scale);

        vertices.Add((RawR3C1 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR3C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR3C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR3C4 - RawControlPoint) * TrickyMapInterface.Scale);

        vertices.Add((RawR4C1 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR4C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR4C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR4C4 - RawControlPoint) * TrickyMapInterface.Scale);

        //UV Points

        Vector2 Point13Dif = (UVPoint3 - UVPoint1);
        Vector2 Point24Dif = (UVPoint4 - UVPoint2);
        Vector2 Point12Dif = (UVPoint2 - UVPoint1);
        Vector2 Point34Dif = (UVPoint4 - UVPoint3);

        Vector2 Point11 = (UVPoint1 + Point13Dif * (1f / 3f));
        Vector2 Point1131Dif = (UVPoint2 + Point24Dif * (1f / 3f) - (UVPoint1 + Point13Dif * (1f / 3f)));
        Vector2 Point12 = (UVPoint1 + Point13Dif * (2f / 3f));
        Vector2 Point1232Dif = (UVPoint2 + Point24Dif * (2f / 3f) - (UVPoint1 + Point13Dif * (2f / 3f)));

        UV.Add(UVPoint1);
        UV.Add(UVPoint1 + Point13Dif * (1f / 3f));
        UV.Add(UVPoint1 + Point13Dif * (2f / 3f));
        UV.Add(UVPoint3);

        UV.Add(UVPoint1 + Point12Dif * (1f / 3f));
        UV.Add(Point11 + Point1131Dif * (1f / 3f));
        UV.Add(Point12 + Point1232Dif * (1f / 3f));
        UV.Add(UVPoint3 + Point34Dif * (1f / 3f));

        UV.Add(UVPoint1 + Point12Dif * (2f / 3f));
        UV.Add(Point11 + Point1131Dif * (2f / 3f));
        UV.Add(Point12 + Point1232Dif * (2f / 3f));
        UV.Add(UVPoint3 + Point34Dif * (2f / 3f));

        UV.Add(UVPoint2);
        UV.Add(UVPoint2 + Point24Dif * (1f / 3f));
        UV.Add(UVPoint2 + Point24Dif * (2f / 3f));
        UV.Add(UVPoint4);

        for (int i = 0; i < UV.Count; i++)
        {
            UV[i] = PointCorrection(UV[i]);
        }

        //Faces
        //Working
        ints.Add(0);
        ints.Add(1);
        ints.Add(4);

        //Working
        ints.Add(1);
        ints.Add(5);
        ints.Add(4);

        //Working
        ints.Add(1);
        ints.Add(2);
        ints.Add(5);

        //Working
        ints.Add(5);
        ints.Add(2);
        ints.Add(6);

        //Working
        ints.Add(2);
        ints.Add(3);
        ints.Add(6);

        //Working
        ints.Add(3);
        ints.Add(7);
        ints.Add(6);

        //Working
        ints.Add(4);
        ints.Add(5);
        ints.Add(8);

        //Working
        ints.Add(8);
        ints.Add(5);
        ints.Add(9);

        //Working
        ints.Add(5);
        ints.Add(6);
        ints.Add(9);

        ////Comment Out
        //ints.Add(6);
        //ints.Add(9);
        //ints.Add(10);

        //ints.Add(6);
        //ints.Add(7);
        //ints.Add(10);

        //ints.Add(10);
        //ints.Add(7);
        //ints.Add(11);
        ////

        //Working
        ints.Add(8);
        ints.Add(9);
        ints.Add(12);

        //Working
        ints.Add(12);
        ints.Add(9);
        ints.Add(13);

        ////Comment out
        //ints.Add(9);
        //ints.Add(10);
        //ints.Add(13);

        //ints.Add(13);
        //ints.Add(10);
        //ints.Add(14);

        //ints.Add(10);
        //ints.Add(11);
        //ints.Add(14);
        /////

        //Working
        ints.Add(15);
        ints.Add(14);
        ints.Add(11);

        //Used Instead because of missing calculation

        //Working
        ints.Add(9);
        ints.Add(6);
        ints.Add(14);

        //Working
        ints.Add(14);
        ints.Add(6);
        ints.Add(11);

        //Working
        ints.Add(6);
        ints.Add(7);
        ints.Add(11);

        //Working
        ints.Add(13);
        ints.Add(9);
        ints.Add(14);

        //Set Mesh Data
        mesh.vertices = vertices.ToArray();
        mesh.triangles = ints.ToArray();
        mesh.uv = UV.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;

        //Set material
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<Renderer>().material = MainMaterial;
        UpdateTexture(TextureAssigment);
        ToggleLightingMode();
    }


    public Vector2 PointCorrection(Vector2 Newpoint)
    {
        if (Newpoint.y < 0)
        {
            Newpoint.y = -Newpoint.y;
        }
        if (Newpoint.x < 0)
        {
            Newpoint.x = -Newpoint.x;
        }
        return Newpoint;
    }

    public void UpdateMeshPoints(bool CubePointUpdate)
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add((RawControlPoint - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR1C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR1C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR1C4 - RawControlPoint) * TrickyMapInterface.Scale);

        vertices.Add((RawR2C1 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR2C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR2C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR2C4 - RawControlPoint) * TrickyMapInterface.Scale);

        vertices.Add((RawR3C1 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR3C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR3C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR3C4 - RawControlPoint) * TrickyMapInterface.Scale);

        vertices.Add((RawR4C1 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR4C2 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR4C3 - RawControlPoint) * TrickyMapInterface.Scale);
        vertices.Add((RawR4C4 - RawControlPoint) * TrickyMapInterface.Scale);

        mesh.vertices = vertices.ToArray();
        if(CubePointUpdate)
        {
            for (int i = 0; i < PatchPoints.Count; i++)
            {
                PatchPoints[i].DisableUpdate = true;
            }
            transform.position = RawControlPoint * TrickyMapInterface.Scale;
            PatchPoints[0].transform.position = RawControlPoint * TrickyMapInterface.Scale;
            PatchPoints[1].transform.position = RawR1C2 * TrickyMapInterface.Scale;
            PatchPoints[2].transform.position = RawR1C3 * TrickyMapInterface.Scale;
            PatchPoints[3].transform.position = RawR1C4 * TrickyMapInterface.Scale;
            PatchPoints[4].transform.position = RawR2C1 * TrickyMapInterface.Scale;
            PatchPoints[5].transform.position = RawR2C2 * TrickyMapInterface.Scale;
            PatchPoints[6].transform.position = RawR2C3 * TrickyMapInterface.Scale;
            PatchPoints[7].transform.position = RawR2C4 * TrickyMapInterface.Scale;
            PatchPoints[8].transform.position = RawR3C1 * TrickyMapInterface.Scale;
            PatchPoints[9].transform.position = RawR3C2 * TrickyMapInterface.Scale;
            PatchPoints[10].transform.position = RawR3C3 * TrickyMapInterface.Scale;
            PatchPoints[11].transform.position = RawR3C4 * TrickyMapInterface.Scale;
            PatchPoints[12].transform.position = RawR4C1 * TrickyMapInterface.Scale;
            PatchPoints[13].transform.position = RawR4C2 * TrickyMapInterface.Scale;
            PatchPoints[14].transform.position = RawR4C3 * TrickyMapInterface.Scale;
            PatchPoints[15].transform.position = RawR4C4 * TrickyMapInterface.Scale;
            for (int i = 0; i < PatchPoints.Count; i++)
            {
                PatchPoints[i].ResetOldPosition();
            }
            for (int i = 0; i < PatchPoints.Count; i++)
            {
                PatchPoints[i].DisableUpdate = false;
            }
        }
        oldPosition = transform.position;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().enabled = true;
        if(PatchPanel.instance.patchObject==this)
        {
            PatchPanel.instance.UpdatePoint(false);
        }
    }

    public void UpdateUVPoints()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        //UV Points
        Vector2 Point13Dif = (UVPoint3 - UVPoint1);
        Vector2 Point24Dif = (UVPoint4 - UVPoint2);
        Vector2 Point12Dif = (UVPoint2 - UVPoint1);
        Vector2 Point34Dif = (UVPoint4 - UVPoint3);

        Vector2 Point11 = (UVPoint1 + Point13Dif * (1f / 3f));
        Vector2 Point1131Dif = (UVPoint2 + Point24Dif * (1f / 3f) - (UVPoint1 + Point13Dif * (1f / 3f)));
        Vector2 Point12 = (UVPoint1 + Point13Dif * (2f / 3f));
        Vector2 Point1232Dif = (UVPoint2 + Point24Dif * (2f / 3f) - (UVPoint1 + Point13Dif * (2f / 3f)));

        List<Vector2> NewUV = new List<Vector2>();

        NewUV.Add(UVPoint1);
        NewUV.Add(UVPoint1 + Point13Dif * (1f / 3f));
        NewUV.Add(UVPoint1 + Point13Dif * (2f / 3f));
        NewUV.Add(UVPoint3);

        NewUV.Add(UVPoint1 + Point12Dif * (1f / 3f));
        NewUV.Add(Point11 + Point1131Dif * (1f / 3f));
        NewUV.Add(Point12 + Point1232Dif * (1f / 3f));
        NewUV.Add(UVPoint3 + Point34Dif * (1f / 3f));

        NewUV.Add(UVPoint1 + Point12Dif * (2f / 3f));
        NewUV.Add(Point11 + Point1131Dif * (2f / 3f));
        NewUV.Add(Point12 + Point1232Dif * (2f / 3f));
        NewUV.Add(UVPoint3 + Point34Dif * (2f / 3f));

        NewUV.Add(UVPoint2);
        NewUV.Add(UVPoint2 + Point24Dif * (1f / 3f));
        NewUV.Add(UVPoint2 + Point24Dif * (2f / 3f));
        NewUV.Add(UVPoint4);

        for (int i = 0; i < NewUV.Count; i++)
        {
            NewUV[i] = PointCorrection(NewUV[i]);
        }
        mesh.uv = NewUV.ToArray();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void ToggleLightingMode()
    {
        if(!TrickyMapInterface.Instance.NoLightMode)
        {
            LightMode();
        }
        else
        {
            NoLightMode();
        }
        UpdateTexture(TextureAssigment);
    }

    public void SelectedObject()
    {
        if (!TrickyMapInterface.Instance.NoLightMode)
        {
            Renderer.material.EnableKeyword("_EMISSION");
        }
        GenerateCubePoints();
        Renderer.material.SetColor("_EmissionColor", Color.grey);
    }

    public void UnSelectedObject()
    {
        if (!TrickyMapInterface.Instance.NoLightMode)
        {
            Renderer.material.DisableKeyword("_EMISSION");
        }
        DestroyCube();
        Renderer.material.SetColor("_EmissionColor", Color.white);
    }

    public void LightMode()
    {
        Renderer.material = MainMaterial;
    }

    public void NoLightMode()
    {
        NoLightMaterial.SetTexture("_EmissionMap", TrickyMapInterface.Instance.textures[TextureAssigment]);
        Renderer.material = NoLightMaterial;
    }

    public void UpdateTexture(int a)
    {
        try
        {
            Renderer.material.mainTexture = TrickyMapInterface.Instance.textures[a];
            TextureAssigment = a;
            Renderer.material.SetTexture("_EmissionMap", TrickyMapInterface.Instance.textures[TextureAssigment]);
        }
        catch
        {
            Renderer.material.mainTexture = TrickyMapInterface.Instance.ErrorTexture;
        }
    }

    void LoadLowPolyMesh()
    {
        List<int> ints = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> UV = new List<Vector2>();
        Mesh mesh = new Mesh();

        //Face 1

        if (!vertices.Contains(Point3))
        {
            vertices.Add(Point3);
            UV.Add(UVPoint1);
        }
        ints.Add(vertices.IndexOf(Point3));

        if (!vertices.Contains(Point2))
        {
            vertices.Add(Point2);
            UV.Add(UVPoint4);
        }
        ints.Add(vertices.IndexOf(Point2));

        if (!vertices.Contains(Point1))
        {
            vertices.Add(Point1);
            UV.Add(UVPoint3);
        }
        ints.Add(vertices.IndexOf(Point1));

        ////Face 2

        if (!vertices.Contains(Point4))
        {
            vertices.Add(Point4);
            UV.Add(UVPoint2);
        }
        ints.Add(vertices.IndexOf(Point4));

        if (!vertices.Contains(Point2))
        {
            vertices.Add(Point2);
            UV.Add(UVPoint4);
        }
        ints.Add(vertices.IndexOf(Point2));

        if (!vertices.Contains(Point3))
        {
            vertices.Add(Point3);
            UV.Add(UVPoint1);
        }
        ints.Add(vertices.IndexOf(Point3));

        mesh.vertices = vertices.ToArray();
        mesh.triangles = ints.ToArray();
        mesh.uv = UV.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;

        ToggleLightingMode();
    }

    void Update()
    {
        if (transform.position != oldPosition)
        {
            Vector3 Dif = (transform.position - oldPosition) / TrickyMapInterface.Scale;
            RawControlPoint += Dif;
            RawR1C2 += Dif;
            RawR1C3 += Dif;
            RawR1C4 += Dif;
            RawR2C1 += Dif;
            RawR2C2 += Dif;
            RawR2C3 += Dif;
            RawR2C4 += Dif;
            RawR3C1 += Dif;
            RawR3C2 += Dif;
            RawR3C3 += Dif;
            RawR3C4 += Dif;
            RawR4C1 += Dif;
            RawR4C2 += Dif;
            RawR4C3 += Dif;
            RawR4C4 += Dif;
            UpdateMeshPoints(true);
        }
    }

    public Vector3 GetCentrePoint()
    {
        Vector3 vector3 = (RawControlPoint + RawR1C2 + RawR1C3 + RawR1C4 + RawR2C1 + RawR2C2 + RawR2C3 + RawR2C4 + RawR3C1 + RawR3C2 /*+ RawR3C3*/ + RawR3C4 + RawR4C1 + RawR4C2 +RawR4C3+RawR4C4)/15;
        return vector3;
    }
}
