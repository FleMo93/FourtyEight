using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_Resources : MonoBehaviour {

    public so_DataSetGlobal DataGlobal;
    public Text Stone;
    public Text Coal;
    public Text Iron;
    public Text CrystelDida;
    public Text CrystelGale;
    public Text Energy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Stone.text = DataGlobal.Stone.ToString();
        Coal.text = DataGlobal.Coal.ToString();
        Iron.text = DataGlobal.Iron.ToString();
        CrystelDida.text = DataGlobal.CrystalDida.ToString();
        CrystelGale.text = DataGlobal.CrystalGale.ToString();
        Energy.text = DataGlobal.Energy.ToString();
    }
}
