using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSX_Modder.Utilities;

namespace SSX_Modder.FileHandlers.MapEditor
{
    public class PBDHandler
    {
        public byte[] MagicBytes;
        public int NumPlayerStarts;
        public int NumPatches;
        public int NumInstances;
        public int NumParticleInstances;
        public int NumMaterials;
        public int NumMaterialBlocks;
        public int NumLights;
        public int NumSplines;
        public int NumSplineSegments;
        public int NumTextureFlipbooks;
        public int NumModels;
        public int ParticleModelCount;
        public int NumTextures;

        public int NumCameras;
        public int LightMapSize;

        public int PlayerStartOffset;
        public int PatchOffset;
        public int InstanceOffset;
        public int ParticleInstancesOffset;
        public int MaterialOffset;
        public int MaterialBlocksOffset;
        public int LightsOffset;
        public int SplineOffset;
        public int SplineSegmentOffset;
        public int TextureFlipbookOffset;
        public int ModelPointerOffset;
        public int ModelsOffset;


        public int ParticleModelPointerOffset;
        public int ParticleModelsOffset;
        public int CameraPointerOffset;
        public int CamerasOffset;
        public int HashOffset;
        public int ModelDataOffset;
        public int Unknown34;
        public int Unknown35;

        public List<Patch> Patches;
        public List<Model> models;
        public List<Spline> splines;
        public List<SplinesSegments> splinesSegments;
        public List<TextureFlipbook> textureFlipbooks;
        public List<Instance> Instances;
        public List<ParticleInstance> particleInstances;

