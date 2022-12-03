using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.Utilities;

public class Randomiser : MonoBehaviour
{
    public string Seed = "0";
    float MaxXYZ = 0.50f;
    int MaxRotation = 20;
    public void Randomise()
    {
        TrickyMapInterface trickyMapInterface = TrickyMapInterface.Instance;

        var Patches = trickyMapInterface.patchObjects;

        for (int i = 0; i < Patches.Count; i++)
        {
            var TempPatch = Patches[i].GetComponent<PatchObject>();

            Vector3 Highest = TempPatch.RawControlPoint;
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR1C2);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR1C3);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR1C4);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR2C1);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR2C2);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR2C3);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR2C4);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR3C1);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR3C2);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR3C3);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR3C4);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR4C1);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR4C2);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR4C3);
            Highest = JsonUtil.Highest(Highest, TempPatch.RawR4C4);

            Vector3 Lowest = TempPatch.RawControlPoint;
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR1C2);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR1C3);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR1C4);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR2C1);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR2C2);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR2C3);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR2C4);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR3C1);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR3C2);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR3C3);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR3C4);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR4C1);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR4C2);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR4C3);
            Lowest = JsonUtil.Lowest(Lowest, TempPatch.RawR4C4);

            float Scale = Vector3.Distance(Highest, Lowest);

            TempPatch.RawR3C3 += GenerateRandomXYZ(Scale, i*4);

            TempPatch.RawR3C2 += GenerateRandomXYZ(Scale, i*4+1);

            TempPatch.RawR2C3 += GenerateRandomXYZ(Scale, i * 4 + 2);

            TempPatch.RawR2C2 += GenerateRandomXYZ(Scale, i * 4 + 3);

            TempPatch.LoadNURBSpatch();
        }

        var Instances = trickyMapInterface.instanceObjects;
        for (int i = 0; i < Instances.Count; i++)
        {
            var TempInstance = Instances[i].GetComponent<InstanceObject>();

            TempInstance.transform.localEulerAngles += GenerateRandomAngle(i);
        }
    }

    Vector3 GenerateRandomAngle(int Index)
    {
        System.Random r = new System.Random(Seed.GetHashCode()+Index);
        float tempx = (float)r.Next(-MaxRotation*100, MaxRotation*100)/100f;
        float tempy = (float)r.Next(-MaxRotation*100, MaxRotation*100)/ 100f;
        float tempz = (float)r.Next(-MaxRotation*100, MaxRotation*100)/ 100f;
        return new Vector3(tempx, tempy, tempz);
    }

    Vector3 GenerateRandomXYZ(float Scale, int Index)
    {
        System.Random r = new System.Random(Seed.GetHashCode() + Index);
        float tempx = ((float)r.Next(-1 * 100, 1 * 100)/100)* MaxXYZ;
        float tempy = ((float)r.Next(-1 * 100, 1 * 100) / 100) * MaxXYZ;
        float tempz = ((float)r.Next(-1 * 100, 1 * 100) / 100) * MaxXYZ;
        return new Vector3(tempx, tempy, tempz)*Scale;
    }
}
