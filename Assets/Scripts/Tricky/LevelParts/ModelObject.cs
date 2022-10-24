using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelObject
{
    public string ModelName;
    public List<Mesh> meshes = new List<Mesh>();

    public static Material GenerateMaterial(int block, int meshID)
    {
        Material material = new Material(Shader.Find("ModelShader"));
        material.CopyPropertiesFromMaterial(TrickyMapInterface.Instance.ModelMaterial);
        int MaterialID = 0;
        if(TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints.Count-1>= meshID)
        {
            MaterialID = TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints[meshID];
        }
        else
        {
            MaterialID = TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints[TrickyMapInterface.Instance.materialBlock.MaterialBlockJsons[block].ints.Count - 1];
        }
        int TextureID = TrickyMapInterface.Instance.materialJson.MaterialsJsons[MaterialID].TextureID;
        material.SetTexture("_MainTexture", GetTexture(TextureID));
        return material;
    }

    public static Texture2D GetTexture(int TextureID)
    {
        Texture2D texture = null;
        try
        {
            texture = TrickyMapInterface.Instance.textures[TextureID];

        }
        catch
        {
            texture = TrickyMapInterface.Instance.ErrorTexture;
        }
        return texture;
    }
}
