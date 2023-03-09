using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ListButton : MonoBehaviour
{
    int ID;
    public TMP_Text ButtonName;
    public UnityAction<int> SendUpdatePing;

    public void SetData(string SetName, int NewID)
    {
        ButtonName.text = SetName;
        ID = NewID;
    }

    public void SendPing()
    {
        SendUpdatePing.Invoke(ID);
    }
}
