using RapidGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchPage : MonoBehaviour
{
    public List<PatchObject> patchObjects = new List<PatchObject>();

    private void OnGUI()
    {
        if (patchObjects.Count == 1)
        {
            DoGUI();
        }
    }
    
    Rect rect = new Rect(Vector2.one * 100, new Vector2(400f, 175f));

    public void DoGUI()
    {
        var TempPatch = patchObjects[0];
        rect = RGUI.ResizableWindow(GetHashCode(), rect,
    (id) =>
    {
        TempPatch.PatchName = RGUI.Field(TempPatch.PatchName, "Patch Name");
        TempPatch.LightMapPoint = RGUI.Field(TempPatch.LightMapPoint, "Lightmap Point");

        TempPatch.UVPoint1 = RGUI.Field(TempPatch.UVPoint1, "UV Point 1");

        TempPatch.UVPoint2 = RGUI.Field(TempPatch.UVPoint2, "UV Point 2");

        TempPatch.UVPoint3 = RGUI.Field(TempPatch.UVPoint3, "UV Point 3");

        TempPatch.UVPoint4 = RGUI.Field(TempPatch.UVPoint4, "UV Point 4");

        GUILayout.BeginHorizontal();
        TempPatch.RawControlPoint.x = RGUI.Field(TempPatch.RawControlPoint.x, "R1C1");
        TempPatch.RawControlPoint.y = RGUI.Field(TempPatch.RawControlPoint.y);
        TempPatch.RawControlPoint.z = RGUI.Field(TempPatch.RawControlPoint.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR1C2.x = RGUI.Field(TempPatch.RawR1C2.x, "R1C2");
        TempPatch.RawR1C2.y = RGUI.Field(TempPatch.RawR1C2.y);
        TempPatch.RawR1C2.z = RGUI.Field(TempPatch.RawR1C2.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR1C3.x = RGUI.Field(TempPatch.RawR1C3.x, "R1C3");
        TempPatch.RawR1C3.y = RGUI.Field(TempPatch.RawR1C3.y);
        TempPatch.RawR1C3.z = RGUI.Field(TempPatch.RawR1C3.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR1C4.x = RGUI.Field(TempPatch.RawR1C4.x, "R1C4");
        TempPatch.RawR1C4.y = RGUI.Field(TempPatch.RawR1C4.y);
        TempPatch.RawR1C4.z = RGUI.Field(TempPatch.RawR1C4.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR2C1.x = RGUI.Field(TempPatch.RawR2C1.x, "R2C1");
        TempPatch.RawR2C1.y = RGUI.Field(TempPatch.RawR2C1.y);
        TempPatch.RawR2C1.z = RGUI.Field(TempPatch.RawR2C1.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR2C2.x = RGUI.Field(TempPatch.RawR2C2.x, "R2C2");
        TempPatch.RawR2C2.y = RGUI.Field(TempPatch.RawR2C2.y);
        TempPatch.RawR2C2.z = RGUI.Field(TempPatch.RawR2C2.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR2C3.x = RGUI.Field(TempPatch.RawR2C3.x, "R2C3");
        TempPatch.RawR2C3.y = RGUI.Field(TempPatch.RawR2C3.y);
        TempPatch.RawR2C3.z = RGUI.Field(TempPatch.RawR2C3.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR2C4.x = RGUI.Field(TempPatch.RawR2C4.x, "R2C4");
        TempPatch.RawR2C4.y = RGUI.Field(TempPatch.RawR2C4.y);
        TempPatch.RawR2C4.z = RGUI.Field(TempPatch.RawR2C4.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR3C1.x = RGUI.Field(TempPatch.RawR3C1.x, "R3C1");
        TempPatch.RawR3C1.y = RGUI.Field(TempPatch.RawR3C1.y);
        TempPatch.RawR3C1.z = RGUI.Field(TempPatch.RawR3C1.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR3C2.x = RGUI.Field(TempPatch.RawR3C2.x, "R3C2");
        TempPatch.RawR3C2.y = RGUI.Field(TempPatch.RawR3C2.y);
        TempPatch.RawR3C2.z = RGUI.Field(TempPatch.RawR3C2.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR3C3.x = RGUI.Field(TempPatch.RawR3C3.x, "R3C3");
        TempPatch.RawR3C3.y = RGUI.Field(TempPatch.RawR3C3.y);
        TempPatch.RawR3C3.z = RGUI.Field(TempPatch.RawR3C3.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR3C4.x = RGUI.Field(TempPatch.RawR3C4.x, "R3C4");
        TempPatch.RawR3C4.y = RGUI.Field(TempPatch.RawR3C4.y);
        TempPatch.RawR3C4.z = RGUI.Field(TempPatch.RawR3C4.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR4C1.x = RGUI.Field(TempPatch.RawR4C1.z, "R4C1");
        TempPatch.RawR4C1.y = RGUI.Field(TempPatch.RawR4C1.y);
        TempPatch.RawR4C1.z = RGUI.Field(TempPatch.RawR4C1.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR4C2.x = RGUI.Field(TempPatch.RawR4C2.x, "R4C2");
        TempPatch.RawR4C2.y = RGUI.Field(TempPatch.RawR4C2.y);
        TempPatch.RawR4C2.z = RGUI.Field(TempPatch.RawR4C2.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR4C3.x = RGUI.Field(TempPatch.RawR4C3.x, "R4C3");
        TempPatch.RawR4C3.y = RGUI.Field(TempPatch.RawR4C3.y);
        TempPatch.RawR4C3.z = RGUI.Field(TempPatch.RawR4C3.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.RawR4C4.x = RGUI.Field(TempPatch.RawR4C4.x, "R4C4");
        TempPatch.RawR4C4.y = RGUI.Field(TempPatch.RawR4C4.y);
        TempPatch.RawR4C4.z = RGUI.Field(TempPatch.RawR4C4.z);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.PatchStyle = RGUI.Field(TempPatch.PatchStyle, "Patch Style");
        TempPatch.TrickOnlyPatch = RGUI.Field(TempPatch.TrickOnlyPatch, "Tricky Only Patch");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        TempPatch.TextureAssigment = RGUI.Field(TempPatch.TextureAssigment, "Texture ID");
        TempPatch.LightmapID = RGUI.Field(TempPatch.LightmapID, "Lightmap ID");
        GUILayout.EndHorizontal();
       

        GUI.DragWindow();
    },
    "Patch Page");
    }
}
