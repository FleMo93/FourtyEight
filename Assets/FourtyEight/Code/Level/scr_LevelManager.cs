using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_LevelManager : MonoBehaviour
{


    public static so_DataSetGlobal gds;
    public  so_DataSetGlobal gds_nonStatic;

    void Awake()
    {
        gds = gds_nonStatic;
        gds.SetInitTime(Time.time);
    }

    void Update()
    {
        gds.time_Game = Time.time;
        gds.time_Level = Time.time - gds.GetInitTime();
    }

    public static float GetLevelTime()
    {
        return gds.time_Level;
    }
}
