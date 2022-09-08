using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class SplinePanel : MonoBehaviour
{
    public static SplinePanel instance;

    public SplineSegmentObject splineSegment;
    public SplineObject splineObject;
    bool DisableUpdate;


    public TMP_InputField NameInput;
    public TMP_InputField Unknown1Input;
    public TMP_InputField Unknown2Input;

    public GameObject SegmentSelectorBox;
    public GameObject SegmentSelectorPrefab;
    public List<GameObject> SegmentSelectorList = new List<GameObject>();

    public TMP_Text SegmentName;
    public RowCollumHandler Point1;
    public RowCollumHandler Point2;
    public RowCollumHandler Point3;
    public RowCollumHandler Point4;

    public TMP_InputField Unknown1X;
    public TMP_InputField Unknown1Y;
    public TMP_InputField Unknown1Z;
    public TMP_InputField Unknown1W;

    public TMP_InputField Unknown32;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadSplineAndSegment(SplineSegmentObject gameObject)
    {
        DisableUpdate = true;
        splineSegment = gameObject.GetComponent<SplineSegmentObject>();
        splineObject = gameObject.GetComponentInParent<SplineObject>();

        NameInput.text = splineObject.SplineName;
        Unknown1Input.text = splineObject.Unknown1.ToString();
        Unknown2Input.text = splineObject .Unknown2.ToString();

        DestroySplineSelectors();
        GenerateSplineSelector();
        LoadSegment();
        DisableUpdate = false;
    }

    public void HideSelfAndChild()
    {
        if (splineSegment != null)
        {
            splineSegment.UnSelectedObject();
        }
        gameObject.SetActive(false);
    }

    public void LoadSegment(bool UpdateData = true)
    {
        if (UpdateData)
        {
            splineSegment.SelectedObject();
        }
        DisableUpdate = true;
        SegmentName.text = "Segment " + splineObject.splineSegmentObjects.IndexOf(splineSegment).ToString();

        Point1.SetXYZ(splineSegment.Point1);
        Point2.SetXYZ(splineSegment.Point2);
        Point3.SetXYZ(splineSegment.Point3);
        Point4.SetXYZ(splineSegment.Point4);

        Unknown1X.text = splineSegment.ScalingPoint.x.ToString();
        Unknown1Y.text = splineSegment.ScalingPoint.x.ToString();
        Unknown1Z.text = splineSegment.ScalingPoint.x.ToString();
        Unknown1W.text = splineSegment.ScalingPoint.x.ToString();

        Unknown32.text = splineSegment.Unknown32.ToString();

        DisableUpdate = false;
    }

    void GenerateSplineSelector()
    {
        for (int i = 0; i < splineObject.splineSegmentObjects.Count; i++)
        {
           var TempGameobject= Instantiate(SegmentSelectorPrefab, SegmentSelectorBox.transform);
            TempGameobject.GetComponent<ListButton>().SetData("Segment " + i.ToString(), i);
            TempGameobject.GetComponent<ListButton>().SendUpdatePing = ChangeSegmentSelection;
            SegmentSelectorList.Add(TempGameobject);
        }
    }

    void DestroySplineSelectors()
    {
        for (int i = 0; i < SegmentSelectorList.Count; i++)
        {
            Destroy(SegmentSelectorList[i].gameObject);
        }
        SegmentSelectorList.Clear();
    }

    void ChangeSegmentSelection(int a)
    {
        SelectorScript.instance.ManualSelection(splineObject.splineSegmentObjects[a].gameObject);
    }

    public void SetSplineName(string NewName)
    {
        if (!DisableUpdate)
        {
            splineObject.SplineName = NewName;
        }
    }

    public void SetUnknown1Input(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown1Input.GetComponent<Image>().color = Color.white;
                splineObject.Unknown1 =  Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown1Input.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown2Input(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown2Input.GetComponent<Image>().color = Color.white;
                splineObject.Unknown2 = Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown2Input.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void UpdatePoints()
    {
        if (!DisableUpdate)
        {
            splineSegment.Point1 = Point1.GrabXYZ();
            splineSegment.Point2 = Point2.GrabXYZ();
            splineSegment.Point3 = Point3.GrabXYZ();
            splineSegment.Point4 = Point4.GrabXYZ();
            splineSegment.SetDataLineRender(false);
        }
    }

    public void SetUnknown1X(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown1X.GetComponent<Image>().color = Color.white;
                splineSegment.ScalingPoint.x = Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown1X.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown1Y(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown1Y.GetComponent<Image>().color = Color.white;
                splineSegment.ScalingPoint.y = Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown1Y.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown1Z(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown1Z.GetComponent<Image>().color = Color.white;
                splineSegment.ScalingPoint.z = Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown1Z.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown1W(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown1W.GetComponent<Image>().color = Color.white;
                splineSegment.ScalingPoint.w = Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown1W.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void SetUnknown32(string Unknown1)
    {
        if (!DisableUpdate)
        {
            try
            {
                Unknown32.GetComponent<Image>().color = Color.white;
                splineSegment.Unknown32 = Int32.Parse(Unknown1);
            }
            catch
            {
                Unknown32.GetComponent<Image>().color = Color.red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
