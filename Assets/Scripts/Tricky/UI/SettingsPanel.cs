using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.UI;
public class SettingsPanel : MonoBehaviour
{
    LevelEditorSettings settings;
    public TMP_InputField LanuchInput;
    public TMP_InputField EmulaterInput;
    public TMP_InputField WorkspaceInput;

    public TMP_Text PatchResText;
    public Slider PatchSlider;

    bool DisableUpdate;

    private void Start()
    {

    }

    public void LoadSaveFile()
    {
        DisableUpdate = true;
        settings = TrickyMapInterface.Instance.settings;
        LanuchInput.text = settings.LaunchPath;
        EmulaterInput.text = settings.EmulatorPath;
        //WorkspaceInput.text = TrickyMapInterface.Instance.settings.WorkspacePath;

        PatchResText.text = "Patch Resolution: " + settings.PatchResolution.ToString();
        PatchSlider.value = settings.PatchResolution;
        DisableUpdate = false;
    }

    public void UpdateResolution(float Value)
    {
        settings.PatchResolution = (int)Value;
        PatchResText.text = "Patch Resolution: " + settings.PatchResolution.ToString();
    }

    public void ApplySettings()
    {
        TrickyMapInterface.Instance.UpdateNURBSRes();
        TrickyMapInterface.Instance.settings.Save(TrickyMapInterface.Instance.ConfigPath);
    }

    public void UpdateLaunchPath(string Path)
    {
        if (!DisableUpdate)
        {
            if (File.Exists(Path) && (Path.ToLower().Contains(".iso") || Path.ToLower().Contains(".elf")))
            {
                LanuchInput.GetComponent<Image>().color = Color.white;
                settings.LaunchPath = Path;
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
                settings.EmulatorPath = Path;
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
                settings.WorkspacePath = Path;
            }
            else
            {
                WorkspaceInput.GetComponent<Image>().color = Color.red;
            }
        }
    }
}
