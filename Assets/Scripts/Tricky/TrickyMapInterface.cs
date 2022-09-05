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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

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

    string BigPath;
    bool BigImported;
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
            if(Directory.Exists(UnityEngine.Application.persistentDataPath + "\\TempExtracted"))
            {
                Directory.Delete(UnityEngine.Application.persistentDataPath + "\\TempExtracted", true);
            }
            Directory.CreateDirectory(UnityEngine.Application.persistentDataPath +"\\TempExtracted");
            bigHandler.ExtractBig(UnityEngine.Application.persistentDataPath + "\\TempExtracted");
            string[] Paths = Directory.GetFiles(UnityEngine.Application.persistentDataPath + "\\TempExtracted", "*.map", SearchOption.AllDirectories);
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
                bigHandler.LoadFolder(UnityEngine.Application.persistentDataPath + "\\TempExtracted");
                bigHandler.bigType = BigType.C0FB;
                bigHandler.BuildBig(openFileDialog.FileName);
            }
        }
    }

    public void QuitApp()
    {
        UnityEngine.Application.Quit();
    }

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

        //PBDHandler = new PBDHandler();
        PBDHandler.NumTextures = sshHandler.sshImages.Count;
        PBDHandler.Patches = patchList;
        PBDHandler.Save(path);
    }

    void LoadMapFiles(string Path)
    {
        LoadPath = Path.Substring(0, Path.Length-4);
        PBDHandler = new PBDHandler();
        mMapHandler = new MapHandler();
        //LoadLighting();
        LoadMap();
        if (LoadPBD())
        {
            LoadTextures();
            LoadPatches();
            LoadSplines();
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
                File.Copy(LoadPath+  " - Copy.pbd", LoadPath + ".pbd");
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
        for (int i = 0; i < PBDHandler.splines.Count; i++)
        {
            var Temp = PBDHandler.splines[i];
            List<SplinesSegments> Segments = new List<SplinesSegments>();

            for (int a = Temp.SplineSegmentPosition; a < Temp.SplineSegmentCount + Temp.SplineSegmentPosition; a++)
            {
                Segments.Add(PBDHandler.splinesSegments[a]);
            }

            GameObject TempSpline = Instantiate(SplinePrefab, splineParent.transform);
            TempSpline.transform.name = mMapHandler.Splines[i].Name + " (" + i.ToString()+ ")";
            TempSpline.GetComponent<SplineObject>().LoadSpline(Temp, Segments);
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

    Vector3 VertexToVector(Vertex3 vertex3)
    {
        return new Vector3(vertex3.X, vertex3.Z, vertex3.Y);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
