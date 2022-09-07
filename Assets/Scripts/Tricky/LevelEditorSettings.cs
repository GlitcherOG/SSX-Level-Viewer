using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelEditorSettings
{
    public string Version;
    public KeyCode Cameraforward = KeyCode.W;
    public KeyCode Camerabackward = KeyCode.S;
    public KeyCode Cameraleft = KeyCode.A;
    public KeyCode Cameraright = KeyCode.D;
    public KeyCode Cameradown = KeyCode.C;
    public KeyCode Cameraup = KeyCode.Space;
    public KeyCode Cameraboost = KeyCode.LeftShift;
    public KeyCode Cameraslow = KeyCode.LeftControl;
    public KeyCode Cameratoggle = KeyCode.LeftAlt;
    public KeyCode ZeroCamera = KeyCode.Keypad0;
    [Space(10)]
    public string EmulatorPath = "";
    public string WorkspacePath = "";
    public string LaunchPath = "";

    public void Save(string path)
    {
        Version = TrickyMapInterface.Instance.Version;
        string serializer = JsonUtility.ToJson(this);
        File.WriteAllText(path, serializer);
    }

    public static LevelEditorSettings Load(string Path)
    {
        if (File.Exists(Path))
        {
            string FileData = File.ReadAllText(Path);
            return JsonUtility.FromJson<LevelEditorSettings>(FileData);
        }
        else
        {
            return new LevelEditorSettings();
        }
    }
}
