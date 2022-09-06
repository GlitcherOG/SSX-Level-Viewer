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
    public GameObject SelectedGameObject;
    public string selectedTag;
    public GameObject PatchController;
    public GameObject XYZMovement;

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

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
            if (Input.GetMouseButtonDown(0) && !FlyAroundCamera.Active)
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);
                if (results.Count == 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    int layerMask = 1 << 7;
                    int layerMask2 = 1 << 6;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        RaycastSelection(hit);
                    }
                    else
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask2))
                    {
                        RaycastSelection(hit);
                    }
                    else
                    if (Physics.Raycast(ray, out hit))
                    {
                        Deselect();
                        RaycastSelection(hit);
                    }
                    else
                    {
                        Deselect();
                        if (PatchPanel.instance != null)
                        {
                            PatchPanel.instance.DestoyCubes();
                        }
                    }

                }
            }
        }
    }

    void RaycastSelection(RaycastHit hit)
    {
        //Debug.Log(hit.transform.name);
        XYZMovement.SetActive(false);
        SelectedGameObject = hit.transform.gameObject;
        SelectedObject = true;
        selectedTag = SelectedGameObject.transform.tag;
        SelectionCheck();
    }

    void SelectionCheck()
    {
        if (SelectedObject)
        {
            if (selectedTag == "Patch")
            {
                PatchController.SetActive(true);
                PatchPanel.instance.UpdateAll(SelectedGameObject.GetComponent<PatchObject>());
            }
            if(selectedTag == "Patch Point")
            {
                XYZMovement.SetActive(true);
                PatchController.SetActive(true);
                XYZMovement.GetComponent<XYZMovmentController>().SetParent(SelectedGameObject);
            }
            if(selectedTag == "XYZMovement")
            {
                PatchController.SetActive(true);
                XYZMovement.SetActive(true);
                XYZMovement.GetComponent<XYZMovmentController>().SetOldParent();
                SelectedGameObject.GetComponent<XYZPoint>().SetActive();
            }
        }
        else
        {
            PatchController.SetActive(false);
            XYZMovement.SetActive(false);
        }
    }

    public void Deselect()
    {
        if(SelectedObject)
        {
            if (selectedTag == "Patch")
            {
                SelectedGameObject.GetComponent<PatchObject>().UnSelectedObject();
                PatchPanel.instance.DestoyCubes();
            }
            XYZMovement.GetComponent<XYZMovmentController>().RemoveParent();
            XYZMovement.SetActive(false);
            PatchController.GetComponent<PatchPanel>().HideSelfAndChild();
            selectedTag = "";
            SelectedObject = false;
            SelectedGameObject = null;
        }
    }
}
