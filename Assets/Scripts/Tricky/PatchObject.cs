using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers.MapEditor;
using System.IO;

public class PatchObject : MonoBehaviour
{
    public string PatchName;
    public Vector4 ScalePoint; //Possition?
    public Vector3 NormalisedScalePoint;
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

    public Material MainMaterial;
    public Material NoLightMaterial;
    public Renderer Renderer;

    [Space(10)]
    public Vector3 ControlPoint;
    public Vector3 R1C2;
    public Vector3 R1C3;
    public Vector3 R1C4;
    public Vector3 R2C1;
    public Vector3 R2C2;
    public Vector3 R2C3;
    public Vector3 R2C4;
    public Vector3 R3C1;
    public Vector3 R3C2;
    public Vector3 R3C3;
    public Vector3 R3C4;
    public Vector3 R4C1;
    public Vector3 R4C2;
    public Vector3 R4C3;
    public Vector3 R4C4;

    public void LoadPatch(Patch import, string NewName)
    {
        patch= import;
        PatchName = NewName;
        ScalePoint = new Vector4(import.ScalePoint.X, import.ScalePoint.Y, import.ScalePoint.Z, import.ScalePoint.W);
        NormalisedScalePoint = new Vector3(import.ScalePoint.X/ import.ScalePoint.W, import.ScalePoint.Y/ import.ScalePoint.W, import.ScalePoint.Z/ import.ScalePoint.W);
        UVPoint1 = new Vector2(import.UVPoint1.X, import.UVPoint1.Y);
        UVPoint2 = new Vector2(import.UVPoint2.X, import.UVPoint2.Y);
        UVPoint3 = new Vector2(import.UVPoint3.X, import.UVPoint3.Y);
        UVPoint4 = new Vector2(import.UVPoint4.X, import.UVPoint4.Y);

        R4C4 = Vertex3ToVector3(import.R4C4);
        R4C3 = Vertex3ToVector3(import.R4C3);
        R4C2 = Vertex3ToVector3(import.R4C2);
        R4C1 = Vertex3ToVector3(import.R4C1);
        R3C4 = Vertex3ToVector3(import.R3C4);
        R3C3 = Vertex3ToVector3(import.R3C3);
        R3C2 = Vertex3ToVector3(import.R3C2); 
        R3C1 = Vertex3ToVector3(import.R3C1);
        R2C4 = Vertex3ToVector3(import.R2C4);
        R2C3 = Vertex3ToVector3(import.R2C3);
        R2C2 = Vertex3ToVector3(import.R2C2);
        R2C1 = Vertex3ToVector3(import.R2C1);
        R1C4 = Vertex3ToVector3(import.R1C4);
        R1C3 = Vertex3ToVector3(import.R1C3);
        R1C2 = Vertex3ToVector3(import.R1C2);
        ControlPoint = Vertex3ToVector3(import.R1C1);

        LowestPoints = Vertex3ToVector3(import.LowestXYZ);
        HighestPoints = Vertex3ToVector3(import.HighestXYZ);

        Point1 = (Vertex3ToVector3(import.Point1)- ControlPoint);
        Point2 = (Vertex3ToVector3(import.Point2)- ControlPoint);
        Point3 = (Vertex3ToVector3(import.Point3)- ControlPoint);
        Point4 = (Vertex3ToVector3(import.Point4)- ControlPoint);
        OldPoint1 = Vertex3ToVector3(import.Point1);
        OldPoint2 = Vertex3ToVector3(import.Point2);
        OldPoint3 = Vertex3ToVector3(import.Point3);
        OldPoint4 = Vertex3ToVector3(import.Point4); 

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
    }

