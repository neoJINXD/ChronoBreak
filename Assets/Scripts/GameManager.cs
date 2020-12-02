using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    // Game data - Needs to be saved
    private string name;


    // Individual level data
    public bool gameDone;



    void Start() 
    {
        // gameDone = false;
    }
}
