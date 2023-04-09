using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjImporter : MonoBehaviour
{

    public static Mesh ObjLoad(string path)
    {
        string[] Lines = File.ReadAllLines(path);
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> TextureCords = new List<Vector2>();
        List<Faces> MeshFaces = new List<Faces>();
        Mesh mesh = new Mesh();
        //Load File
        for (int a = 0; a < Lines.Length; a++)
        {
            if (Lines[a].StartsWith("v "))
            {
                string[] splitLine = Lines[a].Split(' ');
                Vector3 vector3 = new Vector3();
                vector3.x = float.Parse(splitLine[1]);
                vector3.y = float.Parse(splitLine[2]);
                vector3.z = float.Parse(splitLine[3]);
                vertices.Add(vector3);
            }

            if (Lines[a].StartsWith("vt "))
            {
                string[] splitLine = Lines[a].Split(' ');
                Vector2 vector2 = new Vector2();
                vector2.x = float.Parse(splitLine[1]);
                vector2.y = float.Parse(splitLine[2]);
                TextureCords.Add(vector2);
            }

            if (Lines[a].StartsWith("vn "))
            {
                string[] splitLine = Lines[a].Split(' ');
                Vector3 vector3 = new Vector3();
                vector3.x = float.Parse(splitLine[1]);
                vector3.y = float.Parse(splitLine[2]);
                vector3.z = float.Parse(splitLine[3]);
                normals.Add(vector3);
            }

            if (Lines[a].StartsWith("f "))
            {
                string[] splitLine = Lines[a].Split(' ');
                Faces faces = new Faces();

                string[] SplitPoint = splitLine[1].Split('/');
                faces.V1Pos = int.Parse(SplitPoint[0]) - 1;
                faces.UV1Pos = int.Parse(SplitPoint[1]) - 1;
                faces.Normal1Pos = int.Parse(SplitPoint[2]) - 1;

                SplitPoint = splitLine[2].Split('/');
                faces.V2Pos = int.Parse(SplitPoint[0]) - 1;
                faces.UV2Pos = int.Parse(SplitPoint[1]) - 1;
                faces.Normal2Pos = int.Parse(SplitPoint[2]) - 1;

                SplitPoint = splitLine[3].Split('/');
                faces.V3Pos = int.Parse(SplitPoint[0]) - 1;
                faces.UV3Pos = int.Parse(SplitPoint[1]) - 1;
                faces.Normal3Pos = int.Parse(SplitPoint[2]) - 1;

                MeshFaces.Add(faces);
            }
        }

        //Generate New Mesh

        List<Vector3> NewVertices = new List<Vector3>();
        List<Vector2> NewTextureCords = new List<Vector2>();
        List<Vector3> NewNormals = new List<Vector3>();
        List<int> Indices = new List<int>();

        for (int i = 0; i < MeshFaces.Count; i++)
        {
            NewVertices.Add(vertices[MeshFaces[i].V1Pos]);
            NewVertices.Add(vertices[MeshFaces[i].V2Pos]);
            NewVertices.Add(vertices[MeshFaces[i].V3Pos]);

            NewTextureCords.Add(TextureCords[MeshFaces[i].UV1Pos]);
            NewTextureCords.Add(TextureCords[MeshFaces[i].UV2Pos]);
            NewTextureCords.Add(TextureCords[MeshFaces[i].UV3Pos]);

            NewNormals.Add(normals[MeshFaces[i].Normal1Pos]);
            NewNormals.Add(normals[MeshFaces[i].Normal2Pos]);
            NewNormals.Add(normals[MeshFaces[i].Normal3Pos]);

            Indices.Add(3*i);
            Indices.Add(3 * i + 1);
            Indices.Add(3 * i + 2);
        }

        mesh.vertices = NewVertices.ToArray();
        mesh.normals = NewNormals.ToArray();
        mesh.uv = NewTextureCords.ToArray();
        mesh.triangles = Indices.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        return mesh;
    }
    public struct Faces
    {
        public Vector3 V1;
        public Vector3 V2;
        public Vector3 V3;

        public int V1Pos;
        public int V2Pos;
        public int V3Pos;

        public Vector2 UV1;
        public Vector2 UV2;
        public Vector2 UV3;

        public int UV1Pos;
        public int UV2Pos;
        public int UV3Pos;

        public Vector3 Normal1;
        public Vector3 Normal2;
        public Vector3 Normal3;

        public int Normal1Pos;
        public int Normal2Pos;
        public int Normal3Pos;

        public bool tripstriped;
    }
}