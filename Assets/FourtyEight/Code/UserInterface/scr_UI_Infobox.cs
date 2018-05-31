using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_Infobox : MonoBehaviour {

    public so_DataSet DataSet;
    public Image Icon;
    public Text Name;
    public Text Description;
    public List<Attribute> Attributes;
    List<so_DataSet.Attribute> attToShow;

    // Use this for initialization
    void Start () {
        Icon.sprite = DataSet.MainIcon;
        Name.text = DataSet.Name;
        Description.text = DataSet.Desctription;
        attToShow = DataSet.Attributes.ToArray().Where(x => x.UiOrder != 999).OrderBy(x => x.UiOrder).Take(6).ToList();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Attributes.Count; i++)
        {
            if (i < attToShow.Count)
            {
                Attributes[i].Icon.sprite = attToShow[i].Icon;
                Attributes[i].Value.text = attToShow[i].Value.ToString();
            }
            else
            {
                Attributes[i].Object.SetActive(false);
            }

        }
    }

    [System.Serializable]
    public class Attribute
    {
        public GameObject Object;
        public Image Icon;
        public Text Value;
    }
}
