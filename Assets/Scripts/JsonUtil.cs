using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SSXMultiTool.Utilities
{
    public class JsonUtil
    {
        public static UnityEngine.Vector3 NumericVector3ToUnity(System.Numerics.Vector3 vector)
        {
            return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Vector3 Vector4ToVector3(Vector4 vector4)
        {
            return new Vector3(vector4.x, vector4.y, vector4.z);
        }

        public static Vector4 Vector3ToVector4(Vector3 vector3, float w = 1)
        {
            return new Vector4(vector3.x, vector3.y, vector3.z, w);
        }

        public static float[] Vector4ToArray(Vector4 vector4)
        {
            float[] array = new float[4];
            array[0] = vector4.x;
            array[1] = vector4.y;
            array[2] = vector4.z;
            array[3] = vector4.w;
            return array;
        }

        public static Vector4 ArrayToVector4(float[] floats)
        {
            return new Vector4(floats[0], floats[1], floats[2], floats[3]);
        }

        public static float[] Vector3ToArray(Vector3 vector3)
        {
            float[] array = new float[3];
            array[0] = vector3.x;
            array[1] = vector3.y;
            array[2] = vector3.z;
            return array;
        }

        public static Vector3 ArrayToVector3(float[] floats)
        {
            return new Vector3(floats[0], floats[1], floats[2]);
        }

        public static Vector3 Highest(Vector3 current, Vector3 vector3)
        {
            Vector3 vertex = vector3;
            if (vertex.x > current.x)
            {
                current.x = vertex.x;
            }
            if (vertex.y > current.y)
            {
                current.y = vertex.y;
            }
            if (vertex.z > current.z)
            {
                current.z = vertex.z;
            }
            return current;
        }

        public static Vector3 Lowest(Vector3 current, Vector3 vector3)
        {
            Vector3 vertex = vector3;
            if (vertex.x < current.x)
            {
                current.x = vertex.x;
            }
            if (vertex.y < current.y)
            {
                current.y = vertex.y;
            }
            if (vertex.z < current.z)
            {
                current.z = vertex.z;
            }
            return current;
        }

        public static float[] QuaternionToArray(Quaternion quaternion)
        {
            float[] array = new float[4];
            array[0] = quaternion.x;
            array[1] = quaternion.y;
            array[2] = quaternion.z;
            array[3] = quaternion.w;
            return array;
        }

        public static Quaternion ArrayToQuaternion(float[] array)
        {
            return new Quaternion(array[0], array[1], array[2], array[3]);
        }

        public static float GenerateDistance(Vector3 Point1, Vector3 Point2, Vector3 Point3, Vector3 Point4)
        {
            float Distance = 0;
            Distance += Vector3.Distance(Point1, Point2);
            Distance += Vector3.Distance(Point2, Point3);
            Distance += Vector3.Distance(Point3, Point4);
            return Distance;
        }

        public static Vector4 Vector2ToVector4(Vector2 vector2, float z = 1, float w = 1)
        {
            return new Vector4(vector2.x, vector2.y, z, w);
        }
    }
}
