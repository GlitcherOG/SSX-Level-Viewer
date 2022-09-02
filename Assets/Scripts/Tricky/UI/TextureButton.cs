using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextureButton : MonoBehaviour
{
    public int ID;
    public UnityAction<int> unityEvent;
    public RawImage image;

    public void SendEvent()
    {
        unityEvent.Invoke(ID);
    }
}
