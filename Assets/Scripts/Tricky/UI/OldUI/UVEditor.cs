using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UVEditor : MonoBehaviour
{
    public PatchObject patchObject;
    public Texture2D texture;

    public RawImage rawImage1;
    public RawImage rawImage2;
    public RawImage rawImage3;
    public RawImage rawImage4;

    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;

    public TMP_InputField Point1XInput;
    public TMP_InputField Point1YInput;
    public TMP_InputField Point2XInput;
    public TMP_InputField Point2YInput;
    public TMP_InputField Point3XInput;
    public TMP_InputField Point3YInput;
    public TMP_InputField Point4XInput;
    public TMP_InputField Point4YInput;

    bool disableupdate;

    private void Awake()
    {
        point1.GetComponent<RawImage>().color = new Color32(202, 202, 202, 255);
        point2.GetComponent<RawImage>().color = new Color32(0, 119, 49, 255);
        point3.GetComponent<RawImage>().color = Color.blue;
        point4.GetComponent<RawImage>().color = Color.black;
    }

    public void SetActiveWithPatch(PatchObject _patchObject)
    {
        disableupdate = true;
        patchObject = _patchObject;
        texture = TrickyMapInterface.Instance.textures[patchObject.TextureAssigment];

        rawImage1.texture = texture;
        rawImage2.texture = texture;
        rawImage3.texture = texture;
        rawImage4.texture = texture;

        point1.transform.localPosition = patchObject.UVPoint1*150f;
        point2.transform.localPosition = patchObject.UVPoint2*150f;
        point3.transform.localPosition = patchObject.UVPoint3*150f;
        point4.transform.localPosition = patchObject.UVPoint4*150f;

        Point1XInput.text = patchObject.UVPoint1.x.ToString();
        Point1YInput.text = patchObject.UVPoint1.y.ToString();
        Point2XInput.text = patchObject.UVPoint2.x.ToString();
        Point2YInput.text = patchObject.UVPoint2.y.ToString();
        Point3XInput.text = patchObject.UVPoint3.x.ToString();
        Point3YInput.text = patchObject.UVPoint3.y.ToString();
        Point4XInput.text = patchObject.UVPoint4.x.ToString();
        Point4YInput.text = patchObject.UVPoint4.y.ToString();
        disableupdate = false;
    }

    public void Deactive()
    {
        patchObject = null;
        gameObject.SetActive(false);
    }

    public void UVInput(int a)
    {
        if (!disableupdate)
        {
            try
            {
                if (a == 1)
                {
                    patchObject.UVPoint1.x = float.Parse(Point1XInput.text);
                    patchObject.UVPoint1.y = float.Parse(Point1YInput.text);
                }
                if (a == 2)
                {
                    patchObject.UVPoint2.x = float.Parse(Point2XInput.text);
                    patchObject.UVPoint2.y = float.Parse(Point2YInput.text);
                }
                if (a == 3)
                {
                    patchObject.UVPoint3.x = float.Parse(Point3XInput.text);
                    patchObject.UVPoint3.y = float.Parse(Point3YInput.text);
                }
                if (a == 4)
                {
                    patchObject.UVPoint4.x = float.Parse(Point4XInput.text);
                    patchObject.UVPoint4.y = float.Parse(Point4YInput.text);
                }
                patchObject.UpdateUVPoints();
                point1.transform.localPosition = patchObject.UVPoint1 * 150f;
                point2.transform.localPosition = patchObject.UVPoint2 * 150f;
                point3.transform.localPosition = patchObject.UVPoint3 * 150f;
                point4.transform.localPosition = patchObject.UVPoint4 * 150f;
            }
            catch
            {

            }
        }
    }

    public void UVPointMoved(int a)
    {
        if(!disableupdate)
        {




            Point1XInput.text = patchObject.UVPoint1.x.ToString();
            Point1YInput.text = patchObject.UVPoint1.y.ToString();
            Point2XInput.text = patchObject.UVPoint2.x.ToString();
            Point2YInput.text = patchObject.UVPoint2.y.ToString();
            Point3XInput.text = patchObject.UVPoint3.x.ToString();
            Point3YInput.text = patchObject.UVPoint3.y.ToString();
            Point4XInput.text = patchObject.UVPoint4.x.ToString();
            Point4YInput.text = patchObject.UVPoint4.y.ToString();
        }
    }
}
