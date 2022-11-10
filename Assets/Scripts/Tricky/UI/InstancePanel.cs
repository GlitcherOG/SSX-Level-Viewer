using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstancePanel : MonoBehaviour
{
    public static InstancePanel instance;
    public bool DisallowUpdate;
    public InstanceObject instanceObject;

    public TMP_InputField InstanceName;

    public RowCollumHandler Location;
    public RowCollumHandler Rotation;
    public RowCollumHandler Scale;

    public RowCollumHandler Unknown1;
    public RowCollumHandler Unknown2;
    public RowCollumHandler Unknown3;
    public RowCollumHandler Unknown4;
    public RowCollumHandler Unknown5;
    public RowCollumHandler Unknown6;
    public RowCollumHandler Unknown7;
    public RowCollumHandler RGBA;

    public TMP_InputField ModelID;
    public TMP_InputField PrevInstance;
    public TMP_InputField NextInstance;
    public TMP_InputField UnknownInt1;
    public TMP_InputField UnknownInt2;
    public TMP_InputField UnknownInt3;
    public TMP_InputField ModelID2;
    public TMP_InputField UnknownInt4;
    public TMP_InputField UnknownInt5;
    public TMP_InputField UnknownInt6;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateAll(InstanceObject inObject)
    {
        if(instanceObject !=null)
        {
            instanceObject.UnSelectedObject();
        }

        DisallowUpdate = true;
        instanceObject = inObject;
        instanceObject.SelectedObject();
        InstanceName.text = instanceObject.InstanceName;

        Location.SetXYZ(instanceObject.InstancePosition);
        Rotation.SetXYZ(instanceObject.rotation);
        Scale.SetXYZ(instanceObject.scale);

        Unknown1.SetXYZW(instanceObject.Unknown5);
        Unknown2.SetXYZW(instanceObject.Unknown6);
        Unknown3.SetXYZW(instanceObject.Unknown7);
        Unknown4.SetXYZW(instanceObject.Unknown8);
        Unknown5.SetXYZW(instanceObject.Unknown9);
        Unknown6.SetXYZW(instanceObject.Unknown10);
        Unknown7.SetXYZW(instanceObject.Unknown11);
        RGBA.SetXYZW(instanceObject.RGBA);

        ModelID.text = instanceObject.ModelID.ToString();
        PrevInstance.text = instanceObject.PrevInstance.ToString();
        NextInstance.text = instanceObject.NextInstance.ToString();
        UnknownInt1.text = instanceObject.UnknownInt26.ToString();
        UnknownInt2.text = instanceObject.UnknownInt27.ToString();
        UnknownInt3.text = instanceObject.UnknownInt28.ToString();
        ModelID2.text = instanceObject.ModelID2.ToString();
        UnknownInt4.text = instanceObject.UnknownInt30.ToString();
        UnknownInt5.text = instanceObject.UnknownInt31.ToString();
        UnknownInt6.text = instanceObject.UnknownInt32.ToString();
        DisallowUpdate = false;
    }

    public void SetInstanceData()
    {
        if(!DisallowUpdate)
        {
            instanceObject.InstanceName = InstanceName.text;
            instanceObject.InstancePosition = Location.GrabXYZ();
            instanceObject.rotation = Rotation.GrabXYZ();
            instanceObject.scale = Scale.GrabXYZ();

            instanceObject.Unknown5 = Unknown1.GrabXYZW();
            instanceObject.Unknown6 = Unknown2.GrabXYZW();
            instanceObject.Unknown7 = Unknown3.GrabXYZW();
            instanceObject.Unknown8 = Unknown4.GrabXYZW();
            instanceObject.Unknown9 = Unknown5.GrabXYZW();
            instanceObject.Unknown10 = Unknown6.GrabXYZW();
            instanceObject.Unknown11 = Unknown7.GrabXYZW();
            instanceObject.RGBA = RGBA.GrabXYZW();
            instanceObject.UpdateTransform();
        }
    }

    public void SetModelID()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.ModelID = Int32.Parse(ModelID.text);
                instanceObject.GenerateMeshes();
                ModelID.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                ModelID.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetPrevInstance()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.PrevInstance = Int32.Parse(PrevInstance.text);
                PrevInstance.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                PrevInstance.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetNextInstance()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.NextInstance = Int32.Parse(NextInstance.text);
                NextInstance.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                NextInstance.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown1()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.UnknownInt26 = Int32.Parse(UnknownInt1.text);
                UnknownInt1.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                UnknownInt1.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown2()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.UnknownInt27 = Int32.Parse(UnknownInt2.text);
                UnknownInt2.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                UnknownInt2.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown3()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.UnknownInt28 = Int32.Parse(UnknownInt3.text);
                UnknownInt3.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                UnknownInt3.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetModelID2()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.ModelID2 = Int32.Parse(ModelID2.text);
                ModelID2.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                ModelID2.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown4()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.UnknownInt30 = Int32.Parse(UnknownInt4.text);
                UnknownInt4.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                UnknownInt4.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown5()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.UnknownInt31 = Int32.Parse(UnknownInt5.text);
                UnknownInt5.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                UnknownInt5.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown6()
    {
        if (!DisallowUpdate)
        {
            try
            {
                instanceObject.UnknownInt31 = Int32.Parse(UnknownInt6.text);
                UnknownInt6.GetComponent<Image>().color = Color.white;
            }
            catch
            {
                UnknownInt6.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void HideSelfAndChild()
    {
        if (instanceObject != null)
        {
            instanceObject.UnSelectedObject();
            gameObject.SetActive(false);
        }
    }
}
