using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

public class SkyboxManager : MonoBehaviour
{
    public bool active = true;
    string LoadPath;
    public static SkyboxManager Instance;
    public Camera SkyboxCamera;

    public Material SkyboxMaterial;
    public GameObject Skybox;
    
    public PrefabJsonHandler PrefabJson;
    public MaterialJsonHandler materialJson;
    public List<Texture2D> textures;
    public List<PrefabObject> modelObjects = new List<PrefabObject>();
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
            //Located in Mouse Look Script
            //Skybox.transform.eulerAngles = new Vector3(-90, 0, 0);
        }
    }

    public void LoadSkyboxData(string StringPath)
    {
        StringPath = Path.GetDirectoryName(StringPath);
        LoadPath = StringPath;
        LoadTextures(StringPath + "\\Textures");
        materialJson = MaterialJsonHandler.Load(StringPath + "\\Material.json");
        LoadModels(StringPath + "\\Prefabs.json");

        if (modelObjects.Count != 0)
        {
            Skybox = modelObjects[0].GeneratePrefab(true);
            Skybox.transform.parent = transform;
            Skybox.transform.localPosition = Vector3.zero;
            Skybox.transform.localEulerAngles = Vector3.zero;

        }
        SkyboxCamera.backgroundColor = textures[0].GetPixel(textures[0].width - 1, textures[0].height - 1);
    }

    void LoadModels(string Path)
    {
        modelObjects = new List<PrefabObject>();
        PrefabJson = PrefabJsonHandler.Load(Path);
        for (int i = 0; i < PrefabJson.PrefabJsons.Count; i++)
        {
            var TempModelJson = PrefabJson.PrefabJsons[i];
            PrefabObject mObject = new PrefabObject();
            mObject.LoadPrefab(TempModelJson, true);
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
                    //NewImage.filterMode = FilterMode.Point;
                    //NewImage.wrapMode = TextureWrapMode.MirrorOnce;
                }
                textures.Add(NewImage);
            }
        }
    }
}
