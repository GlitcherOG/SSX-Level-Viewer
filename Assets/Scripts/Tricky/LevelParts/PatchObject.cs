using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

public class PatchObject : MonoBehaviour
{
    NURBS.Surface surface;

    public string PatchName;
    public Vector4 LightMapPoint; 
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
    public Vector3 RawR3C3;
    public Vector3 RawR3C4;
    [Space(5)]
    public Vector3 RawR4C1;
    public Vector3 RawR4C2;
    public Vector3 RawR4C3;
    public Vector3 RawR4C4;

    [Space(10)]
    public int PatchStyle;
    public int Unknown2; //Lighting/Material
    public int TextureAssigment;
    public int LightmapID;
    public int Unknown4; 
    public int Unknown5; 
    public int Unknown6; 

    [Space(10)]
    public Material MainMaterial;
    public Renderer Renderer;
    public GameObject PointPrefab;
    public List<PatchPoint> PatchPoints;

    Vector3 oldPosition;

    public void LoadPatch(PatchesJsonHandler.PatchJson import)
    {
        PatchName = import.PatchName;
        LightMapPoint = JsonUtil.ArrayToVector4(import.LightMapPoint);

        UVPoint1 = JsonUtil.ArrayToVector4(import.UVPoint1);
        UVPoint2 = JsonUtil.ArrayToVector4(import.UVPoint2);
        UVPoint3 = JsonUtil.ArrayToVector4(import.UVPoint3);
        UVPoint4 = JsonUtil.ArrayToVector4(import.UVPoint4);

        RawR4C4 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R4C4));
        RawR4C3 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R4C3));
        RawR4C2 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R4C2));
        RawR4C1 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R4C1));
        RawR3C4 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R3C4));
        RawR3C3 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R3C3));
        RawR3C2 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R3C2));
        RawR3C1 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R3C1));
        RawR2C4 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R2C4));
        RawR2C3 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R2C3));
        RawR2C2 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R2C2));
        RawR2C1 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R2C1));
        RawR1C4 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R1C4));
        RawR1C3 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R1C3));
        RawR1C2 = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R1C2));
        RawControlPoint = MathTools.FixYandZ(JsonUtil.ArrayToVector3(import.R1C1));


        //float tempx = Random.Range(-1000f, 1000f);
        //float tempy = Random.Range(-1000f, 1000f);
        //float tempz = Random.Range(-1000f, 1000f);
        //Debug.Log(tempy);
        //RawR3C3 += new Vector3(tempx, tempy, tempz);

        // tempx = Random.Range(-1000f, 1000f);
        // tempy = Random.Range(-1000f, 1000f);
        // tempz = Random.Range(-1000f, 1000f);
        //RawR3C2 += new Vector3(tempx, tempy, tempz);

        // tempx = Random.Range(-1000f, 1000f);
        // tempy = Random.Range(-1000f, 1000f);
        // tempz = Random.Range(-1000f, 1000f);
        //RawR2C3 += new Vector3(tempx, tempy, tempz);

        // tempx = Random.Range(-1000f, 1000f);
        // tempy = Random.Range(-1000f, 1000f);
        // tempz = Random.Range(-1000f, 1000f);
        //RawR2C2 += new Vector3(tempx, tempy, tempz);

        PatchStyle = import.PatchStyle;
        Unknown2 = import.Unknown2;
        TextureAssigment = import.TextureAssigment;
        LightmapID = import.LightmapID;
        Unknown4 = import.Unknown4;
        Unknown5 = import.Unknown5;
        Unknown6 = import.Unknown6;

        transform.position = RawControlPoint * TrickyMapInterface.Scale;

        LoadNURBSpatch();
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

    public PatchesJsonHandler.PatchJson GeneratePatch()
    {
        PatchesJsonHandler.PatchJson patch = new PatchesJsonHandler.PatchJson();
        patch.PatchName = PatchName;
        patch.LightMapPoint = JsonUtil.Vector4ToArray(LightMapPoint);

        patch.UVPoint1 = JsonUtil.Vector4ToArray(JsonUtil.Vector2ToVector4(UVPoint1,1,1));
        patch.UVPoint2 = JsonUtil.Vector4ToArray(JsonUtil.Vector2ToVector4(UVPoint2, 1, 1));
        patch.UVPoint3 = JsonUtil.Vector4ToArray(JsonUtil.Vector2ToVector4(UVPoint3, 1, 1));
        patch.UVPoint4 = JsonUtil.Vector4ToArray(JsonUtil.Vector2ToVector4(UVPoint4, 1, 1));

        patch.R1C1 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawControlPoint));
        patch.R1C2 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR1C2));
        patch.R1C3 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR1C3));
        patch.R1C4 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR1C4));
        patch.R2C1 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR2C1));
        patch.R2C2 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR2C2));
        patch.R2C3 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR2C3));
        patch.R2C4 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR2C4));
        patch.R3C1 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR3C1));
        patch.R3C2 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR3C2));
        patch.R3C3 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR3C3));
        patch.R3C4 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR3C4));
        patch.R4C1 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR4C1));
        patch.R4C2 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR4C2));
        patch.R4C3 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR4C3));
        patch.R4C4 = JsonUtil.Vector3ToArray(MathTools.FixYandZ(RawR4C4));

        patch.PatchStyle = PatchStyle;
        patch.Unknown2 = Unknown2;
        patch.TextureAssigment = TextureAssigment;
        patch.LightmapID = LightmapID;
        patch.Unknown4 = Unknown4;
        patch.Unknown5 = Unknown5;
        patch.Unknown6 = Unknown6;

        return patch;
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

    public void LoadNURBSpatch()
    {
        Vector3[,] vertices = new Vector3[4, 4];

        //Vertices
        vertices[0, 0] = transform.InverseTransformPoint(RawControlPoint * TrickyMapInterface.Scale);
        vertices[1, 0] = transform.InverseTransformPoint(RawR1C2 * TrickyMapInterface.Scale);
        vertices[2, 0] = transform.InverseTransformPoint(RawR1C3 * TrickyMapInterface.Scale);
        vertices[3, 0] = transform.InverseTransformPoint(RawR1C4 * TrickyMapInterface.Scale);

        vertices[0, 1] = transform.InverseTransformPoint(RawR2C1 * TrickyMapInterface.Scale);
        vertices[1, 1] = transform.InverseTransformPoint(RawR2C2 * TrickyMapInterface.Scale);
        vertices[2, 1] = transform.InverseTransformPoint(RawR2C3 * TrickyMapInterface.Scale);
        vertices[3, 1] = transform.InverseTransformPoint(RawR2C4 * TrickyMapInterface.Scale);

        vertices[0, 2] = transform.InverseTransformPoint(RawR3C1 * TrickyMapInterface.Scale);
        vertices[1, 2] = transform.InverseTransformPoint(RawR3C2 * TrickyMapInterface.Scale);
        vertices[2, 2] = transform.InverseTransformPoint(RawR3C3 * TrickyMapInterface.Scale);
        vertices[3, 2] = transform.InverseTransformPoint(RawR3C4 * TrickyMapInterface.Scale);

        vertices[0, 3] = transform.InverseTransformPoint(RawR4C1 * TrickyMapInterface.Scale);
        vertices[1, 3] = transform.InverseTransformPoint(RawR4C2 * TrickyMapInterface.Scale);
        vertices[2, 3] = transform.InverseTransformPoint(RawR4C3 * TrickyMapInterface.Scale);
        vertices[3, 3] = transform.InverseTransformPoint(RawR4C4 * TrickyMapInterface.Scale);

        //Control points
        NURBS.ControlPoint[,] cps = new NURBS.ControlPoint[4, 4];
        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 pos = vertices[i, j];
                cps[i, j] = new NURBS.ControlPoint(pos.x, pos.y, pos.z, 1);
                c++;
            }
        }

        int degreeU = 3;
        int degreeV = 3;

        int resolutionU = TrickyMapInterface.Instance.settings.PatchResolution; //7;
        int resolutionV = TrickyMapInterface.Instance.settings.PatchResolution; //7; ()

        surface = new NURBS.Surface(cps, degreeU, degreeV);

        //Update degree
        surface.DegreeU(degreeU);
        surface.DegreeV(degreeV);

        //Update control points
        surface.controlPoints = cps;

        //Build mesh (reusing Mesh to save GC allocation)
        var mesh=surface.BuildMesh(resolutionU, resolutionV);

        //Build UV Points

        cps = new NURBS.ControlPoint[2, 2];

        cps[0, 0] = new NURBS.ControlPoint(UVPoint1.x, UVPoint1.y, 0, 1);
        cps[0, 1] = new NURBS.ControlPoint(UVPoint2.x, UVPoint2.y, 0, 1);
        cps[1, 0] = new NURBS.ControlPoint(UVPoint3.x, UVPoint3.y, 0, 1);
        cps[1, 1] = new NURBS.ControlPoint(UVPoint4.x, UVPoint4.y, 0, 1);

        surface = new NURBS.Surface(cps, 1, 1);

        Vector3[] UV = surface.ReturnVertices(resolutionU, resolutionV);

        Vector2[] UV2 = new Vector2[UV.Length];

        for (int i = 0; i < UV.Length; i++)
        {
            UV2[i] = PointCorrection(new Vector2(UV[i].x, UV[i].y));
        }
        mesh.uv = UV2;

        //Build Lightmap Points

        cps = new NURBS.ControlPoint[2, 2];

        cps[0, 0] = new NURBS.ControlPoint(-LightMapPoint.x, -LightMapPoint.y, 0, 1);
        cps[0, 1] = new NURBS.ControlPoint(-LightMapPoint.x, -LightMapPoint.y+ LightMapPoint.w, 0, 1);
        cps[1, 0] = new NURBS.ControlPoint(-LightMapPoint.x+LightMapPoint.z, 0, 0, 1);
        cps[1, 1] = new NURBS.ControlPoint(-LightMapPoint.x + LightMapPoint.z, -LightMapPoint.y + LightMapPoint.w, 0, 1);

        surface = new NURBS.Surface(cps, 1, 1);

        UV = surface.ReturnVertices(resolutionU, resolutionV);

        UV2 = new Vector2[UV.Length];

        mesh.uv4 = UV2;


        //Set material
        GetComponent<MeshFilter>().mesh= mesh;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<Renderer>().material = MainMaterial;
        UpdateTexture(TextureAssigment);
        ToggleLightingMode();
    }

    //public void LoadHighPolyMesh()
    //{
    //    List<int> ints = new List<int>();
    //    List<Vector3> vertices = new List<Vector3>();
    //    List<Vector2> UV = new List<Vector2>();
    //    Mesh mesh = new Mesh();

    //    //Vertices
    //    vertices.Add((RawControlPoint - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR1C2 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR1C3 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR1C4 - RawControlPoint) * TrickyMapInterface.Scale);

    //    vertices.Add((RawR2C1 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR2C2 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR2C3 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR2C4 - RawControlPoint) * TrickyMapInterface.Scale);

    //    vertices.Add((RawR3C1 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR3C2 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR3C3 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR3C4 - RawControlPoint) * TrickyMapInterface.Scale);

    //    vertices.Add((RawR4C1 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR4C2 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR4C3 - RawControlPoint) * TrickyMapInterface.Scale);
    //    vertices.Add((RawR4C4 - RawControlPoint) * TrickyMapInterface.Scale);

    //    //UV Points

    //    Vector2 Point13Dif = (UVPoint3 - UVPoint1);
    //    Vector2 Point24Dif = (UVPoint4 - UVPoint2);
    //    Vector2 Point12Dif = (UVPoint2 - UVPoint1);
    //    Vector2 Point34Dif = (UVPoint4 - UVPoint3);

    //    Vector2 Point11 = (UVPoint1 + Point13Dif * (1f / 3f));
    //    Vector2 Point1131Dif = (UVPoint2 + Point24Dif * (1f / 3f) - (UVPoint1 + Point13Dif * (1f / 3f)));
    //    Vector2 Point12 = (UVPoint1 + Point13Dif * (2f / 3f));
    //    Vector2 Point1232Dif = (UVPoint2 + Point24Dif * (2f / 3f) - (UVPoint1 + Point13Dif * (2f / 3f)));

    //    UV.Add(UVPoint1);
    //    UV.Add(UVPoint1 + Point13Dif * (1f / 3f));
    //    UV.Add(UVPoint1 + Point13Dif * (2f / 3f));
    //    UV.Add(UVPoint3);

    //    UV.Add(UVPoint1 + Point12Dif * (1f / 3f));
    //    UV.Add(Point11 + Point1131Dif * (1f / 3f));
    //    UV.Add(Point12 + Point1232Dif * (1f / 3f));
    //    UV.Add(UVPoint3 + Point34Dif * (1f / 3f));

    //    UV.Add(UVPoint1 + Point12Dif * (2f / 3f));
    //    UV.Add(Point11 + Point1131Dif * (2f / 3f));
    //    UV.Add(Point12 + Point1232Dif * (2f / 3f));
    //    UV.Add(UVPoint3 + Point34Dif * (2f / 3f));

    //    UV.Add(UVPoint2);
    //    UV.Add(UVPoint2 + Point24Dif * (1f / 3f));
    //    UV.Add(UVPoint2 + Point24Dif * (2f / 3f));
    //    UV.Add(UVPoint4);

    //    for (int i = 0; i < UV.Count; i++)
    //    {
    //        UV[i] = PointCorrection(UV[i]);
    //    }

    //    //Faces
    //    //Working
    //    ints.Add(0);
    //    ints.Add(1);
    //    ints.Add(4);

    //    //Working
    //    ints.Add(1);
    //    ints.Add(5);
    //    ints.Add(4);

    //    //Working
    //    ints.Add(1);
    //    ints.Add(2);
    //    ints.Add(5);

    //    //Working
    //    ints.Add(5);
    //    ints.Add(2);
    //    ints.Add(6);

    //    //Working
    //    ints.Add(2);
    //    ints.Add(3);
    //    ints.Add(6);

    //    //Working
    //    ints.Add(3);
    //    ints.Add(7);
    //    ints.Add(6);

    //    //Working
    //    ints.Add(4);
    //    ints.Add(5);
    //    ints.Add(8);

    //    //Working
    //    ints.Add(8);
    //    ints.Add(5);
    //    ints.Add(9);

    //    //Working
    //    ints.Add(5);
    //    ints.Add(6);
    //    ints.Add(9);

    //    //Comment Out
    //    ints.Add(9);
    //    ints.Add(6);
    //    ints.Add(10);

    //    ints.Add(6);
    //    ints.Add(7);
    //    ints.Add(10);

    //    ints.Add(10);
    //    ints.Add(7);
    //    ints.Add(11);
    //    //

    //    //Working
    //    ints.Add(8);
    //    ints.Add(9);
    //    ints.Add(12);

    //    //Working
    //    ints.Add(12);
    //    ints.Add(9);
    //    ints.Add(13);

    //    //Comment out
    //    ints.Add(9);
    //    ints.Add(10);
    //    ints.Add(13);

    //    ints.Add(13);
    //    ints.Add(10);
    //    ints.Add(14);

    //    ints.Add(10);
    //    ints.Add(11);
    //    ints.Add(14);
    //    ///

    //    //Working
    //    ints.Add(15);
    //    ints.Add(14);
    //    ints.Add(11);

    //    //Set Mesh Data
    //    mesh.vertices = vertices.ToArray();
    //    mesh.triangles = ints.ToArray();
    //    mesh.uv = UV.ToArray();
    //    mesh.RecalculateNormals();
    //    GetComponent<MeshFilter>().mesh = mesh;

    //    //Set material
    //    GetComponent<MeshCollider>().enabled = false;
    //    GetComponent<MeshCollider>().sharedMesh = mesh;
    //    GetComponent<MeshCollider>().enabled = true;
    //    GetComponent<Renderer>().material = MainMaterial;
    //    UpdateTexture(TextureAssigment);
    //    ToggleLightingMode();
    //}


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
        LoadNURBSpatch();
        if (CubePointUpdate)
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
        if (PatchPanel.instance.patchObject == this)
        {
            PatchPanel.instance.UpdatePoint(false);
        }
    }

    public void UpdateUVPoints()
    {
        LoadNURBSpatch();
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
        Renderer.material.SetFloat("_OutlineWidth", -1);
        Renderer.material.SetColor("_OutlineColor", Color.red);
        Renderer.material.SetFloat("_OpacityMaskOutline", 0.5f);
        GenerateCubePoints();
    }

    public void UnSelectedObject()
    {
        Renderer.material.SetFloat("_OutlineWidth", 0);
        Renderer.material.SetFloat("_OpacityMaskOutline", 0f);
        Renderer.material.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        DestroyCube();
    }

    public void LightMode()
    {
        
    }

    public void NoLightMode()
    {

    }

    public void UpdateTexture(int a)
    {
        try
        {
            Renderer.material.SetTexture("_MainTexture", TrickyMapInterface.Instance.textures[TextureAssigment]);
            Renderer.material.SetTextureOffset("_Lightmap", new Vector2(LightMapPoint.x, LightMapPoint.y));
            Renderer.material.SetTextureScale("_Lightmap", new Vector2(LightMapPoint.z, LightMapPoint.w));
            Renderer.material.SetTexture("_Lightmap", TrickyMapInterface.Instance.lightmaps[LightmapID]);
            TextureAssigment = a;
        }
        catch
        {
            Renderer.material.SetTexture("_MainTexture", TrickyMapInterface.Instance.ErrorTexture);;
        }
    }

    public Vector3 GetCentrePoint()
    {
        Vector3 vector3 = (RawControlPoint + RawR1C2 + RawR1C3 + RawR1C4 + RawR2C1 + RawR2C2 + RawR2C3 + RawR2C4 + RawR3C1 + RawR3C2 + RawR3C3 + RawR3C4 + RawR4C1 + RawR4C2 +RawR4C3+RawR4C4)/16;
        return vector3;
    }
}
