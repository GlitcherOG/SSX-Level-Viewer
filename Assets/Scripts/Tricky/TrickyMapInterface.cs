using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using SSXMultiTool.JsonFiles.Tricky;

public class TrickyMapInterface : MonoBehaviour
{
    public static TrickyMapInterface Instance;
    public LevelEditorSettings settings;
    public bool NoLightMode;
    public string LoadPath;
    public static float Scale = 0.01f;
    [Header("Parent Objects")]
    public GameObject patchesParent;
    public GameObject splineParent;
    public GameObject instanceParent;
    public GameObject particleInstanceParent;
    [Header("Object Prefabs")]
    public GameObject SplinePrefab;
    public GameObject PatchPrefab;
    public GameObject InstancePrefab;
    public GameObject particleInstancePrefab;
    [Header("Json Files")]
    public PatchesJsonHandler PatchJson;
    public SplineJsonHandler SplineJson;

    public Texture2D ErrorTexture;
    public bool TextureChanged;

    [Header("Lists")]
    public List<Texture2D> textures;
    public List<PatchObject> patchObjects = new List<PatchObject>();
    public List<SplineObject> splineObjects = new List<SplineObject>();
    public List<InstanceObject> instanceObjects = new List<InstanceObject>();
    public List<ParticleInstanceObject> particleInstancesObjects = new List<ParticleInstanceObject>();

    public string ConfigPath;
    public string Version = "0.0.3";

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
    }


    void SpawnPoints(Vector3 vector3, string Name)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = vector3;
        gameObject.transform.parent = this.gameObject.transform;
        gameObject.name = Name;
        gameObject.AddComponent<CubeID>();
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
        StringPath = Path.GetDirectoryName(StringPath);
        LoadTextures(StringPath+ "\\Textures");
        LoadPatches(StringPath + "\\Patches.json");
        LoadSplines(StringPath + "\\Splines.json");
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

    void LoadInstances()
    {

    }

    void LoadParticleInstances()
    {

    }

    void LoadTextureFlipbooks()
    {

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
                }
                textures.Add(NewImage);
            }
        }
    }
    void LoadSkyBox()
    {

    }

    void LoadLighting()
    {

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

    #endregion
}
