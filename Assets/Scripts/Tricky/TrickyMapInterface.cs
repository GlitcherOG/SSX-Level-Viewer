using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers;
using SSX_Modder.FileHandlers.MapEditor;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class TrickyMapInterface : MonoBehaviour
{
    public static TrickyMapInterface Instance;
    public LevelEditorSettings settings;
    public SSHHandler sshHandler;
    public SSHHandler skyboxHandler;
    public SSHHandler lightingHandler;
    PBDHandler PBDHandler;
    MapHandler mMapHandler;
    public bool NoLightMode;
    public string LoadPath;
    public static float Scale = 0.025f;
    public GameObject patchesParent;
    public GameObject splineParent;

    public GameObject SplinePrefab;
    public GameObject PatchPrefab;

    public Texture2D ErrorTexture;
    public bool TextureChanged;

    public List<Texture2D> textures;
    public List<PatchObject> patchObjects = new List<PatchObject>();
    public List<SplineObject> splineObjects = new List<SplineObject>();

    string BigPath;
    bool BigImported;
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
        for (int i = 0; i < sshHandler.sshImages.Count; i++)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            MemoryStream stream = new MemoryStream();
            sshHandler.sshImages[i].bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] TempByte = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(TempByte, 0, TempByte.Length);
            texture2D.LoadImage(TempByte, true);
            textures.Add(texture2D);
        }

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

    #region Load Stuff
    public void OpenFileMap()
    {
        OpenFileDialog saveFileDialog = new OpenFileDialog()
        {
            Filter = "Map File (*.map)|*.map|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = false
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            LoadMapFiles(saveFileDialog.FileName);
        }
    }

    public void ExtractBig()
    {
        BigHandler bigHandler = new BigHandler();
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "Big Archive (*.big)|*.big|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = false
        };
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            bigHandler.LoadBig(openFileDialog.FileName);
            if (Directory.Exists(UnityEngine.Application.dataPath + "\\TempExtracted"))
            {
                Directory.Delete(UnityEngine.Application.dataPath + "\\TempExtracted", true);
            }
            Directory.CreateDirectory(UnityEngine.Application.dataPath + "\\TempExtracted");
            bigHandler.ExtractBig(UnityEngine.Application.dataPath + "\\TempExtracted");
            string[] Paths = Directory.GetFiles(UnityEngine.Application.dataPath + "\\TempExtracted", "*.map", SearchOption.AllDirectories);
            for (int i = 0; i < Paths.Length; i++)
            {
                if (Paths[i].Contains(".map"))
                {
                    BigImported = true;
                    BigPath = Paths[i];
                    LoadMapFiles(Paths[i]);
                    break;
                }
            }
        }
    }

    void LoadMapFiles(string Path)
    {
        LoadPath = Path.Substring(0, Path.Length - 4);
        PBDHandler = new PBDHandler();
        mMapHandler = new MapHandler();
        //LoadLighting();
        LoadMap();
        if (LoadPBD())
        {
            LoadTextures();
            LoadPatches();
            LoadSplines();
            LoadTextureFlipbooks();
            //LoadInstances();
            LoadParticleInstances();
        }
        try
        {
            LoadSkyBox();
        }
        catch
        {
            NotifcationBarUI.instance.ShowNotifcation("No Skybox Textures Detected", 5f);
        }
    }

    void LoadInstances()
    {
        for (int i = 0; i < PBDHandler.Instances.Count; i++)
        {
            SpawnPoints(ConversionTools.Vertex3ToVector3(PBDHandler.Instances[i].Unknown4) * Scale, mMapHandler.InternalInstances[i].Name);
        }
    }

    void LoadParticleInstances()
    {
        for (int i = 0; i < PBDHandler.particleInstances.Count; i++)
        {
            SpawnPoints(ConversionTools.Vertex3ToVector3(PBDHandler.particleInstances[i].Unknown4) * Scale, mMapHandler.ParticleInstances[i].Name);
        }
    }

    void LoadTextureFlipbooks()
    {

    }

    bool LoadPBD()
    {
        try
        {
            if (File.Exists(LoadPath + " - Copy.pbd"))
            {
                File.Delete(LoadPath + ".pbd");
                while (File.Exists(LoadPath + ".pbd"))
                {

                }
                File.Copy(LoadPath + " - Copy.pbd", LoadPath + ".pbd");
            }
            PBDHandler.loadandsave(LoadPath + ".pbd");
            return true;
        }
        catch
        {
            return false;
        }
    }

    void LoadSplines()
    {
        splineObjects = new List<SplineObject>();
        for (int i = 0; i < PBDHandler.splines.Count; i++)
        {
            var Temp = PBDHandler.splines[i];

            List<SplinesSegments> Segments = new List<SplinesSegments>();
            for (int a = Temp.SplineSegmentPosition; a < Temp.SplineSegmentCount + Temp.SplineSegmentPosition; a++)
            {
                Segments.Add(PBDHandler.splinesSegments[a]);
            }

            GameObject TempSpline = Instantiate(SplinePrefab, splineParent.transform);
            TempSpline.transform.name = mMapHandler.Splines[i].Name + " (" + i.ToString() + ")";
            TempSpline.GetComponent<SplineObject>().LoadSpline(Temp, Segments);
            splineObjects.Add(TempSpline.GetComponent<SplineObject>());
        }
    }

    void LoadPatches()
    {
        var TempData = PBDHandler.Patches;
        for (int i = 0; i < TempData.Count; i++)
        {
            GameObject gameObject = Instantiate(PatchPrefab, patchesParent.transform);
            var Patch = TempData[i];
            var PatchHolder = gameObject.GetComponent<PatchObject>();
            PatchHolder.LoadPatch(Patch, mMapHandler.Patchs[i].Name.TrimEnd(' '));
            gameObject.transform.name = mMapHandler.Patchs[i].Name.TrimEnd(' ') + " (" + i + ")";
            patchObjects.Add(PatchHolder);
        }
    }

    void LoadMap()
    {
        mMapHandler.Load(LoadPath + ".map");
    }

    void LoadTextures()
    {
        sshHandler = new SSHHandler();
        sshHandler.LoadSSH(LoadPath + ".ssh");
        textures = new List<Texture2D>();
        for (int i = 0; i < sshHandler.sshImages.Count; i++)
        {
            sshHandler.BrightenBitmap(i);
            Texture2D texture2D = new Texture2D(1, 1);
            MemoryStream stream = new MemoryStream();
            sshHandler.sshImages[i].bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] TempByte = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(TempByte, 0, TempByte.Length);
            texture2D.LoadImage(TempByte, true);
            textures.Add(texture2D);
        }
    }
    void LoadSkyBox()
    {
        skyboxHandler = new SSHHandler();
        skyboxHandler.LoadSSH(LoadPath + "_sky.ssh");
    }

    void LoadLighting()
    {
        lightingHandler = new SSHHandler();
        sshHandler.LoadSSH(LoadPath + "_L.ssh");
    }

    #endregion

    #region Save Stuff
    void SavePBD(string path)
    {
        mMapHandler.Patchs.Clear();
        List<Patch> patchList = new List<Patch>();
        for (int i = 0; i < patchObjects.Count; i++)
        {
            var TempLinker = new LinkerItem();
            patchList.Add(patchObjects[i].GeneratePatch());
            TempLinker.Name = patchObjects[i].PatchName;
            TempLinker.UID = i;
            TempLinker.Ref = 1;
            TempLinker.Hashvalue = "0";
            mMapHandler.Patchs.Add(TempLinker);
        }

        List<Spline> splineList = new List<Spline>();
        List<SplinesSegments> splinesSegmentsList = new List<SplinesSegments>();
        int SegmentPos = 0;
        for (int i = 0; i < splineObjects.Count; i++)
        {
            splineList.Add(splineObjects[i].GenerateSpline(SegmentPos));
            splinesSegmentsList.InsertRange(splinesSegmentsList.Count, splineObjects[i].GetSegments(SegmentPos, i));
            SegmentPos += splineObjects[i].splineSegmentObjects.Count;
        }

        //PBDHandler = new PBDHandler();
        PBDHandler.NumTextures = sshHandler.sshImages.Count;
        PBDHandler.Patches = patchList;
        PBDHandler.splines = splineList;
        PBDHandler.splinesSegments = splinesSegmentsList;
        PBDHandler.Save(path);
    }

    public void SaveFileMap()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = "Map File (*.map)|*.map|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = false
        };
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            SaveFiles(saveFileDialog.FileName);
            NotifcationBarUI.instance.ShowNotifcation("Exported Map", 5f);
        }
    }

    public void SaveFiles(string path)
    {
        SavePBD(path.Substring(0, path.Length - 4) + ".pbd");

        mMapHandler.Save(path);
        if (TextureChanged)
        {
            for (int i = 0; i < sshHandler.sshImages.Count; i++)
            {
                sshHandler.DarkenImage(i);
            }
            sshHandler.SaveSSH(path.Substring(0, path.Length - 4) + ".ssh");
        }

    }

    public void MakeBig()
    {
        if (BigImported)
        {
            BigHandler bigHandler = new BigHandler();
            SaveFileDialog openFileDialog = new SaveFileDialog()
            {
                Filter = "Big Archive (*.big)|*.big|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFiles(BigPath);
                bigHandler.LoadFolder(UnityEngine.Application.dataPath + "\\TempExtracted");
                bigHandler.bigType = BigType.C0FB;
                bigHandler.BuildBig(openFileDialog.FileName);
            }
        }
    }

    #endregion
}
