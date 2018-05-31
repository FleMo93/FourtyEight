using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_Infobox : MonoBehaviour {

    public so_DataSet DataSet;
    public Image Icon;
    public Text Name;
    public Text Description;
    public List<Attribute> Attributes;

	// Use this for initialization
	void Start () {
        Icon.sprite = DataSet.MainIcon;
        Name.text = DataSet.Name;
        Description.text = DataSet.Desctription;

        for (int i = 0; i < Attributes.Count; i++)
        {
            if ( i < DataSet.Attributes.Count)
            {
                Attributes[i].Icon.sprite = DataSet.Attributes[i].Icon;
                Attributes[i].Value.text = DataSet.Attributes[i].Value.ToString();
            }
            else
            {
                Attributes[i].Object.SetActive(false);
            }

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [System.Serializable]
    public class Attribute
    {
        public GameObject Object;
        public Image Icon;
        public Text Value;
    }
}
