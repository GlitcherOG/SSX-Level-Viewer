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
    public static float Scale = 0.1f;
    public GameObject patches;
    public Texture2D ErrorTexture;


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
            SavePBD(saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.Length - 4) + ".pbd");
            sshHandler.SaveSSH(saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.Length - 4) + ".ssh");
            MessageBox.Show("Exported Map");
        }
    }
    string BigPath;
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
            BigPath = openFileDialog.FileName;
            bigHandler.LoadBig(openFileDialog.FileName);
            Directory.CreateDirectory(openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length-4));
            bigHandler.ExtractBig(openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4));
            Process.Start(openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4));
        }
    }

    public void ForceUpdateAllTextures()
    {
        textures = new List<Texture2D>();
        for (int i = 0; i < sshHandler.sshImages.Count; i++)
        {
            //sshHandler.BrightenBitmap(i);
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
        BigHandler bigHandler = new BigHandler();
        if (BigPath!=""||BigPath==null)
        {
            bigHandler.LoadFolderC0FB(BigPath.Substring(0, BigPath.Length - 4));
            bigHandler.bigType = BigType.C0FB;
            bigHandler.BuildBig(BigPath);
        }
    }

    public void QuitApp()
    {
        UnityEngine.Application.Quit();
    }

    void SavePBD(string path)
    {
        List<Patch> patchList = new List<Patch>();
        for (int i = 0; i < patchObjects.Count; i++)
        {
            patchList.Add(patchObjects[i].GeneratePatch());
        }

        PBDHandler = new PBDHandler();
        PBDHandler.Patches = patchList;
        PBDHandler.Save(path);
    }

    void LoadMapFiles(string Path)
    {
        LoadPath = Path.Substring(0, Path.Length-4);
        PBDHandler = new PBDHandler();
        mMapHandler = new MapHandler();
        //LoadSkyBox();
        //LoadLighting();
        LoadMap();
        if (LoadPBD())
        {
            LoadTextures();
            LoadPatches();
            LoadSplines();
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
            SpawnPoints(new Vector3(Temp.X1, Temp.Z1, Temp.Y1)*Scale, mMapHandler.Splines[i].Name);
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
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = patches.transform;
            gameObject.transform.tag = "Patch";
            var Patch = TempData[i];
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshCollider>();
            var RendererTemp = gameObject.AddComponent<MeshRenderer>();
            RendererTemp.receiveShadows = false;
            var PatchHolder = gameObject.AddComponent<PatchObject>();
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
        sshHandler.LoadSSH(LoadPath + "_sky.ssh");
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
