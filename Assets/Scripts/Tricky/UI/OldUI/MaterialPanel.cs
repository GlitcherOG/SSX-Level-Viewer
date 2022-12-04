using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialPanel : MonoBehaviour
{
    public static MaterialPanel instance;
    public GameObject Canvas;
    public GameObject ButtonPrefab;
    public int MaterialID;
    bool DisableUpdate;

    public TMP_InputField MaterialName;

    public TMP_InputField TextureID;
    public TMP_InputField UnknownInt2;
    public TMP_InputField UnknownInt3;
    public TMP_InputField UnknownInt8;

    public TMP_InputField UnknownFloat1;
    public TMP_InputField UnknownFloat2;
    public TMP_InputField UnknownFloat3;
    public TMP_InputField UnknownFloat4;
    public TMP_InputField UnknownFloat5;
    public TMP_InputField UnknownFloat6;
    public TMP_InputField UnknownFloat7;
    public TMP_InputField UnknownFloat8;

    public TMP_InputField UnknownInt13;
    public TMP_InputField UnknownInt14;
    public TMP_InputField UnknownInt15;
    public TMP_InputField UnknownInt16;
    public TMP_InputField UnknownInt17;
    public TMP_InputField UnknownInt18;
    public TMP_InputField TextureFlipbookID;
    public TMP_InputField UnknownInt20;

    public List<GameObject> Buttons = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            Destroy(Buttons[i]);
        }
        Buttons = new List<GameObject>();

        for (int i = 0; i < TrickyMapInterface.Instance.materialJson.MaterialsJsons.Count; i++)
        {
            GameObject NewGameObject = Instantiate(ButtonPrefab, Canvas.transform);
            int a = i;
            NewGameObject.SetActive(true);
            NewGameObject.GetComponent<MaterialButton>().ID = a;
            NewGameObject.GetComponent<MaterialButton>().SetButtonData(TrickyMapInterface.Instance.materialJson.MaterialsJsons[i].MaterialName, TrickyMapInterface.Instance.materialJson.MaterialsJsons[i].TextureID);
            NewGameObject.GetComponent<MaterialButton>().SendPing.AddListener(delegate { instance.GetMaterialData(NewGameObject.GetComponent<MaterialButton>().ID); });
            Buttons.Add(NewGameObject);
        }
    }

    public void GetMaterialData(int ID)
    {
        DisableUpdate=true;
        var Material = TrickyMapInterface.Instance.materialJson.MaterialsJsons[ID];
        MaterialID = ID;
        MaterialName.text = Material.MaterialName;

        TextureID.text = Material.TextureID.ToString();
        UnknownInt2.text = Material.UnknownInt2.ToString();
        UnknownInt3.text = Material.UnknownInt3.ToString();
        UnknownInt8.text = Material.UnknownInt8.ToString();

        UnknownFloat1.text = Material.UnknownFloat1.ToString();
        UnknownFloat2.text = Material.UnknownFloat2.ToString();
        UnknownFloat3.text = Material.UnknownFloat3.ToString();
        UnknownFloat4.text = Material.UnknownFloat4.ToString();
        UnknownFloat5.text = Material.UnknownFloat5.ToString();
        UnknownFloat6.text = Material.UnknownFloat6.ToString();
        UnknownFloat7.text = Material.UnknownFloat7.ToString();
        UnknownFloat8.text = Material.UnknownFloat8.ToString();

        UnknownInt13.text = Material.UnknownInt13.ToString();
        UnknownInt14.text = Material.UnknownInt14.ToString();
        UnknownInt15.text = Material.UnknownInt15.ToString();
        UnknownInt16.text = Material.UnknownInt16.ToString();
        UnknownInt17.text = Material.UnknownInt17.ToString();
        UnknownInt18.text = Material.UnknownInt18.ToString();
        TextureFlipbookID.text = Material.TextureFlipbookID.ToString();
        UnknownInt20.text = Material.UnknownInt20.ToString();

        DisableUpdate = false;
    }
}
