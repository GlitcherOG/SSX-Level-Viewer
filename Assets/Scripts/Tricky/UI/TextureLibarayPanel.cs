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
    public RawImage image;
    public TMP_Text color;
    public TMP_InputField shortName;

    public List<TextureButton> buttons;

    public bool Loaded;

    int SelectedImage = -1;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeSelected(int a)
    {
        if (a != -1)
        {
            SelectedImage = -1;
            image.texture = TrickyMapInterface.Instance.textures[a];
            color.text = TrickyMapInterface.Instance.sshHandler.sshImages[a].sshTable.colorTable.Count.ToString();
            shortName.text = TrickyMapInterface.Instance.sshHandler.sshImages[a].shortname;
            SelectedImage = a;
        }
        else
        {
            SelectedImage = a;
            image.texture = null;
            color.text = "";
            shortName.text = "";
        }
    }

    public void UpdateName(string NewName)
    {
        if (SelectedImage != -1)
        {
            var Images = TrickyMapInterface.Instance.sshHandler.sshImages;
            var Image = Images[SelectedImage];
            Image.shortname = NewName;
            Images[SelectedImage] = Image;
            TrickyMapInterface.Instance.sshHandler.sshImages = Images;
            buttons[SelectedImage].GetComponentInChildren<TMP_Text>().text = NewName;
        }
    }

    public void ReplaceImage()
    {
        if (SelectedImage != -1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "PNG Image (*.png)|*.png|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var Temp = TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage].bitmap;
                var ColorTable = TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage].sshTable;
                TrickyMapInterface.Instance.sshHandler.LoadSingle(openFileDialog.FileName, SelectedImage);
                if (TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage].sshTable.colorTable.Count <= 255)
                {
                    TrickyMapInterface.Instance.ForceUpdateAllTextures();
                    ChangeSelected(SelectedImage);
                    TrickyMapInterface.Instance.TextureChanged = true;
                }
                else
                {
                    int OldColourCount = TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage].sshTable.colorTable.Count;
                    var tempImage = TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage];
                    tempImage.sshTable = ColorTable;
                    tempImage.bitmap = Temp;
                    TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage] = tempImage;
                    MessageBox.Show("Warning Excedes Colour Limit (" + OldColourCount.ToString() + " Colours)");
                }
            }
        }
    }

    public void ExportImage()
    {
        if (SelectedImage != -1)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog()
            {
                Filter = "PNG Image (*.png)|*.png|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TrickyMapInterface.Instance.sshHandler.BMPOneExtract(openFileDialog.FileName, SelectedImage);
            }
        }
    }

    public void AddImage()
    {
        var Images = TrickyMapInterface.Instance.sshHandler.sshImages;
        var NewImage = TrickyMapInterface.Instance.sshHandler.sshImages[0];
        NewImage.bitmap = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        Images.Add(NewImage);
        TrickyMapInterface.Instance.sshHandler.sshImages = Images;
        TrickyMapInterface.Instance.ForceUpdateAllTextures();
        ChangeSelected(TrickyMapInterface.Instance.sshHandler.sshImages.Count-1);
        TrickyMapInterface.Instance.TextureChanged = true;
        DestroyButtons();
        LoadButtons();
    }

    public void RemoveImage()
    {
        if(SelectedImage!=-1)
        {
            var Images = TrickyMapInterface.Instance.sshHandler.sshImages;
            Images.RemoveAt(SelectedImage);
            TrickyMapInterface.Instance.sshHandler.sshImages = Images;
            TrickyMapInterface.Instance.ForceUpdateAllTextures();
            ChangeSelected(-1);
            TrickyMapInterface.Instance.TextureChanged = true;
            DestroyButtons();
            LoadButtons();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Loaded)
        {
            LoadButtons();
        }
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

    void LoadButtons()
    {
        buttons = new List<TextureButton>();
        for (int i = 0; i < TrickyMapInterface.Instance.textures.Count; i++)
        {
            Loaded = true;
            GameObject TempButton = Instantiate(ImageButtonPrefab, ImageScrollBox.transform);
            TempButton.GetComponentInChildren<TMPro.TMP_Text>().text = TrickyMapInterface.Instance.sshHandler.sshImages[i].shortname;
            TempButton.GetComponent<TextureButton>().ID = i;
            TempButton.GetComponent<TextureButton>().unityEvent = ChangeSelected;
            TempButton.GetComponent<TextureButton>().image.texture = TrickyMapInterface.Instance.textures[i];
            TempButton.SetActive(true);
            buttons.Add(TempButton.GetComponent<TextureButton>());
        }
    }
}
