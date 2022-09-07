using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.UI;
public class SettingsPanel : MonoBehaviour
{
    public TMP_InputField LanuchInput;
    public TMP_InputField EmulaterInput;
    public TMP_InputField WorkspaceInput;

    bool DisableUpdate;

    private void Start()
    {
        LoadSaveFile();
    }

    public void LoadSaveFile()
    {
        DisableUpdate = true;
        LanuchInput.text = TrickyMapInterface.Instance.settings.LaunchPath;
        EmulaterInput.text = TrickyMapInterface.Instance.settings.EmulatorPath;
        WorkspaceInput.text = TrickyMapInterface.Instance.settings.WorkspacePath;
        DisableUpdate = false;
    }

    public void UpdateLaunchPath(string Path)
    {
        if (!DisableUpdate)
        {
            if (File.Exists(Path) && (Path.ToLower().Contains(".iso") || Path.ToLower().Contains(".elf")))
            {
                LanuchInput.GetComponent<Image>().color = Color.white;
                TrickyMapInterface.Instance.settings.LaunchPath = Path;
                TrickyMapInterface.Instance.settings.Save(TrickyMapInterface.Instance.ConfigPath);
            }
            else
            {
                LanuchInput.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateEmulatorPath(string Path)
    {
        if (!DisableUpdate)
        {
            if (File.Exists(Path))
            {
                EmulaterInput.GetComponent<Image>().color = Color.white;
                TrickyMapInterface.Instance.settings.EmulatorPath = Path;
                TrickyMapInterface.Instance.settings.Save(TrickyMapInterface.Instance.ConfigPath);
            }
            else
            {
                EmulaterInput.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateWorkspacePath(string Path)
    {
        if (!DisableUpdate)
        {
            if (Directory.Exists(Path))
            {
                WorkspaceInput.GetComponent<Image>().color = Color.white;
                TrickyMapInterface.Instance.settings.WorkspacePath = Path;
                TrickyMapInterface.Instance.settings.Save(TrickyMapInterface.Instance.ConfigPath);
            }
            else
            {
                WorkspaceInput.GetComponent<Image>().color = Color.red;
            }
        }
    }
}
