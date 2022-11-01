using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotifcationBarUI : MonoBehaviour
{
    public static NotifcationBarUI instance;
    public GameObject bar;
    public TMP_Text text;

    public float Timer;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer<0)
        {
            if (bar!=null)
            {
                bar.SetActive(false);
            }
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }

    public void ShowNotifcation(string Notif, float Time)
    {
        text.text = Notif;
        Timer = Time;
        bar.SetActive(true);
    }
    
}
