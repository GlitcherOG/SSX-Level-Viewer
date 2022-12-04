using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

public class TextureLibarayPanel : MonoBehaviour
{
    public static TextureLibarayPanel instance;
    public GameObject ImageButtonPrefab;
    public GameObject ImageScrollBox;
    public RawImage Image;
    public List<TextureButton> buttons = new List<TextureButton>();

    int SelectedImage = -1;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeSelected(int a)
    {
        if (a != -1)
        {
            Image.texture = TrickyMapInterface.Instance.textures[a];
        }
        else
        {
            Image.texture = null;
        }
        SelectedImage=a;
    }

    public void RefreshLibaray()
    {
        TrickyMapInterface.Instance.ReloadTextures();
        LoadButtons();
    }

    void DestroyButtons()
    {
        SelectedImage = -1;
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i].gameObject);
        }
        buttons.Clear();
    }

    public void LoadButtons()
    {
        DestroyButtons();
        buttons = new List<TextureButton>();
        for (int i = 0; i < TrickyMapInterface.Instance.textures.Count; i++)
        {
            GameObject TempButton = Instantiate(ImageButtonPrefab, ImageScrollBox.transform);
            TempButton.GetComponentInChildren<TMPro.TMP_Text>().text = i.ToString();
            TempButton.GetComponent<TextureButton>().ID = i;
            TempButton.GetComponent<TextureButton>().unityEvent = ChangeSelected;
            TempButton.GetComponent<TextureButton>().image.texture = TrickyMapInterface.Instance.textures[i];
            TempButton.SetActive(true);
            buttons.Add(TempButton.GetComponent<TextureButton>());
        }
    }
}
