using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using SSXMultiTool.JsonFiles.Tricky;
using Dummiesman;
using SSXMultiTool.Utilities;
using SSXMultiTool.FileHandlers.LevelFiles.TrickyPS2;

public class TrickyMapInterface : MonoBehaviour
{
    public static TrickyMapInterface Instance;
    public Shader shader;
    public LevelEditorSettings settings;
    public bool NoLightMode;
    public bool HardwareMode;
    public string LoadPath = "";
    public static float Scale = 0.01f; //1f;
    [Header("Parent Objects")]
    public GameObject patchesParent;
    public GameObject splineParent;
    public GameObject instanceParent;
    public GameObject particleInstanceParent;
    public GameObject lightParent;
    [Header("Object Prefabs")]
    public GameObject SplinePrefab;
    public GameObject PatchPrefab;
    public GameObject InstancePrefab;
    public GameObject particleInstancePrefab;
    public GameObject lightPrefab;

    [Header("Json Files")]
    public PatchesJsonHandler PatchJson;
    public SplineJsonHandler SplineJson;
    public InstanceJsonHandler InstanceJson;
    public ModelJsonHandler ModelJson;
    public MaterialJsonHandler materialJson;
    public MaterialBlockJsonHandler materialBlock;
    public LightJsonHandler lightJson;

    public Texture2D ErrorTexture;
    public bool TextureChanged;

    [Header("Lists")]
    public List<Texture2D> textures;
    public List<Texture2D> lightmaps;
    public List<PatchObject> patchObjects = new List<PatchObject>();
    public List<SplineObject> splineObjects = new List<SplineObject>();
    public List<InstanceObject> instanceObjects = new List<InstanceObject>();
    public List<ParticleInstanceObject> particleInstancesObjects = new List<ParticleInstanceObject>();
    public List<ModelObject> modelObjects = new List<ModelObject>();

    public Material ModelMaterial;

    public string ConfigPath;
    public string Version = "0.0.6";

    public GameObject LevelEditorObject;
    public GameObject MaterialLibrayObject;

    private void Awake()
    {
        ConfigPath = UnityEngine.Application.dataPath + "/Config.json";
        Instance = this;
        settings = LevelEditorSettings.Load(ConfigPath);
        if (settings.Version != Version)
        {
            settings = new LevelEditorSettings();
        }
        settings.Save(UnityEngine.Application.dataPath + "/Config.json");
    }

