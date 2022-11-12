using Dummiesman;
using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public bool active = true;
    string LoadPath;
    public static SkyboxManager Instance;

    public Material SkyboxMaterial;
    public GameObject Skybox;
    
    public ModelJsonHandler ModelJson;
    public MaterialJsonHandler materialJson;
    public MaterialBlockJsonHandler materialBlock;
    public List<Texture2D> textures;
    public List<ModelObject> modelObjects = new List<ModelObject>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            //Skybox.transform.eulerAngles = new Vector3(-90, 0, 0);
        }
    }

    public void LoadSkyboxData(string StringPath)
    {
        StringPath = Path.GetDirectoryName(StringPath);
        LoadPath = StringPath;
        materialBlock = MaterialBlockJsonHandler.Load(StringPath + "\\MaterialBlocks.json");
        materialJson = MaterialJsonHandler.Load(StringPath + "\\Material.json");
        LoadModels(StringPath + "\\ModelHeaders.json");
        LoadTextures(StringPath + "\\Textures");

        for (int i = 0; i < modelObjects[0].meshes.Count; i++)
        {
            var NewObject = new GameObject();
            NewObject.layer = 8;
            NewObject.transform.parent = Skybox.transform;
            NewObject.transform.localPosition = new Vector3(0, 0, 0);
            NewObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            var Renderer = NewObject.AddComponent<MeshRenderer>();
            var Filter = NewObject.AddComponent<MeshFilter>();
            Material newSkyboxMat = new Material(SkyboxMaterial);
            newSkyboxMat.SetTexture("_MainTexture", textures[i]);
            Filter.mesh = modelObjects[0].meshes[i];
            Renderer.material = newSkyboxMat;
        }
    }

    void LoadModels(string Path)
    {
        modelObjects = new List<ModelObject>();
        ModelJson = ModelJsonHandler.Load(Path);
        for (int i = 0; i < ModelJson.ModelJsons.Count; i++)
        {
            var TempModelJson = ModelJson.ModelJsons[i];
            ModelObject mObject = new ModelObject();
            GameObject gameObject = new OBJLoader().Load(LoadPath + "/Models/" + i.ToString() + ".obj", null);
            var Meshes = gameObject.GetComponentsInChildren<MeshFilter>();
            for (int a = 0; a < Meshes.Length; a++)
            {
                mObject.meshes.Add(Meshes[a].mesh);
            }
            Destroy(gameObject);

            mObject.ModelName = TempModelJson.ModelName;
            modelObjects.Add(mObject);
        }
    }

    void LoadTextures(string Folder)
    {
        string[] Files = Directory.GetFiles(Folder);
        textures = new List<Texture2D>();
        for (int i = 0; i < Files.Length; i++)
        {
            Texture2D NewImage = new Texture2D(1, 1);
            if (Files[i].ToLower().Contains(".png"))
            {
                using (Stream stream = File.Open(Files[i], FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    NewImage.LoadImage(bytes);
                    //NewImage.wrapMode = TextureWrapMode.MirrorOnce;
                }
                textures.Add(NewImage);
            }
        }
    }
}
