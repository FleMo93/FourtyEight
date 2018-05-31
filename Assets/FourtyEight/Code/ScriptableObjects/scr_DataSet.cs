using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DataSet : MonoBehaviour
{
    public List<Attribute> Attributes;

    [System.Serializable]
    public class Attribute
    {
        public string Name;
        public float Value;
    }

}
