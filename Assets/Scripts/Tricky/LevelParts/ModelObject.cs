using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelObject
{
    public string ModelName;
    public Vector3 Scale;
    public List<Mesh> meshes = new List<Mesh>();

    public static Material GenerateMaterial(int block, int meshID)
    {
        Material material = new Material(Shader.Find("Standard"));
        material.CopyPropertiesFromMaterial(TrickyMapInterface.Instance.ModelMaterial);
        int MaterialID = 0;
        if(TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints.Count-1>= meshID)
        {
            MaterialID = TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints[meshID];
        }
        else
        {
            MaterialID = TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints[0];
        }
        int TextureID = TrickyMapInterface.Instance.materialJson.MaterialsJsons[MaterialID].TextureID;
        material.mainTexture = TrickyMapInterface.Instance.textures[TextureID];
        material.SetTexture("_EmissionMap", material.mainTexture);
        return material;
    }
}
