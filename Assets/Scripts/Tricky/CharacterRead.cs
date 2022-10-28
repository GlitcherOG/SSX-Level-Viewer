using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRead : MonoBehaviour
{
    public bool active;
    public string StringAddress;
    public string StringCameraAddress;
    public ProcessMemory processMemory = new ProcessMemory();
    public long CharacterAddress;
    public long CameraAddress;

    public GameObject cameraGameObject;

    // Start is called before the first frame update
    void Start()
    {
        CharacterAddress = Convert.ToInt32(StringAddress, 16);
        CameraAddress = Convert.ToInt32(StringCameraAddress, 16);

        if (processMemory.InitaliseProcess("pcsx2"))
        {
            active = true;
            Debug.Log("Pcsx2 Detected");
        }
        else
        {
            Debug.Log("Pcsx2 Not Detected");
        }
    }

    void Update()
    {
        if(active)
        {
            float[] Location = processMemory.ReadFloats(CameraAddress, 6);
            cameraGameObject.transform.localPosition = new Vector3(Location[0]*TrickyMapInterface.Scale, Location[1] * TrickyMapInterface.Scale, Location[2] * TrickyMapInterface.Scale);
            cameraGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            //cameraGameObject.transform.localEulerAngles = new Vector3(Location[5], -Location[4], Location[3]) * Mathf.Rad2Deg;
            cameraGameObject.transform.Rotate(Location[3] * Mathf.Rad2Deg * -1, 0.0f, 0.0f);
            cameraGameObject.transform.Rotate(0.0f, Location[5] * Mathf.Rad2Deg * -1, 0.0f);
            cameraGameObject.transform.Rotate(0.0f, 0.0f, -Location[4] * Mathf.Rad2Deg * -1);
        }
    }

}
