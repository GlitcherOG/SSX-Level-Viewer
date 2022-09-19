using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstanceObject : MonoBehaviour
{
    public Vector3 MatrixCol1;
    public Vector3 MatrixCol2;
    public Vector3 MatrixCol3;
    public Vector3 ParticleInstancePosition;
    public int UnknownInt1;
    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;
    public int UnknownInt7;
    public int UnknownInt8;
    public int UnknownInt9;
    public int UnknownInt10;
    public int UnknownInt11;
    public int UnknownInt12;

    //public void LoadParticleInstance(ParticleInstance particleInstance)
    //{
    //    MatrixCol1 = ConversionTools.Vertex3ToVector3(particleInstance.MatrixCol1);
    //    MatrixCol2 = ConversionTools.Vertex3ToVector3(particleInstance.MatrixCol3);
    //    MatrixCol3 = ConversionTools.Vertex3ToVector3(particleInstance.MatrixCol2);
    //    ParticleInstancePosition = ConversionTools.Vertex3ToVector3(particleInstance.ParticleInstancePosition);
    //    UnknownInt1 = particleInstance.UnknownInt1;
    //    LowestXYZ = ConversionTools.Vertex3ToVector3(particleInstance.LowestXYZ);
    //    HighestXYZ = ConversionTools.Vertex3ToVector3(particleInstance.HighestXYZ);
    //    UnknownInt8 = particleInstance.UnknownInt8;
    //    UnknownInt9 = particleInstance.UnknownInt9;
    //    UnknownInt10 = particleInstance.UnknownInt10;
    //    UnknownInt11 = particleInstance.UnknownInt11;
    //    UnknownInt12 = particleInstance.UnknownInt12;
    //}

    //public ParticleInstance GenerateParticleInstance()
    //{
    //    ParticleInstance particleInstance = new ParticleInstance();
    //    particleInstance.MatrixCol1 = ConversionTools.Vector3ToVertex3(MatrixCol1,0f);
    //    particleInstance.MatrixCol2 = ConversionTools.Vector3ToVertex3(MatrixCol1,0f);
    //    particleInstance.MatrixCol3 = ConversionTools.Vector3ToVertex3(MatrixCol1,0f);
    //    particleInstance.ParticleInstancePosition = ConversionTools.Vector3ToVertex3(ParticleInstancePosition, 1f);

    //    particleInstance.UnknownInt1 = UnknownInt1;

    //    particleInstance.LowestXYZ = ConversionTools.Vector3ToVertex3(LowestXYZ);
    //    particleInstance.HighestXYZ = ConversionTools.Vector3ToVertex3(HighestXYZ);

    //    particleInstance.UnknownInt8 = UnknownInt8;
    //    particleInstance.UnknownInt9 = UnknownInt9;
    //    particleInstance.UnknownInt10 = UnknownInt10;
    //    particleInstance.UnknownInt11 = UnknownInt11;
    //    particleInstance.UnknownInt12 = UnknownInt12;

    //    return particleInstance;
    //}
}
