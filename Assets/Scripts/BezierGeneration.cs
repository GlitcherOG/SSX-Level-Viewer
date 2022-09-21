using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierGeneration
{
    public int Tezilation;
    public Vector2 UVPoint1;
    public Vector2 UVPoint2;
    public Vector2 UVPoint3;
    public Vector2 UVPoint4;
    public Vector3[,] vectors = new Vector3[4,4];

    public Mesh GenerateMesh()
    {
        List<int> ints = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> UV = new List<Vector2>();
        Mesh mesh = new Mesh();

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


        for (int x = 0; x < Tezilation; x++)
        {
            float e = (float)x / (float)Tezilation;
            for (int y = 0; y < Tezilation; y++)
            {
                float f = (float)y / (float)Tezilation;
                Vector3 a = CalculateCubicBezierPoint(e, vectors[0, 0], vectors[1, 0], vectors[2, 0], vectors[3, 0]);
                Vector3 b = CalculateCubicBezierPoint(e, vectors[0, 1], vectors[1, 1], vectors[2, 1], vectors[3, 1]);
                Vector3 c = CalculateCubicBezierPoint(e, vectors[0, 2], vectors[1, 2], vectors[2, 2], vectors[3, 2]);
                Vector3 d = CalculateCubicBezierPoint(e, vectors[0, 3], vectors[1, 3], vectors[2, 3], vectors[3, 3]);

                vertices.Add(CalculateCubicBezierPoint(f, a, b, c, d));
            }
        }

        for (int y = 0; y < Tezilation-1; y++)
        {
            for (int x = 0; x < Tezilation-1; x++)
            {
                ints.Add(y * Tezilation + x + 1);
                ints.Add(y * Tezilation + x);
                ints.Add((y + 1) * Tezilation + x);

                ints.Add((y + 1) * Tezilation + x);
                ints.Add((y + 1) * Tezilation + x + 1);
                ints.Add(y * Tezilation + x + 1);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = ints.ToArray();
        //mesh.uv = UV.ToArray();
        mesh.RecalculateNormals();
        return mesh;
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
}
