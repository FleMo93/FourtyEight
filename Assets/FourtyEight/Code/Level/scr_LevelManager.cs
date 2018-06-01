using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_LevelManager : MonoBehaviour {

    private static float  initialTime;

    void Awake()
    {
        initialTime = Time.time;
    }
    public static float GetLevelTime() {
        return Time.time - initialTime ;
    }
}
