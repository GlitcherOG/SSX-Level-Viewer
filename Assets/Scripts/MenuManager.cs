using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SSXGames games = SSXGames.SSX;
    public List<GameObject> Background = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Left()
    {
        if (games != SSXGames.SSX)
        {
            Background[Convert.ToInt32(games)].SetActive(false);
            games += -1;
            Background[Convert.ToInt32(games)].SetActive(true);
        }
    }

    public void Right()
    {
        if (games != SSXGames.SSX3)
        {
            Background[Convert.ToInt32(games)].SetActive(false);
            games += 1;
            Background[Convert.ToInt32(games)].SetActive(true);
        }
    }
}

public enum SSXGames
{
    SSX,
    SSXTricky,
    SSX3
}
