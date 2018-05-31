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

    public void ResetStats()
    {
        Stone = 0;
        Coal = 0;
        Iron = 0;
        CrystalDida = 0;
        CrystalGale = 0;
        Energy = 0;
    }
}