    public Patch GeneratePatch()
    {
        Patch patch = new Patch();

        patch.ScalePoint = Vector4ToVertex3(ScalePoint);

        patch.UVPoint1 = Vector2ToVertex3(UVPoint1);
        patch.UVPoint2 = Vector2ToVertex3(UVPoint2);
        patch.UVPoint3 = Vector2ToVertex3(UVPoint3);
        patch.UVPoint4 = Vector2ToVertex3(UVPoint4);

        patch.R1C1 = Vector3ToVertex3(ControlPoint);
        patch.R1C2 = Vector3ToVertex3(R1C2);
        patch.R1C3 = Vector3ToVertex3(R1C3);
        patch.R1C4 = Vector3ToVertex3(R1C4);
        patch.R2C1 = Vector3ToVertex3(R2C1);
        patch.R2C2 = Vector3ToVertex3(R2C2);
        patch.R2C3 = Vector3ToVertex3(R2C3);
        patch.R2C4 = Vector3ToVertex3(R2C4);
        patch.R3C1 = Vector3ToVertex3(R3C1);
        patch.R3C2 = Vector3ToVertex3(R3C2);
        patch.R3C3 = Vector3ToVertex3(R3C3);
        patch.R3C4 = Vector3ToVertex3(R3C4);
        patch.R4C1 = Vector3ToVertex3(R4C1);
        patch.R4C2 = Vector3ToVertex3(R4C2);
        patch.R4C3 = Vector3ToVertex3(R4C3);
        patch.R4C4 = Vector3ToVertex3(R4C4);

        Vertex3 HighestXYZ = new Vertex3();
        Vertex3 LowestXYZ = new Vertex3();

        HighestXYZ = Vector3ToVertex3(RawControlPoint);
        HighestXYZ = Highest(HighestXYZ, RawR1C2);
        HighestXYZ = Highest(HighestXYZ, RawR1C3);
        HighestXYZ = Highest(HighestXYZ, RawR1C4);
        HighestXYZ = Highest(HighestXYZ, RawR2C1);
        HighestXYZ = Highest(HighestXYZ, RawR2C2);
        HighestXYZ = Highest(HighestXYZ, RawR2C3);
        HighestXYZ = Highest(HighestXYZ, RawR2C4);
        HighestXYZ = Highest(HighestXYZ, RawR3C1);
        HighestXYZ = Highest(HighestXYZ, RawR3C2);
        //HighestXYZ = Highest(HighestXYZ, RawR3C3);
        HighestXYZ = Highest(HighestXYZ, RawR3C4);
        HighestXYZ = Highest(HighestXYZ, RawR4C1);
        HighestXYZ = Highest(HighestXYZ, RawR4C2);
        HighestXYZ = Highest(HighestXYZ, RawR4C3);
        HighestXYZ = Highest(HighestXYZ, RawR4C4);

        LowestXYZ = Vector3ToVertex3(RawControlPoint);
        LowestXYZ = Lowest(LowestXYZ, RawR1C2);
        LowestXYZ = Lowest(LowestXYZ, RawR1C3);
        LowestXYZ = Lowest(LowestXYZ, RawR1C4);
        LowestXYZ = Lowest(LowestXYZ, RawR2C1);
        LowestXYZ = Lowest(LowestXYZ, RawR2C2);
        LowestXYZ = Lowest(LowestXYZ, RawR2C3);
        LowestXYZ = Lowest(LowestXYZ, RawR2C4);
        LowestXYZ = Lowest(LowestXYZ, RawR3C1);
        LowestXYZ = Lowest(LowestXYZ, RawR3C2);
        //LowestXYZ = Lowest(LowestXYZ, RawR3C3);
        LowestXYZ = Lowest(LowestXYZ, RawR3C4);
        LowestXYZ = Lowest(LowestXYZ, RawR4C1);
        LowestXYZ = Lowest(LowestXYZ, RawR4C2);
        LowestXYZ = Lowest(LowestXYZ, RawR4C3);
        LowestXYZ = Lowest(LowestXYZ, RawR4C4);

        patch.HighestXYZ = HighestXYZ;
        patch.LowestXYZ = LowestXYZ;

        patch.Point1 = Vector3ToVertex3(RawControlPoint);
        patch.Point2 = Vector3ToVertex3(RawR4C1);
        patch.Point3 = Vector3ToVertex3(RawR1C4);
        patch.Point4 = Vector3ToVertex3(RawR4C4);

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
    Vertex3 Highest(Vertex3 current,Vector3 vector3)
    {
        Vertex3 vertex = Vector3ToVertex3(vector3);
        if(vertex.X > current.X)
        {
            current.X = vertex.X;
        }
        if (vertex.Y > current.Y)
        {
            current.Y = vertex.Y;
        }
        if (vertex.Z > current.Z)
        {
            current.Z = vertex.Z;
        }
        return current;
    }

    Vertex3 Lowest(Vertex3 current, Vector3 vector3)
    {
        Vertex3 vertex = Vector3ToVertex3(vector3);
        if (vertex.X < current.X)
        {
            current.X = vertex.X;
        }
        if (vertex.Y < current.Y)
        {
            current.Y = vertex.Y;
        }
        if (vertex.Z < current.Z)
        {
            current.Z = vertex.Z;
        }
        return current;
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

    void SpawnMainPoints()
    {
        SpawnPoints(RawControlPoint, "Point C");
        SpawnPoints(RawR1C2, "R1C2");
        SpawnPoints(RawR1C3, "R1C3");
        SpawnPoints(RawR1C4, "R1C4");
        SpawnPoints(RawR2C1, "R2C1");
        SpawnPoints(RawR2C2, "R2C2");
        SpawnPoints(RawR2C3, "R2C3");
        SpawnPoints(RawR2C4, "R2C4");
        SpawnPoints(RawR3C1, "R3C1");
        SpawnPoints(RawR3C2, "R3C2");
        //SpawnPoints(NewPoint6, "Guessed R3C3");
        SpawnPoints(RawR3C4, "R3C4");
        SpawnPoints(RawR4C1, "R4C1");
        SpawnPoints(RawR4C2, "R4C2");
        SpawnPoints(RawR4C3, "R4C3");
        SpawnPoints(RawR4C4, "R4C4");
    }

    public Vector3 ScaleVector(Vector3 vector)
    {
        vector = new Vector3(vector.x/NormalisedScalePoint.x, vector.y / NormalisedScalePoint.y, vector.z / NormalisedScalePoint.z);
        return vector;
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

    public void UpdateMeshPoints()
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
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().enabled = true;
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

        Renderer.material.SetColor("_EmissionColor", Color.grey);
    }

    public void UnSelectedObject()
    {
        if (!TrickyMapInterface.Instance.NoLightMode)
        {
            Renderer.material.DisableKeyword("_EMISSION");
        }

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

    public void UpdatePatchStyle(int a)
    {
        PatchStyle = a;
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

    Vector3 Vertex3ToVector3(Vertex3 vertex3)
    {
        return new Vector3(vertex3.X, vertex3.Z, vertex3.Y);
    }

    Vertex3 Vector2ToVertex3(Vector2 vector2)
    {
        Vertex3 vertex3 = new Vertex3();
        vertex3.X = vector2.x;
        vertex3.Y = vector2.y;
        vertex3.Z = 1f;
        vertex3.W = 1f;
        return vertex3;
    }

    Vertex3 Vector3ToVertex3(Vector3 vector3)
    {
        Vertex3 vertex3 = new Vertex3();
        vertex3.X = vector3.x;
        vertex3.Y = vector3.z;
        vertex3.Z = vector3.y;
        vertex3.W = 1f;
        return vertex3;
    }

    Vertex3 Vector4ToVertex3(Vector4 vector4)
    {
        Vertex3 vertex3 = new Vertex3();
        vertex3.X = vector4.x;
        vertex3.Y = vector4.y;
        vertex3.Z = vector4.z;
        vertex3.W = vector4.w;
        return vertex3;
    }
}
