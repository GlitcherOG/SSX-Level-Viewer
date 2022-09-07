using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SplinePanel : MonoBehaviour
{
    public static SplinePanel instance;
    public TMP_InputField NameInput;
    public TMP_InputField Unknown1Input;
    public TMP_InputField Unknown2Input;

    public GameObject SegmentSelectorBox;
    public GameObject SegmentSelectorPrefab;
    public List<GameObject> SegmentSelectorList;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
