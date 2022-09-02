using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject MainObject;
    public void Toggle()
    {
        MainObject.SetActive(!MainObject.activeInHierarchy);
    }
}
