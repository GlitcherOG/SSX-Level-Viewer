using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Windows.Forms;

public class TextureLibarayPanel : MonoBehaviour
{
    public static TextureLibarayPanel instance;
    public GameObject ImageButtonPrefab;
    public GameObject ImageScrollBox;
    public RawImage image;
    public TMP_Text color;
    public TMP_InputField shortName;

    public bool Loaded;

    int SelectedImage = -1;

    private void Awake()
    {
        instance = this;

    }

    public void ChangeSelected(int a)
    {
        image.texture = TrickyMapInterface.Instance.textures[a];
        color.text = TrickyMapInterface.Instance.sshHandler.sshImages[a].sshTable.colorTable.Count.ToString();
        shortName.text = TrickyMapInterface.Instance.sshHandler.sshImages[a].shortname;
        SelectedImage = a;
    }

    public void UpdateName(string NewName)
    {
        //TrickyMapInterface.Instance.sshHandler.sshImages[SelectedImage].shortname = NewName;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Loaded)
        {
            for (int i = 0; i < TrickyMapInterface.Instance.textures.Count; i++)
            {
                Loaded = true;
                GameObject TempButton = Instantiate(ImageButtonPrefab, ImageScrollBox.transform);
                TempButton.GetComponentInChildren<TMPro.TMP_Text>().text = TrickyMapInterface.Instance.sshHandler.sshImages[i].shortname;
                TempButton.GetComponent<TextureButton>().ID = i;
                TempButton.GetComponent<TextureButton>().unityEvent = ChangeSelected;
                TempButton.GetComponent<TextureButton>().image.texture = TrickyMapInterface.Instance.textures[i];
                TempButton.SetActive(true);
            }
        }
    }
}