        public void loadandsave(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                MagicBytes = StreamUtil.ReadBytes(stream, 4);
                NumPlayerStarts = StreamUtil.ReadInt32(stream); //NA
                NumPatches = StreamUtil.ReadInt32(stream); //Done
                NumInstances = StreamUtil.ReadInt32(stream); //Done
                NumParticleInstances = StreamUtil.ReadInt32(stream); //Done
                NumMaterials = StreamUtil.ReadInt32(stream);
                NumMaterialBlocks = StreamUtil.ReadInt32(stream);
                NumLights = StreamUtil.ReadInt32(stream);
                NumSplines = StreamUtil.ReadInt32(stream); //Done
                NumSplineSegments = StreamUtil.ReadInt32(stream); //Done
                NumTextureFlipbooks = StreamUtil.ReadInt32(stream); //Done
                NumModels = StreamUtil.ReadInt32(stream);
                ParticleModelCount = StreamUtil.ReadInt32(stream);
                NumTextures = StreamUtil.ReadInt32(stream);
                NumCameras = StreamUtil.ReadInt32(stream); //Used in SSXFE MAP
                LightMapSize = StreamUtil.ReadInt32(stream);

                PlayerStartOffset = StreamUtil.ReadInt32(stream);
                PatchOffset = StreamUtil.ReadInt32(stream); //Done
                InstanceOffset = StreamUtil.ReadInt32(stream); //Done
                ParticleInstancesOffset = StreamUtil.ReadInt32(stream); //Done
                MaterialOffset = StreamUtil.ReadInt32(stream);
                MaterialBlocksOffset = StreamUtil.ReadInt32(stream);
                LightsOffset = StreamUtil.ReadInt32(stream);
                SplineOffset = StreamUtil.ReadInt32(stream); //Done
                SplineSegmentOffset = StreamUtil.ReadInt32(stream); //Done
                TextureFlipbookOffset = StreamUtil.ReadInt32(stream); //Done
                ModelPointerOffset = StreamUtil.ReadInt32(stream);
                ModelsOffset = StreamUtil.ReadInt32(stream);

                ParticleModelPointerOffset = StreamUtil.ReadInt32(stream);
                ParticleModelsOffset = StreamUtil.ReadInt32(stream);
                CameraPointerOffset = StreamUtil.ReadInt32(stream);
                CamerasOffset = StreamUtil.ReadInt32(stream);
                HashOffset = StreamUtil.ReadInt32(stream);
                ModelDataOffset = StreamUtil.ReadInt32(stream);
                Unknown34 = StreamUtil.ReadInt32(stream);
                Unknown35 = StreamUtil.ReadInt32(stream);

                //Patch Loading
                stream.Position = PatchOffset;
                Patches = new List<Patch>();
                for (int i = 0; i < NumPatches; i++)
                {
                    Patch patch = LoadPatch(stream);
                    Patches.Add(patch);
                }

                stream.Position = InstanceOffset;
                Instances = new List<Instance>();
                for (int i = 0; i < NumInstances; i++)
                {
                    var TempInstance = new Instance();
                    TempInstance.MatrixCol1 = ReadVertices(stream, true);
                    TempInstance.MatrixCol2 = ReadVertices(stream, true);
                    TempInstance.MatrixCol3 = ReadVertices(stream, true);
                    TempInstance.InstancePosition = ReadVertices(stream, true);
                    TempInstance.Unknown5 = ReadVertices(stream, true);
                    TempInstance.Unknown6 = ReadVertices(stream, true);
                    TempInstance.Unknown7 = ReadVertices(stream, true);
                    TempInstance.Unknown8 = ReadVertices(stream, true);
                    TempInstance.Unknown9 = ReadVertices(stream, true);
                    TempInstance.Unknown10 = ReadVertices(stream, true);
                    TempInstance.Unknown11 = ReadVertices(stream, true);
                    TempInstance.RGBA = ReadVertices(stream, true);
                    TempInstance.ModelID = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt18 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt19 = StreamUtil.ReadInt32(stream);

                    TempInstance.LowestXYZ = ReadVertices(stream, false);
                    TempInstance.HighestXYZ = ReadVertices(stream, false);

                    TempInstance.UnknownInt26 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt27 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt28 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt29 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt30 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt31 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt32 = StreamUtil.ReadInt32(stream);
                    Instances.Add(TempInstance);
                }

                stream.Position = ParticleInstancesOffset;
                particleInstances = new List<ParticleInstance>();
                for (int i = 0; i < NumParticleInstances; i++)
                {
                    ParticleInstance TempParticle = new ParticleInstance();
                    TempParticle.Unknown1 = ReadVertices(stream, true);
                    TempParticle.Unknown2 = ReadVertices(stream, true);
                    TempParticle.Unknown3 = ReadVertices(stream, true);
                    TempParticle.Unknown4 = ReadVertices(stream, true);
                    TempParticle.UnknownInt1 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt2 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt3 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt4 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt5 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt6 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt7 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt8 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt9 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt10 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt11 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt12 = StreamUtil.ReadInt32(stream);
                    particleInstances.Add(TempParticle);
                }

                //Spline Data
                stream.Position = SplineOffset;
                splines = new List<Spline>();
                for (int i = 0; i < NumSplines; i++)
                {
                    Spline spline = new Spline();
                    spline.LowestXYZ = ReadVertices(stream, false);
                    spline.HighestXYZ = ReadVertices(stream, false);
                    spline.Unknown1 = StreamUtil.ReadInt32(stream);
                    spline.SplineSegmentCount = StreamUtil.ReadInt32(stream);
                    spline.SplineSegmentPosition = StreamUtil.ReadInt32(stream);
                    spline.Unknown2 = StreamUtil.ReadInt32(stream);
                    splines.Add(spline);
                }

                //Spline Segments
                stream.Position = SplineSegmentOffset;
                splinesSegments = new List<SplinesSegments>();
                for (int i = 0; i < NumSplineSegments; i++)
                {
                    SplinesSegments splinesSegment = new SplinesSegments();

                    splinesSegment.Point4 = ReadVertices(stream, true);
                    splinesSegment.Point3 = ReadVertices(stream, true);
                    splinesSegment.Point2 = ReadVertices(stream, true);
                    splinesSegment.ControlPoint = ReadVertices(stream, true);

                    splinesSegment.ScalingPoint = ReadVertices(stream, true);

                    splinesSegment.PreviousSegment = StreamUtil.ReadInt32(stream);
                    splinesSegment.NextSegment = StreamUtil.ReadInt32(stream);
                    splinesSegment.SplineParent = StreamUtil.ReadInt32(stream);

                    splinesSegment.LowestXYZ = ReadVertices(stream, false);
                    splinesSegment.HighestXYZ = ReadVertices(stream, false);

                    splinesSegment.SegmentDisatnce = StreamUtil.ReadFloat(stream);
                    splinesSegment.PreviousSegmentsDistance = StreamUtil.ReadFloat(stream);
                    splinesSegment.Unknown32 = StreamUtil.ReadInt32(stream);
                    splinesSegments.Add(splinesSegment);
                }

                //Texture Flips
                textureFlipbooks = new List<TextureFlipbook>();
                stream.Position = TextureFlipbookOffset;
                for (int i = 0; i < NumTextureFlipbooks; i++)
                {
                    var TempTextureFlip = new TextureFlipbook();
                    TempTextureFlip.ImageCount = StreamUtil.ReadInt32(stream);
                    TempTextureFlip.ImagePositions = new List<int>();
                    for (int a = 0; a < TempTextureFlip.ImageCount; a++)
                    {
                        TempTextureFlip.ImagePositions.Add(StreamUtil.ReadInt32(stream));
                    }
                    textureFlipbooks.Add(TempTextureFlip);
                }

                //ModelData
                stream.Position = ModelDataOffset;
                models = new List<Model>();
                Model model = new Model();
                model.staticMeshes = new List<StaticMesh>();
                int count = 0;
                while (true)
                {
                    var temp = ReadMesh(stream);
                    if (temp.Equals(new StaticMesh()))
                    {
                        break;
                    }
                    count++;
                    model.staticMeshes.Add(GenerateFaces(temp));
                    stream.Position += 31;
                    if (StreamUtil.ReadByte(stream) == 0x6C)
                    {
                        stream.Position -= 32;
                    }
                    else
                    {
                        stream.Position += 48;
                        models.Add(model);
                        model = new Model();
                        model.staticMeshes = new List<StaticMesh>();
                    }
                }
            }
        }

