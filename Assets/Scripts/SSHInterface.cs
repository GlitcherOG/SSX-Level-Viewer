using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSX_Modder.FileHandlers;
using UnityEngine.UI;
using System.IO;
public class SSHInterface : MonoBehaviour
{
    public string Load;
    public RawImage image;
    Texture2D texture2D;
    public SSHHandler handler;
    // Start is called before the first frame update
    void Start()
    {
        handler = new SSHHandler();
        LoadTexture();
        LoadImage(0);
    }

    public void LoadTexture()
    {
        handler.LoadSSH(Load);
    }

    public void LoadImage(int Pos)
    {
        texture2D = new Texture2D(1, 1);
        MemoryStream stream = new MemoryStream();
        handler.sshImages[Pos].bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        byte[] TempByte = new byte[stream.Length];
        stream.Position = 0;
        stream.Read(TempByte, 0, TempByte.Length);
        texture2D.LoadImage(TempByte, true);
        texture2D.filterMode = FilterMode.Point;
        image.texture = texture2D;
    }
}
