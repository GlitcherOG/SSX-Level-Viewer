using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectorScript : MonoBehaviour
{
    public static SelectorScript instance;
    public bool active = true;

    public bool SelectedObject;
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject XYZMovement;

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    public PatchPage patchPage = null;
    public InstancePage instancePage = null;

    // Start is called before the first frame update
    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //Standard Click clear and Add To List
            if(Input.GetMouseButtonDown(0) && !FlyAroundCamera.Active)
            {
                Deselect();

                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                int a = GUIUtility.hotControl;
                if (a == 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    int DefaultLayer = 1 << 7;
                    int SelectedLayer = 1 << 6;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, DefaultLayer))
                    {
                        RaycastSelection(hit);
                    }


                }



            }
        }
    }

    void RaycastSelection(RaycastHit hit)
    {
        //XYZMovement.SetActive(false);
        //SelectedGameObject = hit.transform.gameObject;
        //SelectedObject = true;
        //selectedTag = SelectedGameObject.transform.tag;
        //SelectionCheck();
    }

    void SelectionCheck()
    {
        //if (SelectedObject)
        //{
        //    if (selectedTag == "Patch")
        //    {
        //        patchPage = gameObject.AddComponent<PatchPage>();
        //        XYZMovement.SetActive(true);
        //        XYZMovement.GetComponent<XYZMovmentController>().SetParent(SelectedGameObject);
        //        patchPage.patchObjects.Add(SelectedGameObject.GetComponent<PatchObject>());

        //        SelectedGameObject.GetComponent<PatchObject>().SelectedObject();
        //    }
        //    if (selectedTag == "Spline")
        //    {
        //        XYZMovement.SetActive(true);
        //        //SplinePanelObject.SetActive(true);
        //        //SplinePanel.instance.LoadSplineAndSegment(SelectedGameObject.GetComponent<SplineSegmentObject>());
        //        XYZMovement.GetComponent<XYZMovmentController>().SetParent(SelectedGameObject);
        //    }
        //    if (selectedTag == "Instances")
        //    {
        //        XYZMovement.SetActive(true);
        //        instancePage = gameObject.AddComponent<InstancePage>();
        //        XYZMovement.GetComponent<XYZMovmentController>().SetParent(SelectedGameObject);
        //        instancePage.instanceObjects.Add(SelectedGameObject.GetComponent<InstanceObject>());

        //    }
        //    if (selectedTag == "Patch Point")
        //    {
        //        XYZMovement.SetActive(true);
        //        SelectedGameObject.GetComponent<PatchPoint>().Selected();
        //        XYZMovement.GetComponent<XYZMovmentController>().SetParent(SelectedGameObject);
        //    }
        //    if (selectedTag == "XYZMovement")
        //    {
        //        XYZMovement.SetActive(true);
        //        XYZMovement.GetComponent<XYZMovmentController>().SetOldParent();
        //        SelectedGameObject.GetComponent<XYZPoint>().SetActive();
        //    }
        //    if (selectedTag == "XYZRotation")
        //    {
        //        XYZMovement.SetActive(true);
        //        XYZMovement.GetComponent<XYZMovmentController>().SetOldParent();
        //        SelectedGameObject.GetComponent<XYZRotation>().SetActive();
        //    }
        //}
        //else
        //{
        //    if(patchPage!=null)
        //    {
        //        Destroy(patchPage);
        //        patchPage = null;
        //    }
        //    XYZMovement.SetActive(false);
        //    //SplinePanelObject.SetActive(false);
        //}
    }

    public void ManualSelection(GameObject NewgameObject, bool DoSelectionCheck = true)
    {
        //Deselect();
        //XYZMovement.SetActive(false);
        //SelectedGameObject = NewgameObject;
        //SelectedObject = true;
        //selectedTag = SelectedGameObject.transform.tag;
        //if (DoSelectionCheck)
        //{
        //    SelectionCheck();
        //}
    }

    public void Deselect()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            var TempObj = gameObjects[i];


            //if (selectedTag == "Patch")
            //{
            //    SelectedGameObject.GetComponent<PatchObject>().UnSelectedObject();
            //}
            //if (selectedTag == "Spline")
            //{
            //    SelectedGameObject.GetComponent<SplineSegmentObject>().UnSelectedObject();
            //}
            //if (selectedTag == "Instances")
            //{
            //    //SelectedGameObject.GetComponent<InstanceObject>().UnSelectedObject();
            //}
            //XYZMovement.GetComponent<XYZMovmentController>().RemoveParent();
            //XYZMovement.SetActive(false);
            ////InstancePanelObject.GetComponent<InstancePanel>().HideSelfAndChild();
            ////SplinePanelObject.GetComponent<SplinePanel>().HideSelfAndChild();

            //if (patchPage != null)
            //{
            //    Destroy(patchPage);
            //    patchPage = null;
            //}

            //if (instancePage != null)
            //{
            //    Destroy(instancePage);
            //    instancePage = null;
            //}

            //selectedTag = "";
            //SelectedObject = false;
            //SelectedGameObject = null;
        }



        //if (SelectedObject)
        //{
        //    if (selectedTag == "Patch")
        //    {
        //        SelectedGameObject.GetComponent<PatchObject>().UnSelectedObject();
        //    }
        //    if(selectedTag=="Spline")
        //    {
        //        SelectedGameObject.GetComponent<SplineSegmentObject>().UnSelectedObject();
        //    }
        //    if(selectedTag== "Instances")
        //    {
        //        //SelectedGameObject.GetComponent<InstanceObject>().UnSelectedObject();
        //    }
        //    XYZMovement.GetComponent<XYZMovmentController>().RemoveParent();
        //    XYZMovement.SetActive(false);
        //    //InstancePanelObject.GetComponent<InstancePanel>().HideSelfAndChild();
        //    //SplinePanelObject.GetComponent<SplinePanel>().HideSelfAndChild();

        //    if (patchPage != null)
        //    {
        //        Destroy(patchPage);
        //        patchPage = null;
        //    }

        //    if(instancePage != null)
        //    {
        //        Destroy(instancePage);
        //        instancePage = null;
        //    }

        //    selectedTag = "";
        //    SelectedObject = false;
        //    SelectedGameObject = null;
        //}
    }
}
