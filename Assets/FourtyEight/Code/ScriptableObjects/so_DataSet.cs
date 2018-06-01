using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New DataSet", menuName = "ScriptabeObjects/DataSet")]
public class so_DataSet : ScriptableObject {

    public string Name; // Name of this Object that should be displayed in the UI
    public string Desctription; // Information about this Object that should be displayed in the UI
    public Sprite MainIcon; // Icon of this Object that should be displayed in the UI

    public List<Attribute> Attributes;

    [System.Serializable]
    public class Attribute
    {
        public scr_Attributes.Attribute Name;
        public Sprite Icon;
        public float Value;
        public int UiOrder = 999;
        public bool TakeFromLocalDataSet = false;
    }
}
