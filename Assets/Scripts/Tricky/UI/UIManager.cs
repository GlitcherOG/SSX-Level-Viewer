using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        #region TopBar Menu
        GUI.Box(new Rect(0, 0, Screen.width, 21), "");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Level"))
        {
            TrickyMapInterface.Instance.OpenFileMap();
        }
        if (GUILayout.Button("Save Level"))
        {

        }
        if (GUILayout.Button("Material Libary"))
        {

        }
        if (GUILayout.Button("Texture Libary"))
        {

        }
        if (GUILayout.Button("Prefab Libary"))
        {

        }
        if (GUILayout.Button("Settings"))
        {

        }
        GUILayout.EndHorizontal();
        #endregion



    }
}
