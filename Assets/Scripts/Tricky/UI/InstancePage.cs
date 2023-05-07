using RapidGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancePage : MonoBehaviour
{
    public List<InstanceObject> instanceObjects = new List<InstanceObject>();
    bool HideLighting;
    private void OnGUI()
    {
        if (instanceObjects.Count == 1)
        {
            DoGUI();
        }
    }

    Rect rect = new Rect(Vector2.one * 100, new Vector2(400f, 175f));

    public void DoGUI()
    {
        var TempPatch = instanceObjects[0];
        using (new RGUI.BackgroundColorScope(Color.black))
        {
            rect = RGUI.ResizableWindow(GetHashCode(), rect,
        (id) =>
        {
            RGUI.BeginBackgroundColor(new Color(1, 1, 1));

            TempPatch.InstanceName = RGUI.Field(TempPatch.InstanceName, "Instance Name");

            GUILayout.BeginHorizontal();
            TempPatch.InstancePosition.x = RGUI.Field(TempPatch.InstancePosition.x, "Position");
            TempPatch.InstancePosition.y = RGUI.Field(TempPatch.InstancePosition.y);
            TempPatch.InstancePosition.z = RGUI.Field(TempPatch.InstancePosition.z);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            TempPatch.rotation.x = RGUI.Field(TempPatch.rotation.x, "Rotation");
            TempPatch.rotation.y = RGUI.Field(TempPatch.rotation.y);
            TempPatch.rotation.z = RGUI.Field(TempPatch.rotation.z);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            TempPatch.scale.x = RGUI.Field(TempPatch.scale.x, "Scale");
            TempPatch.scale.y = RGUI.Field(TempPatch.scale.y);
            TempPatch.scale.z = RGUI.Field(TempPatch.scale.z);
            GUILayout.EndHorizontal();

            HideLighting = GUILayout.Toggle(HideLighting, "Hide Lighting Data");
            if (HideLighting)
            {
                GUILayout.BeginHorizontal();
                TempPatch.Unknown5.x = RGUI.Field(TempPatch.Unknown5.x, "Unknown 5");
                TempPatch.Unknown5.y = RGUI.Field(TempPatch.Unknown5.y);
                TempPatch.Unknown5.z = RGUI.Field(TempPatch.Unknown5.z);
                TempPatch.Unknown5.w = RGUI.Field(TempPatch.Unknown5.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.Unknown6.x = RGUI.Field(TempPatch.Unknown6.x, "Unknown 6");
                TempPatch.Unknown6.y = RGUI.Field(TempPatch.Unknown6.y);
                TempPatch.Unknown6.z = RGUI.Field(TempPatch.Unknown6.z);
                TempPatch.Unknown6.w = RGUI.Field(TempPatch.Unknown6.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.Unknown7.x = RGUI.Field(TempPatch.Unknown7.x, "Unknown 7");
                TempPatch.Unknown7.y = RGUI.Field(TempPatch.Unknown7.y);
                TempPatch.Unknown7.z = RGUI.Field(TempPatch.Unknown7.z);
                TempPatch.Unknown7.w = RGUI.Field(TempPatch.Unknown7.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.Unknown8.x = RGUI.Field(TempPatch.Unknown8.x, "Unknown 8");
                TempPatch.Unknown8.y = RGUI.Field(TempPatch.Unknown8.y);
                TempPatch.Unknown8.z = RGUI.Field(TempPatch.Unknown8.z);
                TempPatch.Unknown8.w = RGUI.Field(TempPatch.Unknown8.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.Unknown9.x = RGUI.Field(TempPatch.Unknown9.x, "Unknown 9");
                TempPatch.Unknown9.y = RGUI.Field(TempPatch.Unknown9.y);
                TempPatch.Unknown9.z = RGUI.Field(TempPatch.Unknown9.z);
                TempPatch.Unknown9.w = RGUI.Field(TempPatch.Unknown9.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.Unknown10.x = RGUI.Field(TempPatch.Unknown10.x, "Unknown 10");
                TempPatch.Unknown10.y = RGUI.Field(TempPatch.Unknown10.y);
                TempPatch.Unknown10.z = RGUI.Field(TempPatch.Unknown10.z);
                TempPatch.Unknown10.w = RGUI.Field(TempPatch.Unknown10.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.Unknown11.x = RGUI.Field(TempPatch.Unknown11.x, "Unknown 11");
                TempPatch.Unknown11.y = RGUI.Field(TempPatch.Unknown11.y);
                TempPatch.Unknown11.z = RGUI.Field(TempPatch.Unknown11.z);
                TempPatch.Unknown11.w = RGUI.Field(TempPatch.Unknown11.w);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                TempPatch.RGBA.x = RGUI.Field(TempPatch.RGBA.x, "RGBA");
                TempPatch.RGBA.y = RGUI.Field(TempPatch.RGBA.y);
                TempPatch.RGBA.z = RGUI.Field(TempPatch.RGBA.z);
                TempPatch.RGBA.w = RGUI.Field(TempPatch.RGBA.w);
                GUILayout.EndHorizontal();
            }

            TempPatch.ModelID = RGUI.Field(TempPatch.ModelID, "ModelID");
            TempPatch.PrevInstance = RGUI.Field(TempPatch.PrevInstance, "PrevInstance");
            TempPatch.NextInstance = RGUI.Field(TempPatch.NextInstance, "NextInstance");
            TempPatch.UnknownInt26 = RGUI.Field(TempPatch.UnknownInt26, "UnknownInt26");
            TempPatch.UnknownInt27 = RGUI.Field(TempPatch.UnknownInt27, "UnknownInt27");
            TempPatch.UnknownInt28 = RGUI.Field(TempPatch.UnknownInt28, "UnknownInt28");
            TempPatch.ModelID2 = RGUI.Field(TempPatch.ModelID2, "ModelID2");
            TempPatch.UnknownInt30 = RGUI.Field(TempPatch.UnknownInt30, "UnknownInt30");
            TempPatch.UnknownInt31 = RGUI.Field(TempPatch.UnknownInt31, "UnknownInt31");
            TempPatch.UnknownInt32 = RGUI.Field(TempPatch.UnknownInt32, "UnknownInt32");
            TempPatch.LTGState = RGUI.Field(TempPatch.LTGState, "LTGState");

            GUI.DragWindow();
        },
        "Instance Page");
        }
    }
}
