using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void HideSelfAndChild()
    {
        if (instanceObject != null)
        {
            instanceObject.UnSelectedObject();
            gameObject.SetActive(false);
        }
    }
}
