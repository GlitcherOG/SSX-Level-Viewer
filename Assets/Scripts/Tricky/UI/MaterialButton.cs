using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MaterialButton : MonoBehaviour
{
    public int ID;
    public TMP_Text MaterialName;
    public RawImage texture2D;
    public UnityEvent SendPing;

    public void SetButtonData(string NewName, int TextureID)
    {
        MaterialName.text = NewName;
        texture2D.texture = TrickyMapInterface.Instance.textures[TextureID];
    }

    public void GetMaterial()
    {
        SendPing.Invoke();
    }
}