        public void Save(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                MagicBytes = StreamUtil.ReadBytes(stream, 4);
                NumPlayerStarts = StreamUtil.ReadInt32(stream);

                StreamUtil.WriteInt32(stream, Patches.Count);

                NumInstances = StreamUtil.ReadInt32(stream);
                NumParticleInstances = StreamUtil.ReadInt32(stream);
                NumMaterials = StreamUtil.ReadInt32(stream);
                NumMaterialBlocks = StreamUtil.ReadInt32(stream);
                NumLights = StreamUtil.ReadInt32(stream);

                StreamUtil.WriteInt32(stream, splines.Count);
                StreamUtil.WriteInt32(stream, splinesSegments.Count);
                StreamUtil.WriteInt32(stream, textureFlipbooks.Count);

                NumModels = StreamUtil.ReadInt32(stream);
                ParticleModelCount = StreamUtil.ReadInt32(stream);

                StreamUtil.WriteInt32(stream, NumTextures);

                NumCameras = StreamUtil.ReadInt32(stream); //Used in SSXFE MAP
                StreamUtil.WriteInt32(stream, 0); //Lightmap size 

                PlayerStartOffset = StreamUtil.ReadInt32(stream);
                PatchOffset = StreamUtil.ReadInt32(stream); //Done Need to make custom write later
                InstanceOffset = StreamUtil.ReadInt32(stream);
                ParticleInstancesOffset = StreamUtil.ReadInt32(stream);
                MaterialOffset = StreamUtil.ReadInt32(stream);
                MaterialBlocksOffset = StreamUtil.ReadInt32(stream);
                LightsOffset = StreamUtil.ReadInt32(stream);
                SplineOffset = StreamUtil.ReadInt32(stream); //Done Need to make custom write later
                SplineSegmentOffset = StreamUtil.ReadInt32(stream); //Done Need to make custom write later
                TextureFlipbookOffset = StreamUtil.ReadInt32(stream); //Done Need to make custom write later
                ModelPointerOffset = StreamUtil.ReadInt32(stream);
                ModelsOffset = StreamUtil.ReadInt32(stream);

                ParticleModelPointerOffset = StreamUtil.ReadInt32(stream);
                ParticleModelsOffset = StreamUtil.ReadInt32(stream);
                CameraPointerOffset = StreamUtil.ReadInt32(stream);
                CamerasOffset = StreamUtil.ReadInt32(stream);
                HashOffset = StreamUtil.ReadInt32(stream);
                ModelDataOffset = StreamUtil.ReadInt32(stream);
                Unknown34 = StreamUtil.ReadInt32(stream);
                Unknown35 = StreamUtil.ReadInt32(stream);

                stream.Position = PatchOffset;
                for (int i = 0; i < Patches.Count; i++)
                {
                    var TempPatch = Patches[i];
                    SaveVertices(stream, TempPatch.ScalePoint, true);

                    SaveVertices(stream, TempPatch.UVPoint1, true);
                    SaveVertices(stream, TempPatch.UVPoint2, true);
                    SaveVertices(stream, TempPatch.UVPoint3, true);
                    SaveVertices(stream, TempPatch.UVPoint4, true);

                    SaveVertices(stream, TempPatch.R4C4, true);
                    SaveVertices(stream, TempPatch.R4C3, true);
                    SaveVertices(stream, TempPatch.R4C2, true);
                    SaveVertices(stream, TempPatch.R4C1, true);
                    SaveVertices(stream, TempPatch.R3C4, true);
                    SaveVertices(stream, TempPatch.R3C3, true);
                    SaveVertices(stream, TempPatch.R3C2, true);
                    SaveVertices(stream, TempPatch.R3C1, true);
                    SaveVertices(stream, TempPatch.R2C4, true);
                    SaveVertices(stream, TempPatch.R2C3, true);
                    SaveVertices(stream, TempPatch.R2C2, true);
                    SaveVertices(stream, TempPatch.R2C1, true);
                    SaveVertices(stream, TempPatch.R1C4, true);
                    SaveVertices(stream, TempPatch.R1C3, true);
                    SaveVertices(stream, TempPatch.R1C2, true);
                    SaveVertices(stream, TempPatch.R1C1, true);

                    SaveVertices(stream, TempPatch.LowestXYZ, false);
                    SaveVertices(stream, TempPatch.HighestXYZ, false);

                    SaveVertices(stream, TempPatch.Point1, true);
                    SaveVertices(stream, TempPatch.Point2, true);
                    SaveVertices(stream, TempPatch.Point3, true);
                    SaveVertices(stream, TempPatch.Point4, true);

                    StreamUtil.WriteInt32(stream, TempPatch.PatchStyle);
                    StreamUtil.WriteInt32(stream, TempPatch.Unknown2);
                    StreamUtil.WriteInt16(stream, TempPatch.TextureAssigment);
                    StreamUtil.WriteInt16(stream, TempPatch.LightmapID);
                    StreamUtil.WriteInt32(stream, TempPatch.Unknown4);
                    StreamUtil.WriteInt32(stream, TempPatch.Unknown5);
                    StreamUtil.WriteInt32(stream, TempPatch.Unknown6);
                }

                //Texture Flipbooks
                stream.Position = TextureFlipbookOffset;
                for (int i = 0; i < textureFlipbooks.Count; i++)
                {
                    StreamUtil.WriteInt32(stream, textureFlipbooks[i].ImagePositions.Count);
                    for (int a = 0; a < textureFlipbooks[i].ImagePositions.Count; a++)
                    {
                        StreamUtil.WriteInt32(stream, textureFlipbooks[i].ImagePositions[a]);
                    }
                }

                //Spline
                stream.Position = SplineOffset;
                for (int i = 0; i < splines.Count; i++)
                {
                    var spline = splines[i];
                    SaveVertices(stream, spline.LowestXYZ, false);
                    SaveVertices(stream, spline.HighestXYZ, false);
                    StreamUtil.WriteInt32(stream, spline.Unknown1);
                    StreamUtil.WriteInt32(stream, spline.SplineSegmentCount);
                    StreamUtil.WriteInt32(stream, spline.SplineSegmentPosition);
                    StreamUtil.WriteInt32(stream, spline.Unknown2);
                }

                //Spline Segments
                stream.Position = SplineSegmentOffset;
                for (int i = 0; i < splinesSegments.Count; i++)
                {
                    var SplineSegment = splinesSegments[i];
                    SaveVertices(stream, SplineSegment.Point4, true);
                    SaveVertices(stream, SplineSegment.Point3, true);
                    SaveVertices(stream, SplineSegment.Point2, true);
                    SaveVertices(stream, SplineSegment.ControlPoint, true);

                    SaveVertices(stream, SplineSegment.ScalingPoint, true);

                    StreamUtil.WriteInt32(stream, SplineSegment.PreviousSegment);
                    StreamUtil.WriteInt32(stream, SplineSegment.NextSegment);
                    StreamUtil.WriteInt32(stream, SplineSegment.SplineParent);

                    SaveVertices(stream, SplineSegment.LowestXYZ, false);
                    SaveVertices(stream, SplineSegment.HighestXYZ, false);

                    StreamUtil.WriteFloat32(stream, SplineSegment.SegmentDisatnce);
                    StreamUtil.WriteFloat32(stream, SplineSegment.PreviousSegmentsDistance);
                    StreamUtil.WriteInt32(stream, SplineSegment.Unknown32);
                }

                stream.Position = InstanceOffset;
                for (int i = 0; i < Instances.Count; i++)
                {
                    var TempInstance = Instances[i];
                    SaveVertices(stream, TempInstance.MatrixCol1, true);
                    SaveVertices(stream, TempInstance.MatrixCol2, true);
                    SaveVertices(stream, TempInstance.MatrixCol3, true);
                    SaveVertices(stream, TempInstance.InstancePosition, true);
                    SaveVertices(stream, TempInstance.Unknown5, true);
                    SaveVertices(stream, TempInstance.Unknown6, true);
                    SaveVertices(stream, TempInstance.Unknown7, true);
                    SaveVertices(stream, TempInstance.Unknown8, true);
                    SaveVertices(stream, TempInstance.Unknown9, true);
                    SaveVertices(stream, TempInstance.Unknown10, true);
                    SaveVertices(stream, TempInstance.Unknown11, true);
                    SaveVertices(stream, TempInstance.RGBA, true);

                    StreamUtil.WriteInt32(stream, TempInstance.ModelID);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt18);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt19);

