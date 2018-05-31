using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New DataSetGlobal", menuName = "ScriptabeObjects/DataSetGlobal")]
public class so_DataSetGlobal : ScriptableObject {

    public float Stone;
    public float Coal;
    public float Iron;
    public float CrystelDida;
    public float CrystelGale;
    public float Energy;

    public void ResetStats()
    {
        Stone = 0f;
        Coal = 0f;
        Iron = 0f;
        CrystelDida = 0f;
        CrystelGale = 0f;
        Energy = 0f;
    }
}
