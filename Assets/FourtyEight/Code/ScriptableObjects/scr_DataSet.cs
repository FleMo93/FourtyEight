using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DataSet : MonoBehaviour
{
    public List<Attribute> Attributes;

    [System.Serializable]
    public class Attribute
    {
        public scr_Attributes.Attribute Name;
        public float Value;
    }

}
