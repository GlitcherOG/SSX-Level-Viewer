using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PatchPanel : MonoBehaviour
{
    public static PatchPanel instance;
    public bool DisallowUpdate;
    public PatchObject patchObject;
    public TMP_InputField InputName;
    public TMP_InputField TextureNumber;
    public TMP_Dropdown PatchStyle;
    public TMP_InputField Unknown1;
    public TMP_InputField Unknown2;
    public TMP_InputField Unknown3;
    public TMP_InputField Unknown4;
    public TMP_InputField Unknown5;

    public GameObject ScrollView;
    public GameObject RCPrefab;
    public GameObject cube;
    public GameObject CubeParent;
    public List<RowCollumHandler> RCPreferences = new List<RowCollumHandler>();
    public List<GameObject> cubePoints = new List<GameObject>();

    public bool Local = false;

    private void Awake()
    {
        instance = this;
        RCPreferences.Add(RCPrefab.GetComponent<RowCollumHandler>());
        for (int i = 0; i < 15; i++)
        {
            GameObject temp = Instantiate(RCPrefab, ScrollView.transform);
            RCPreferences.Add(temp.GetComponent<RowCollumHandler>());
        }

        RCPreferences[0].SetName("R1C1 (Object Position)");
        RCPreferences[1].SetName("R1C2");
        RCPreferences[2].SetName("R1C3");
        RCPreferences[3].SetName("R1C4");
        RCPreferences[4].SetName("R2C1");
        RCPreferences[5].SetName("R2C2");
        RCPreferences[6].SetName("R2C3");
        RCPreferences[7].SetName("R2C4");
        RCPreferences[8].SetName("R3C1");
        RCPreferences[9].SetName("R3C2");
        RCPreferences[10].SetName("Processed R3C3");
        RCPreferences[11].SetName("R3C4");
        RCPreferences[12].SetName("R4C1");
        RCPreferences[13].SetName("R4C2");
        RCPreferences[14].SetName("R4C3");
        RCPreferences[15].SetName("R4C4");

        RCPreferences[0].SetColour(Color.white);
        RCPreferences[1].SetColour(Color.red);
        RCPreferences[2].SetColour(Color.green);
        RCPreferences[3].SetColour(Color.blue);
        RCPreferences[4].SetColour(Color.yellow);
        RCPreferences[5].SetColour(Color.grey);
        RCPreferences[6].SetColour(Color.cyan);
        RCPreferences[7].SetColour(Color.magenta);
        RCPreferences[8].SetColour(new Color32(0,255,85,255));
        RCPreferences[9].SetColour(new Color32(216, 0, 255, 255));
        RCPreferences[10].SetColour(new Color32(185, 0, 255, 255));
        RCPreferences[11].SetColour(new Color32(0, 99, 119, 255));
        RCPreferences[12].SetColour(new Color32(0, 119, 49, 255));
        RCPreferences[13].SetColour(new Color32(119, 16, 0, 255));
        RCPreferences[14].SetColour(new Color32(211, 216, 45, 255));
        RCPreferences[15].SetColour(Color.black);

        for (int i = 0; i < RCPreferences.Count; i++)
        {
            RCPreferences[i].SendUpdatePing.AddListener(UpdatePatchPoint);
        }
    }

    public void UpdateAll(PatchObject patch)
    {
        if(patchObject!=null)
        {
            patchObject.UnSelectedObject();
        }


        DisallowUpdate = true;
        patchObject = patch;
        patchObject.SelectedObject();
        InputName.text = patch.PatchName;
        TextureNumber.text = patch.TextureAssigment.ToString();
        PatchStyle.value = patch.PatchStyle;
        Unknown1.text = patch.Unknown2.ToString();
        Unknown2.text = patch.Unknown3.ToString();
        Unknown3.text = patch.Unknown4.ToString();
        Unknown4.text = patch.Unknown5.ToString();
        Unknown5.text = patch.Unknown6.ToString();

        UpdatePoint(true);
        DisallowUpdate = false;
    }

    public void UpdatePoint(bool DisallowUpdateCheck)
    {
        if (!DisallowUpdateCheck)
        {
            DisallowUpdate = true;
        }
        if (!Local)
        {
            RCPreferences[0].SetXYZ(patchObject.RawControlPoint);
            RCPreferences[1].SetXYZ(patchObject.RawR1C2);
            RCPreferences[2].SetXYZ(patchObject.RawR1C3);
            RCPreferences[3].SetXYZ(patchObject.RawR1C4);
            RCPreferences[4].SetXYZ(patchObject.RawR2C1);
            RCPreferences[5].SetXYZ(patchObject.RawR2C2);
            RCPreferences[6].SetXYZ(patchObject.RawR2C3);
            RCPreferences[7].SetXYZ(patchObject.RawR2C4);
            RCPreferences[8].SetXYZ(patchObject.RawR3C1);
            RCPreferences[9].SetXYZ(patchObject.RawR3C2);
            RCPreferences[10].SetXYZ(patchObject.R3C3);
            RCPreferences[11].SetXYZ(patchObject.RawR3C4);
            RCPreferences[12].SetXYZ(patchObject.RawR4C1);
            RCPreferences[13].SetXYZ(patchObject.RawR4C2);
            RCPreferences[14].SetXYZ(patchObject.RawR4C3);
            RCPreferences[15].SetXYZ(patchObject.RawR4C4);
        }
        else
        {
            RCPreferences[0].SetXYZ(patchObject.RawControlPoint);
            RCPreferences[1].SetXYZ(patchObject.RawR1C2 - patchObject.RawControlPoint);
            RCPreferences[2].SetXYZ(patchObject.RawR1C3 - patchObject.RawControlPoint);
            RCPreferences[3].SetXYZ(patchObject.RawR1C4 - patchObject.RawControlPoint);
            RCPreferences[4].SetXYZ(patchObject.RawR2C1 - patchObject.RawControlPoint);
            RCPreferences[5].SetXYZ(patchObject.RawR2C2 - patchObject.RawControlPoint);
            RCPreferences[6].SetXYZ(patchObject.RawR2C3 - patchObject.RawControlPoint);
            RCPreferences[7].SetXYZ(patchObject.RawR2C4 - patchObject.RawControlPoint);
            RCPreferences[8].SetXYZ(patchObject.RawR3C1 - patchObject.RawControlPoint);
            RCPreferences[9].SetXYZ(patchObject.RawR3C2 - patchObject.RawControlPoint);
            RCPreferences[10].SetXYZ(patchObject.R3C3);
            RCPreferences[11].SetXYZ(patchObject.RawR3C4 - patchObject.RawControlPoint);
            RCPreferences[12].SetXYZ(patchObject.RawR4C1 - patchObject.RawControlPoint);
            RCPreferences[13].SetXYZ(patchObject.RawR4C2 - patchObject.RawControlPoint);
            RCPreferences[14].SetXYZ(patchObject.RawR4C3 - patchObject.RawControlPoint);
            RCPreferences[15].SetXYZ(patchObject.RawR4C4 - patchObject.RawControlPoint);
        }

        if (DisallowUpdateCheck)
        {
            for (int i = 0; i < cubePoints.Count; i++)
            {
                Destroy(cubePoints[i]);
            }
            cubePoints = new List<GameObject>();
            SpawnCube(patchObject.RawControlPoint, Color.white);
            SpawnCube(patchObject.RawR1C2, Color.red);
            SpawnCube(patchObject.RawR1C3, Color.green);
            SpawnCube(patchObject.RawR1C4, Color.blue);
            SpawnCube(patchObject.RawR2C1, Color.yellow);
            SpawnCube(patchObject.RawR2C2, Color.grey);
            SpawnCube(patchObject.RawR2C3, Color.cyan);
            SpawnCube(patchObject.RawR2C4, Color.magenta);
            SpawnCube(patchObject.RawR3C1, new Color32(0, 255, 85, 255));
            SpawnCube(patchObject.RawR3C2, new Color32(216, 0, 255, 255));
            SpawnCube(patchObject.RawR3C3, new Color32(185, 0, 255, 255));
            SpawnCube(patchObject.RawR3C4, new Color32(0, 99, 119, 255));
            SpawnCube(patchObject.RawR4C1, new Color32(0, 119, 49, 255));
            SpawnCube(patchObject.RawR4C2, new Color32(119, 16, 0, 255));
            SpawnCube(patchObject.RawR4C3, new Color32(211, 216, 45, 255));
            SpawnCube(patchObject.RawR4C4, Color.black);
        }
        else
        {
            for (int i = 0; i < cubePoints.Count; i++)
            {
                cubePoints[i].GetComponent<PatchPoint>().DisableUpdate = true;
            }

            cubePoints[0].transform.position = patchObject.RawControlPoint * TrickyMapInterface.Scale;
            cubePoints[1].transform.position = patchObject.RawR1C2 * TrickyMapInterface.Scale;
            cubePoints[2].transform.position = patchObject.RawR1C3 * TrickyMapInterface.Scale;
            cubePoints[3].transform.position = patchObject.RawR1C4 * TrickyMapInterface.Scale;
            cubePoints[4].transform.position = patchObject.RawR2C1 * TrickyMapInterface.Scale;
            cubePoints[5].transform.position = patchObject.RawR2C2 * TrickyMapInterface.Scale;
            cubePoints[6].transform.position = patchObject.RawR2C3 * TrickyMapInterface.Scale;
            cubePoints[7].transform.position = patchObject.RawR2C4 * TrickyMapInterface.Scale;
            cubePoints[8].transform.position = patchObject.RawR3C1 * TrickyMapInterface.Scale;
            cubePoints[9].transform.position = patchObject.RawR3C2 * TrickyMapInterface.Scale;
            cubePoints[10].transform.position = patchObject.RawR3C3 * TrickyMapInterface.Scale;
            cubePoints[11].transform.position = patchObject.RawR3C4 * TrickyMapInterface.Scale;
            cubePoints[12].transform.position = patchObject.RawR4C1 * TrickyMapInterface.Scale;
            cubePoints[13].transform.position = patchObject.RawR4C2 * TrickyMapInterface.Scale;
            cubePoints[14].transform.position = patchObject.RawR4C3 * TrickyMapInterface.Scale;
            cubePoints[15].transform.position = patchObject.RawR4C4 * TrickyMapInterface.Scale;

            for (int i = 0; i < cubePoints.Count; i++)
            {
                cubePoints[i].GetComponent<PatchPoint>().DisableUpdate = false;
            }
        }

        if (!DisallowUpdateCheck)
        {
            DisallowUpdate = false;
        }
    }

    public void DestoyCubes()
    {
        for (int i = 0; i < cubePoints.Count; i++)
        {
            Destroy(cubePoints[i]);
        }
    }

    void SpawnCube(Vector3 Point, Color color)
    {
        GameObject gameObject = Instantiate(cube, Point*TrickyMapInterface.Scale, new Quaternion(0, 0, 0, 0));
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        gameObject.transform.parent = CubeParent.transform;
        gameObject.GetComponent<PatchPoint>().ID = cubePoints.Count;
        gameObject.GetComponent<PatchPoint>().PatchObject = patchObject;
        gameObject.GetComponent<PatchPoint>().unityEvent = UpdatePointUsingCube;
        cubePoints.Add(gameObject);
    }

    public void UpdatePatchPoint()
    {
        if (!DisallowUpdate)
        {
            DisallowUpdate = true;
            if (!Local)
            {
                patchObject.RawControlPoint = RCPreferences[0].GrabXYZ();
                patchObject.transform.position = RCPreferences[0].GrabXYZ()*TrickyMapInterface.Scale;
                patchObject.RawR1C2 = RCPreferences[1].GrabXYZ();
                patchObject.RawR1C3 = RCPreferences[2].GrabXYZ();
                patchObject.RawR1C4 = RCPreferences[3].GrabXYZ();
                patchObject.RawR2C1 = RCPreferences[4].GrabXYZ();
                patchObject.RawR2C2 = RCPreferences[5].GrabXYZ();
                patchObject.RawR2C3 = RCPreferences[6].GrabXYZ();
                patchObject.RawR2C4 = RCPreferences[7].GrabXYZ();
                patchObject.RawR3C1 = RCPreferences[8].GrabXYZ();
                patchObject.RawR3C2 = RCPreferences[9].GrabXYZ();
                patchObject.R3C3 = RCPreferences[10].GrabXYZ();
                patchObject.RawR3C4 = RCPreferences[11].GrabXYZ();
                patchObject.RawR4C1 = RCPreferences[12].GrabXYZ();
                patchObject.RawR4C2 = RCPreferences[13].GrabXYZ();
                patchObject.RawR4C3 = RCPreferences[14].GrabXYZ();
                patchObject.RawR4C4 = RCPreferences[15].GrabXYZ();
            }
            else
            {
                patchObject.RawControlPoint = RCPreferences[0].GrabXYZ();
                patchObject.transform.position = RCPreferences[0].GrabXYZ() * TrickyMapInterface.Scale;
                patchObject.RawR1C2 = RCPreferences[1].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR1C3 = RCPreferences[2].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR1C4 = RCPreferences[3].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR2C1 = RCPreferences[4].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR2C2 = RCPreferences[5].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR2C3 = RCPreferences[6].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR2C4 = RCPreferences[7].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR3C1 = RCPreferences[8].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR3C2 = RCPreferences[9].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.R3C3 = RCPreferences[10].GrabXYZ();
                patchObject.RawR3C4 = RCPreferences[11].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR4C1 = RCPreferences[12].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR4C2 = RCPreferences[13].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR4C3 = RCPreferences[14].GrabXYZ()+ patchObject.RawControlPoint;
                patchObject.RawR4C4 = RCPreferences[15].GrabXYZ()+ patchObject.RawControlPoint;
            }

            for (int i = 0; i < cubePoints.Count; i++)
            {
                cubePoints[i].GetComponent<PatchPoint>().DisableUpdate = true;
            }

            cubePoints[0].transform.position = patchObject.RawControlPoint * TrickyMapInterface.Scale;
            cubePoints[1].transform.position = patchObject.RawR1C2 * TrickyMapInterface.Scale;
            cubePoints[2].transform.position = patchObject.RawR1C3 * TrickyMapInterface.Scale;
            cubePoints[3].transform.position = patchObject.RawR1C4 * TrickyMapInterface.Scale;
            cubePoints[4].transform.position = patchObject.RawR2C1 * TrickyMapInterface.Scale;
            cubePoints[5].transform.position = patchObject.RawR2C2 * TrickyMapInterface.Scale;
            cubePoints[6].transform.position = patchObject.RawR2C3 * TrickyMapInterface.Scale;
            cubePoints[7].transform.position = patchObject.RawR2C4 * TrickyMapInterface.Scale;
            cubePoints[8].transform.position = patchObject.RawR3C1 * TrickyMapInterface.Scale;
            cubePoints[9].transform.position = patchObject.RawR3C2 * TrickyMapInterface.Scale;
            cubePoints[10].transform.position = patchObject.RawR3C3 * TrickyMapInterface.Scale;
            cubePoints[11].transform.position = patchObject.RawR3C4 * TrickyMapInterface.Scale;
            cubePoints[12].transform.position = patchObject.RawR4C1 * TrickyMapInterface.Scale;
            cubePoints[13].transform.position = patchObject.RawR4C2 * TrickyMapInterface.Scale;
            cubePoints[14].transform.position = patchObject.RawR4C3 * TrickyMapInterface.Scale;
            cubePoints[15].transform.position = patchObject.RawR4C4 * TrickyMapInterface.Scale;

            for (int i = 0; i < cubePoints.Count; i++)
            {
                cubePoints[i].GetComponent<PatchPoint>().OldPosition = cubePoints[i].transform.position;
            }

            for (int i = 0; i < cubePoints.Count; i++)
            {
                cubePoints[i].GetComponent<PatchPoint>().DisableUpdate = false;
            }

            patchObject.UpdateMeshPoints();
            patchObject.ProccessPoints();
            patchObject.SelectedObject();
            DisallowUpdate = false;
        }
    }

    public void UpdateTextureAssigment(string NewTexture)
    {
        if (!DisallowUpdate)
        {
            try
            {
                TextureNumber.GetComponent<Image>().color = Color.white;
                patchObject.UpdateTexture(Int32.Parse(NewTexture));
            }
            catch
            {
                TextureNumber.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateUnkown1(string Unknowm)
    {
        if (!DisallowUpdate)
        {
            try
            {
                Unknown1.GetComponent<Image>().color = Color.white;
                patchObject.Unknown2 = Int32.Parse(Unknowm);
            }
            catch
            {
                Unknown1.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateUnkown2(string Unknowm)
    {
        if (!DisallowUpdate)
        {
            try
            {
                Unknown2.GetComponent<Image>().color = Color.white;
                patchObject.Unknown3 = Int32.Parse(Unknowm);
            }
            catch
            {
                Unknown2.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateUnkown3(string Unknowm)
    {
        if (!DisallowUpdate)
        {
            try
            {
                Unknown3.GetComponent<Image>().color = Color.white;
                patchObject.Unknown4 = Int32.Parse(Unknowm);
            }
            catch
            {
                Unknown3.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateUnkown4(string Unknowm)
    {
        if (!DisallowUpdate)
        {
            try
            {
                Unknown4.GetComponent<Image>().color = Color.white;
                patchObject.Unknown5 = Int32.Parse(Unknowm);
            }
            catch
            {
                Unknown4.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdateUnkown5(string Unknowm)
    {
        if (!DisallowUpdate)
        {
            try
            {
                Unknown5.GetComponent<Image>().color = Color.white;
                patchObject.Unknown6 = Int32.Parse(Unknowm);
            }
            catch
            {
                Unknown5.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdatePatchSytle(int NewStyle)
    {
        if (!DisallowUpdate)
        {
            patchObject.UpdatePatchStyle(NewStyle);
        }
    }

    public void UpdatePointUsingCube(int a)
    {
        if (!DisallowUpdate)
        {
            if (!Local)
            {
                RCPreferences[a].SetXYZ(cubePoints[a].transform.position / TrickyMapInterface.Scale);
            }
            else
            {
                if (a != 0)
                {
                    RCPreferences[a].SetXYZ((cubePoints[a].transform.position - cubePoints[0].transform.position) / TrickyMapInterface.Scale);
                }
                else
                {
                    RCPreferences[a].SetXYZ(cubePoints[a].transform.position / TrickyMapInterface.Scale);
                }
            }
        }
    }

    public void ToggleLocalWorld(int Pos)
    {
        if (Pos == 0)
        {
            Local = false;
        }
        else
        {
            Local = true;
        }
        UpdatePoint(false);
    }

    public void SetName(string Name)
    {
        if(!DisallowUpdate)
        {
            patchObject.PatchName = Name;
        }
    }

    public void Update()
    {
        if(patchObject!=null)
        {
            Debug.DrawLine(cubePoints[0].transform.position, cubePoints[1].transform.position, Color.black);
        }
    }
}
