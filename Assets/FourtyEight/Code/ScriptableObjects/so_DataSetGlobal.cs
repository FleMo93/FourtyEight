using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New DataSetGlobal", menuName = "ScriptabeObjects/DataSetGlobal")]
public class so_DataSetGlobal : ScriptableObject {

    public int Stone;
    public int Coal;
    public int Iron;
    public int CrystalDida;
    public int CrystalGale;
    public int Energy;

    private float initialTime;
    public float time_Level;
    public float time_Game;
    public float time_forNextWave;

    public GameObject BuildModeActive;

    public void ResetStats()
    {
        Stone = 0;
        Coal = 0;
        Iron = 0;
        CrystalDida = 0;
        CrystalGale = 0;
        Energy = 0;
        BuildModeActive = null;
    }

    public float GetInitTime()
    {
        return initialTime;
    }

    public void SetInitTime(float i)
    {
        initialTime = i;
    }
}