    //Debug Crap
    private void Update()
    {
        if(Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < patchObjects.Count; i++)
            {
                if (patchObjects[i].PatchStyle == 0)
                {
                    patchObjects[i].Renderer.material.SetColor("_Highlight", Color.red);
                }
                if (patchObjects[i].PatchStyle == 6 || patchObjects[i].PatchStyle == 10)
                {
                    patchObjects[i].Renderer.material.SetColor("_Highlight", Color.yellow);
                }
            }
        }
    }

    public void StartEmulator()
    {
        if (File.Exists(settings.EmulatorPath))
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = settings.EmulatorPath;
            string path = settings.LaunchPath;

            if (File.Exists(path))
            {
                if (path.ToLower().Contains(".iso"))
                {
                    startInfo.Arguments = "\"" + path + "\"";
                }
                else if (path.ToLower().Contains(".elf"))
                {
                    startInfo.Arguments = "-elf \"" + path + "\"";
                }
            }
            else
            {
                NotifcationBarUI.instance.ShowNotifcation("No .Elf or ISO Path set", 5);
            }
            Process.Start(startInfo);
        }
        else
        {
            NotifcationBarUI.instance.ShowNotifcation("No Emulator Path Set", 5);
        }
    }

    public void ReloadEditor()
    {
        SceneManager.LoadScene("TrickyLevelLoader", LoadSceneMode.Single);
    }

    public void ToggleLightModeAll()
    {
        NoLightMode = !NoLightMode;
        for (int i = 0; i < patchObjects.Count; i++)
        {
            patchObjects[i].ToggleLightingMode();
        }
    }

    public void ToggleTextureEmulation()
    {
        HardwareMode = !HardwareMode;
        for (int i = 0; i < patchObjects.Count; i++)
        {
            patchObjects[i].ToggleHardwareMode();
        }
    }

    public void ForceUpdateAllTextures()
    {
        textures = new List<Texture2D>();


        for (int i = 0; i < patchObjects.Count; i++)
        {
            patchObjects[i].UpdateTexture(patchObjects[i].TextureAssigment);
        }
    }

    public void QuitApp()
    {
        UnityEngine.Application.Quit();
    }

    public void HideObjects(int a)
    {
        if (a == 0)
        {
            patchesParent.SetActive(!patchesParent.activeSelf);
        }
        if (a == 1)
        {
            splineParent.SetActive(!splineParent.activeSelf);
        }
        if (a == 2)
        {
            instanceParent.SetActive(!instanceParent.activeSelf);
        }
    }

    public void UpdateNURBSRes()
    {
        for (int i = 0; i < patchObjects.Count; i++)
        {
            patchObjects[i].GetComponent<PatchObject>().LoadNURBSpatch();
        }
    }

    public void RegenerateSplineMesh()
    {
        for (int i = 0; i < splineObjects.Count; i++)
        {
            for (int a = 0; a < splineObjects[i].splineSegmentObjects.Count; a++)
            {
                splineObjects[i].splineSegmentObjects[a].RegenerateModel();
            }
        }
    }

    public void ShowMatieralLibrary()
    {
        SelectorScript.instance.active = false;
        MaterialLibrayObject.SetActive(true);
        MaterialLibrayObject.GetComponent<MaterialPanel>().GenerateButtons();
        LevelEditorObject.SetActive(false);
    }

    public void ShowLeveEditor()
    {
        SelectorScript.instance.active = true;
        MaterialLibrayObject.SetActive(false);
        LevelEditorObject.SetActive(true);
    }

    public Texture2D GrabLightmapTexture(Vector4 lightmapPoint, int ID)
    {
        int XCord = (int)(lightmapPoint.x * lightmaps[ID].width);
        int YCord = (int)(lightmapPoint.y * lightmaps[ID].height);
        int Width = (int)(lightmapPoint.z * lightmaps[ID].width);
        int Height = (int)(lightmapPoint.w * lightmaps[ID].height);
        Texture2D LightmapGrab = new Texture2D(Width, Height);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var Colour = lightmaps[ID].GetPixel(XCord + x, YCord + y);
                LightmapGrab.SetPixel(x, y, Colour);
            }
        }
        LightmapGrab.Apply();

        Texture2D TenByTen = new Texture2D(Width + 2, Height + 2);

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var Colour = LightmapGrab.GetPixel(x, y);
                TenByTen.SetPixel(x+1, y+1, Colour);
            }
        }

        for (int i = 0; i < Width; i++)
        {
            var Colour = LightmapGrab.GetPixel(0, i);
            TenByTen.SetPixel(0, i+1, Colour);

            Colour = LightmapGrab.GetPixel(i, Height-1);
            TenByTen.SetPixel(i+1, Height+1, Colour);
        }

        for (int i = 0; i < Height; i++)
        {
            var Colour = LightmapGrab.GetPixel(i, 0);
            TenByTen.SetPixel(i + 1, 0, Colour);

            Colour = LightmapGrab.GetPixel(Width - 1, i);
            TenByTen.SetPixel(Width + 1, i + 1, Colour);
        }

        //Probably better to replace with averages of what the corners should look like
        var Colour1 = LightmapGrab.GetPixel(0, 0);
        TenByTen.SetPixel(0, 0, Colour1);

        Colour1 = LightmapGrab.GetPixel(Width-1, 0);
        TenByTen.SetPixel(Width + 1, 0, Colour1);

        Colour1 = LightmapGrab.GetPixel(0, Height - 1);
        TenByTen.SetPixel(0, Height + 1, Colour1);

        Colour1 = LightmapGrab.GetPixel(Width - 1, Height - 1);
        TenByTen.SetPixel(Width + 1, Height + 1, Colour1);

        TenByTen.Apply();
        //TenByTen.filterMode = FilterMode.Bilinear;
        return TenByTen;
    }

    #region Load Stuff
    public void OpenFileMap()
    {
        OpenFileDialog saveFileDialog = new OpenFileDialog()
        {
            Filter = "Map Config File (*.ssx)|*.ssx|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = false
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            LoadMapFiles(saveFileDialog.FileName);
        }
    }

    void LoadMapFiles(string StringPath)
    {
        SSXTrickyConfig trickyConfig = SSXTrickyConfig.Load(StringPath);
        if (trickyConfig.Version == 3)
        {
            StringPath = Path.GetDirectoryName(StringPath);
            LoadPath = StringPath;
            LoadTextures(StringPath + "\\Textures");
            Loadlightmaps(StringPath + "\\Lightmaps");
            LoadPatches(StringPath + "\\Patches.json");
            LoadSplines(StringPath + "\\Splines.json");
            materialBlock = MaterialBlockJsonHandler.Load(StringPath + "\\MaterialBlocks.json");
            materialJson = MaterialJsonHandler.Load(StringPath + "\\Material.json");
            LoadModels(StringPath + "\\ModelHeaders.json");
            LoadInstances(StringPath + "\\Instances.json");
            LoadLighting(StringPath + "\\Lights.json");
            SkyboxManager.Instance.LoadSkyboxData(StringPath + "\\Skybox\\");
            LoadAndDisplayLTG(StringPath + "\\Original\\ltg.ltg");
            NotifcationBarUI.instance.ShowNotifcation("Project Loaded", 5);
        }
        else
        {
            NotifcationBarUI.instance.SendMessage("Incorrect Project Version");
        }
    }

    void LoadPatches(string PatchPath)
    {
        PatchJson = new PatchesJsonHandler();
        PatchJson = PatchesJsonHandler.Load(PatchPath);
        for (int i = 0; i < PatchJson.patches.Count; i++)
        {
            GameObject gameObject = Instantiate(PatchPrefab, patchesParent.transform);
            var Patch = PatchJson.patches[i];
            var PatchHolder = gameObject.GetComponent<PatchObject>();
            PatchHolder.LoadPatch(Patch);
            gameObject.transform.name = Patch.PatchName + " (" + i + ")";
            patchObjects.Add(PatchHolder);
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

    void LoadInstances(string Path)
    {
        InstanceJson = new InstanceJsonHandler();
        InstanceJson = InstanceJsonHandler.Load(Path);
        instanceObjects = new List<InstanceObject>();
        for (int i = 0; i < InstanceJson.instances.Count; i++)
        {
            var TempGameObject = Instantiate(InstancePrefab, instanceParent.transform);
            TempGameObject.transform.name = InstanceJson.instances[i].InstanceName + " (" + i.ToString() + ")";
            TempGameObject.GetComponent<InstanceObject>().LoadInstance(InstanceJson.instances[i]);
            instanceObjects.Add(TempGameObject.GetComponent<InstanceObject>());
        }
    }

    void LoadParticleInstances()
    {

    }

    void LoadTextureFlipbooks()
    {

    }

    void LoadLighting(string Path)
    {
        lightJson = new LightJsonHandler();
        lightJson = LightJsonHandler.Load(Path);
        //instanceObjects = new List<InstanceObject>();
        for (int i = 0; i < lightJson.LightJsons.Count; i++)
        {
            var TempGameObject = Instantiate(lightPrefab, lightParent.transform);
            TempGameObject.transform.name = lightJson.LightJsons[i].LightName + " (" + i.ToString() + ")";
            TempGameObject.GetComponent<LightingObject>().LoadLightingObject(lightJson.LightJsons[i]);
            //instanceObjects.Add(TempGameObject.GetComponent<InstanceObject>());
        }
    }

    void LoadSplines(string path)
    {
        SplineJson = new SplineJsonHandler();
        SplineJson = SplineJsonHandler.Load(path);
        for (int i = 0; i < SplineJson.SplineJsons.Count; i++)
        {
            var TempSplineData = SplineJson.SplineJsons[i];
            GameObject TempSpline = Instantiate(SplinePrefab, splineParent.transform);
            TempSpline.transform.name = TempSplineData.SplineName + " (" + i.ToString() + ")";
            TempSpline.GetComponent<SplineObject>().LoadSpline(TempSplineData);
            splineObjects.Add(TempSpline.GetComponent<SplineObject>());
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

    void Loadlightmaps(string Folder)
    {
        string[] Files = Directory.GetFiles(Folder);
        lightmaps = new List<Texture2D>();
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
                    NewImage.filterMode = FilterMode.Point;
                }
                Texture2D correctedTexture = new Texture2D(NewImage.width, NewImage.height);
                for (int x = 0; x < NewImage.width; x++)
                {
                    for (int y = 0; y < NewImage.height; y++)
                    {
                        correctedTexture.SetPixel(x, y, NewImage.GetPixel(x, NewImage.height-1 - y));
                    }
                }
                correctedTexture.Apply();
                lightmaps.Add(correctedTexture);
            }
        }
    }

    public void ReloadTextures()
    {
        if (LoadPath != "")
        {
            string[] Files = Directory.GetFiles(LoadPath + "\\Textures");
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
                    }
                    textures.Add(NewImage);
                }
            }
            for (int i = 0; i < patchObjects.Count; i++)
            {
                patchObjects[i].UpdateTexture(patchObjects[i].TextureAssigment);
            }
        }
    }

    void LoadSkyBox()
    {

    }

    void LoadLighting()
    {

    }

    public GameObject LTGParent;
    public GameObject MainBox;
    public GameObject NodeBox;
    void LoadAndDisplayLTG(string path)
    {
        LTGHandler handler = new LTGHandler();
        handler.LoadLTG(path);

        for (int y = 0; y < handler.pointerListCount; y++)
        {
            for (int x = 0; x < handler.pointerCount; x++)
            {
                if (!handler.mainBboxes[x, y].Equals(new LTGHandler.mainBbox()))
                {
                    var MainBoxTemp = Instantiate(MainBox, LTGParent.transform);
                    MainBoxTemp.transform.position = JsonUtil.NumericVector3ToUnity(handler.mainBboxes[x, y].WorldBounds3*TrickyMapInterface.Scale);
                    for (int y1 = 0; y1 < handler.nodeBoxWidth; y1++)
                    {
                        for (int x1 = 0; x1 < handler.nodeBoxWidth; x1++)
                        {
                            var NodeBoxTemp = Instantiate(NodeBox, LTGParent.transform);
                            NodeBoxTemp.transform.position = JsonUtil.NumericVector3ToUnity(handler.mainBboxes[x, y].nodeBBoxes[x1, y1].WorldBounds3 * TrickyMapInterface.Scale);
                        }
                    }
                }
            }
        }

        LTGParent.transform.localScale = new Vector3(-1, 1, 1);
        LTGParent.transform.localEulerAngles = new Vector3(-90, -90, -90);
    }

    #endregion

    #region Save Stuff

    public void SaveFileMap()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = "Map Config File (*.ssx)|*.ssx|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = false
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            SaveFiles(saveFileDialog.FileName);
        }
    }

    public void SaveFiles(string StringPath)
    {
        StringPath = Path.GetDirectoryName(StringPath);
        SavePatches(StringPath + "\\Patches.json");
        SaveSplines(StringPath + "\\Splines.json");
        SaveInstances(StringPath + "\\Instances.json");
        NotifcationBarUI.instance.ShowNotifcation("Project Saved" ,5);
    }

    public void SavePatches(string PatchPath)
    {
        PatchJson = new PatchesJsonHandler();
        PatchJson.patches = new List<PatchesJsonHandler.PatchJson>();
        for (int i = 0; i < patchObjects.Count; i++)
        {
            PatchJson.patches.Add(patchObjects[i].GeneratePatch());
        }
        PatchJson.CreateJson(PatchPath);
    }

    public void SaveSplines(string SplinePath)
    {
        SplineJson = new SplineJsonHandler();
        SplineJson.SplineJsons = new List<SplineJsonHandler.SplineJson>();
        for (int i = 0; i < splineObjects.Count; i++)
        {
            SplineJson.SplineJsons.Add(splineObjects[i].GenerateSpline());
        }
        SplineJson.CreateJson(SplinePath);
    }

    public void SaveInstances(string InstancePath)
    {
        InstanceJson = new InstanceJsonHandler();
        InstanceJson.instances = new List<InstanceJsonHandler.InstanceJson>();
        for (int i = 0; i < instanceObjects.Count; i++)
        {
            InstanceJson.instances.Add(instanceObjects[i].GenerateInstance());
        }
        InstanceJson.CreateJson(InstancePath);
    }

    #endregion
}