                    SaveVertices(stream, TempInstance.LowestXYZ, false);
                    SaveVertices(stream, TempInstance.HighestXYZ, false);

                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt26);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt27);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt28);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt29);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt30);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt31);
                    StreamUtil.WriteInt32(stream, TempInstance.UnknownInt32);
                }
            }
        }
        #region Standard Mesh Stuff
        public StaticMesh ReadMesh(Stream stream)
        {
            var ModelData = new StaticMesh();

            if (stream.Position >= stream.Length)
            {
                return new StaticMesh();
            }

            stream.Position += 48;

            ModelData.StripCount = StreamUtil.ReadInt32(stream);
            ModelData.Unknown1 = StreamUtil.ReadInt32(stream);
            ModelData.VertexCount = StreamUtil.ReadInt32(stream);
            ModelData.Unknown3 = StreamUtil.ReadInt32(stream);

            stream.Position += 16;

            //Load Strip Count
            List<int> TempStrips = new List<int>();
            for (int a = 0; a < ModelData.StripCount; a++)
            {
                TempStrips.Add(StreamUtil.ReadInt16(stream));
            }
            StreamUtil.AlignBy16(stream);

            stream.Position += 16;
            ModelData.Strips = TempStrips;


            List<UV> UVs = new List<UV>();
            //Read UV Texture Points
            stream.Position += 48;
            for (int a = 0; a < ModelData.VertexCount; a++)
            {
                UV uv = new UV();
                uv.X = StreamUtil.ReadInt16(stream);
                uv.Y = StreamUtil.ReadInt16(stream);
                //uv.XU = StreamUtil.ReadInt16(stream);
                //uv.YU = StreamUtil.ReadInt16(stream);
                UVs.Add(uv);
            }
            StreamUtil.AlignBy16(stream);
            stream.Position += 16;

            //Everything Above is Correct

            ModelData.uv = UVs;

            List<UVNormal> Normals = new List<UVNormal>();
            //Read Normals
            stream.Position += 32;
            for (int a = 0; a < ModelData.VertexCount; a++)
            {
                UVNormal normal = new UVNormal();
                normal.X = StreamUtil.ReadInt16(stream);
                normal.Y = StreamUtil.ReadInt16(stream);
                normal.Z = StreamUtil.ReadInt16(stream);
                Normals.Add(normal);
            }
            StreamUtil.AlignBy16(stream);
            ModelData.uvNormals = Normals;

            List<Vertex3> vertices = new List<Vertex3>();
            stream.Position += 16;
            //Load Vertex
            for (int a = 0; a < ModelData.VertexCount; a++)
            {
                Vertex3 vertex = new Vertex3();
                //Float 16's
                vertex.X = StreamUtil.ReadInt16(stream);
                vertex.Y = StreamUtil.ReadInt16(stream);
                vertex.Z = StreamUtil.ReadInt16(stream);
                vertices.Add(vertex);
            }
            StreamUtil.AlignBy16(stream);
            ModelData.vertices = vertices;
            stream.Position += 16;

            return ModelData;
        }

        public StaticMesh GenerateFaces(StaticMesh models)
        {
            var ModelData = models;
            //Increment Strips
            List<int> strip2 = new List<int>();
            strip2.Add(0);
            foreach (var item in ModelData.Strips)
            {
                strip2.Add(strip2[strip2.Count - 1] + item);
            }
            ModelData.Strips = strip2;

            //Make Faces
            ModelData.faces = new List<Face>();
            int localIndex = 0;
            int Rotation = 0;
            for (int b = 0; b < ModelData.vertices.Count; b++)
            {
                if (InsideSplits(b, ModelData.Strips))
                {
                    Rotation = 0;
                    localIndex = 1;
                    continue;
                }
                if (localIndex < 2)
                {
                    localIndex++;
                    continue;
                }

                ModelData.faces.Add(CreateFaces(b, ModelData, Rotation));
                Rotation++;
                if (Rotation == 2)
                {
                    Rotation = 0;
                }
                localIndex++;
            }

            return ModelData;
        }
        public bool InsideSplits(int Number, List<int> splits)
        {
            foreach (var item in splits)
            {
                if (item == Number)
                {
                    return true;
                }
            }
            return false;
        }
        public Face CreateFaces(int Index, StaticMesh ModelData, int roatation)
        {
            Face face = new Face();
            int Index1 = 0;
            int Index2 = 0;
            int Index3 = 0;
            //Fixes the Rotation For Exporting
            //Swap When Exporting to other formats
            //1-Clockwise
            //0-Counter Clocwise
            if (roatation == 1)
            {
                Index1 = Index;
                Index2 = Index - 1;
                Index3 = Index - 2;
            }
            if (roatation == 0)
            {
                Index1 = Index;
                Index2 = Index - 2;
                Index3 = Index - 1;
            }
            face.V1 = ModelData.vertices[Index1];
            face.V2 = ModelData.vertices[Index2];
            face.V3 = ModelData.vertices[Index3];

            face.V1Pos = Index1;
            face.V2Pos = Index2;
            face.V3Pos = Index3;

            if (ModelData.uv.Count != 0)
            {
                face.UV1 = ModelData.uv[Index1];
                face.UV2 = ModelData.uv[Index2];
                face.UV3 = ModelData.uv[Index3];

                face.UV1Pos = Index1;
                face.UV2Pos = Index2;
                face.UV3Pos = Index3;

                face.Normal1 = ModelData.uvNormals[Index1];
                face.Normal2 = ModelData.uvNormals[Index2];
                face.Normal3 = ModelData.uvNormals[Index3];

                face.Normal1Pos = Index1;
                face.Normal2Pos = Index2;
                face.Normal3Pos = Index3;
            }

            return face;
        }
        #endregion

        public Patch LoadPatch(Stream stream)
        {
            Patch face = new Patch();

            face.ScalePoint = ReadVertices(stream, true);

            face.UVPoint1 = ReadVertices(stream, true);
            face.UVPoint2 = ReadVertices(stream, true);
            face.UVPoint3 = ReadVertices(stream, true);
            face.UVPoint4 = ReadVertices(stream, true);

            face.R4C4 = ReadVertices(stream, true);
            face.R4C3 = ReadVertices(stream, true);
            face.R4C2 = ReadVertices(stream, true);
            face.R4C1 = ReadVertices(stream, true);
            face.R3C4 = ReadVertices(stream, true);
            face.R3C3 = ReadVertices(stream, true);
            face.R3C2 = ReadVertices(stream, true);
            face.R3C1 = ReadVertices(stream, true);
            face.R2C4 = ReadVertices(stream, true);
            face.R2C3 = ReadVertices(stream, true);
            face.R2C2 = ReadVertices(stream, true);
            face.R2C1 = ReadVertices(stream, true);
            face.R1C4 = ReadVertices(stream, true);
            face.R1C3 = ReadVertices(stream, true);
            face.R1C2 = ReadVertices(stream, true);
            face.R1C1 = ReadVertices(stream, true);

            face.LowestXYZ = ReadVertices(stream);
            face.HighestXYZ = ReadVertices(stream);

            face.Point1 = ReadVertices(stream, true);
            face.Point2 = ReadVertices(stream, true);
            face.Point3 = ReadVertices(stream, true);
            face.Point4 = ReadVertices(stream, true);

            face.PatchStyle = StreamUtil.ReadInt32(stream);
            face.Unknown2 = StreamUtil.ReadInt32(stream); //Material/Lighting
            face.TextureAssigment = StreamUtil.ReadInt16(stream);
            face.LightmapID = StreamUtil.ReadInt16(stream);

            //Always the same
            face.Unknown4 = StreamUtil.ReadInt32(stream); //Negitive one
            face.Unknown5 = StreamUtil.ReadInt32(stream);
            face.Unknown6 = StreamUtil.ReadInt32(stream);

            return face;
        }

        public Vertex3 ReadVertices(Stream stream, bool w = false)
        {
            Vertex3 vertex = new Vertex3();
            vertex.X = StreamUtil.ReadFloat(stream);
            vertex.Y = StreamUtil.ReadFloat(stream);
            vertex.Z = StreamUtil.ReadFloat(stream);
            if (w)
            {
                vertex.W = StreamUtil.ReadFloat(stream);
            }
            return vertex;
        }

        public void SaveVertices(Stream stream, Vertex3 vertex3, bool w = false)
        {
            StreamUtil.WriteFloat32(stream, vertex3.X);
            StreamUtil.WriteFloat32(stream, vertex3.Y);
            StreamUtil.WriteFloat32(stream, vertex3.Z);
            if (w)
            {
                StreamUtil.WriteFloat32(stream, vertex3.W);
            }
        }

        public void SaveModel(string path)
        {
            //glstHandler.SavePDBModelglTF(path, this);
        }
    }

    public struct TextureFlipbook
    {
        public int ImageCount;
        public List<int> ImagePositions;
    }

    public struct Spline
    {
        public Vertex3 LowestXYZ;
        public Vertex3 HighestXYZ;
        public int Unknown1;
        public int SplineSegmentCount;
        public int SplineSegmentPosition;
        public int Unknown2;
    }

    public struct SplinesSegments
    {
        public Vertex3 Point4;
        public Vertex3 Point3;
        public Vertex3 Point2;
        public Vertex3 ControlPoint;
        public Vertex3 ScalingPoint; //Not really sure about that
        public int PreviousSegment;
        public int NextSegment;
        public int SplineParent;
        public Vertex3 LowestXYZ;
        public Vertex3 HighestXYZ;
        public float SegmentDisatnce;
        public float PreviousSegmentsDistance;
        public int Unknown32;
    }

    public struct Instance
    {
        public Vertex3 MatrixCol1;
        public Vertex3 MatrixCol2;
        public Vertex3 MatrixCol3;
        public Vertex3 InstancePosition;
        public Vertex3 Unknown5;
        public Vertex3 Unknown6;
        public Vertex3 Unknown7;
        public Vertex3 Unknown8;
        public Vertex3 Unknown9;
        public Vertex3 Unknown10;
        public Vertex3 Unknown11;
        public Vertex3 RGBA;
        public int ModelID;
        public int UnknownInt18;
        public int UnknownInt19;

        public Vertex3 LowestXYZ;
        public Vertex3 HighestXYZ;

        public int UnknownInt26;
        public int UnknownInt27;
        public int UnknownInt28;
        public int UnknownInt29;
        public int UnknownInt30;
        public int UnknownInt31;
        public int UnknownInt32;
    }

    public struct ParticleInstance
    {
        public Vertex3 Unknown1;
        public Vertex3 Unknown2;
        public Vertex3 Unknown3;
        public Vertex3 Unknown4;
        public int UnknownInt1;
        public int UnknownInt2;
        public int UnknownInt3;
        public int UnknownInt4;
        public int UnknownInt5;
        public int UnknownInt6;
        public int UnknownInt7;
        public int UnknownInt8;
        public int UnknownInt9;
        public int UnknownInt10;
        public int UnknownInt11;
        public int UnknownInt12;
    }

    public struct Model
    {
        public List<StaticMesh> staticMeshes;
    }

    public struct StaticMesh
    {
        public int ChunkID;

        public int StripCount;
        public int Unknown1;
        public int VertexCount;
        public int Unknown3;
        public List<int> Strips;

        public List<UV> uv;
        public List<Vertex3> vertices;
        public List<Face> faces;
        public List<UVNormal> uvNormals;
    }

    //Since there both int 16's They need to be divided by 4096
    public struct UV
    {
        public int X;
        public int Y;
        public int XU;
        public int YU;
    }

    public struct UVNormal
    {
        public int X;
        public int Y;
        public int Z;
    }

    public struct Face
    {
        public Vertex3 V1;
        public Vertex3 V2;
        public Vertex3 V3;

        public int V1Pos;
        public int V2Pos;
        public int V3Pos;

        public UV UV1;
        public UV UV2;
        public UV UV3;

        public int UV1Pos;
        public int UV2Pos;
        public int UV3Pos;

        public UVNormal Normal1;
        public UVNormal Normal2;
        public UVNormal Normal3;

        public int Normal1Pos;
        public int Normal2Pos;
        public int Normal3Pos;
    }

    public struct Patch
    {
        public Vertex3 ScalePoint;

        public Vertex3 UVPoint1;
        public Vertex3 UVPoint2;
        public Vertex3 UVPoint3;
        public Vertex3 UVPoint4;

        public Vertex3 R4C4;
        public Vertex3 R4C3;
        public Vertex3 R4C2;
        public Vertex3 R4C1;
        public Vertex3 R3C4;
        public Vertex3 R3C3;
        public Vertex3 R3C2;
        public Vertex3 R3C1;
        public Vertex3 R2C4;
        public Vertex3 R2C3;
        public Vertex3 R2C2;
        public Vertex3 R2C1;
        public Vertex3 R1C4;
        public Vertex3 R1C3;
        public Vertex3 R1C2;
        public Vertex3 R1C1;

        public Vertex3 LowestXYZ;
        public Vertex3 HighestXYZ;

        public Vertex3 Point1;
        public Vertex3 Point2;
        public Vertex3 Point3;
        public Vertex3 Point4;

        //0 - Reset
        //1 - Standard Snow
        //2 - Standard Off Track?
        //3 - Powered Snow
        //4 - Slow Powered Snow
        //5 - Ice Standard
        //6 - Bounce/Unskiiable //
        //7 - Ice/Water No Trail
        //8 - Glidy(Lots Of snow particels) //
        //9 - Rock 
        //10 - Wall
        //11 - No Trail, Ice Crunch Sound Effect//
        //12 - No Sound, No Trail, Small particle Wake//
        //13 - Off Track Metal (Slidly Slow, Metal Grinding sounds, Sparks)
        //14 - Speed, Grinding Sound//
        //15 - Standard?//
        //16 - Standard Sand
        //17 - ?//
        //18 - Show Off Ramp/Metal
        public int PatchStyle; //Type

        public int Unknown2; // Some Kind of material Assignment Or Lighting
        public int TextureAssigment; // Texture Assigment 
        public int LightmapID;
        public int Unknown4; //Negative one
        public int Unknown5; //Same
        public int Unknown6; //Same
    }

    [System.Serializable]
    public struct Vertex3
    {
        public float X;
        public float Y;
        public float Z;

        public float W;
    }
}
