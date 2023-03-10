using RapidGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPage : MonoBehaviour
{
    private void OnGUI()
    {
        DoGUI();
    }
    public string EmnuPath;
    public string WorkPath;
    public string LaunchPath;
    public float PatchRes;
    Rect rect = new Rect(Vector2.one * 100, new Vector2(700f,175f));

    public void DoGUI()
    {
        rect = RGUI.ResizableWindow(GetHashCode(), rect,
    (id) =>
    {
        TrickyMapInterface.Instance.settings.EmulatorPath = RGUI.Field(TrickyMapInterface.Instance.settings.EmulatorPath, "Emulator Path");
        TrickyMapInterface.Instance.settings.WorkspacePath = RGUI.Field(TrickyMapInterface.Instance.settings.WorkspacePath, "Workspace Path");
        TrickyMapInterface.Instance.settings.LaunchPath = RGUI.Field(TrickyMapInterface.Instance.settings.LaunchPath, "Launch Path");
        TrickyMapInterface.Instance.settings.PatchResolution = RGUI.Slider(TrickyMapInterface.Instance.settings.PatchResolution, 2, 12, "Patch Resolution");

        if (GUILayout.Button("Apply"))
        {
            TrickyMapInterface.Instance.UpdateNURBSRes();
            TrickyMapInterface.Instance.settings.Save(UnityEngine.Application.dataPath + "/Config.json");
        }
        GUI.DragWindow();
    },
    "Settings");
    }
}
