using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomekScriptTest2 : MonoBehaviour, I_IClickable {

    public so_DataSet Data;

    public scr_DataSet GetScrDataSet()
    {
        return null;
    }

    public so_DataSet GetSoDataSet()
    {
        return Data;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
