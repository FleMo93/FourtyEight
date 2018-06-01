using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class scr_UI_BuildInfoData : MonoBehaviour
{
    public GameObject BuildInfoBox;
    public Image Icon;
    public Text TextStone;
    public Text TextCoal;
    public Text TextIron;
    public Text TextDida;
    public Text TextGale;
    public Text TextEnergy;

    so_DataSet DataSet;


    public void BtnClick_SetDataSet(so_DataSet _dataSet)
    {
        DataSet = _dataSet;
        BuildInfoBox.SetActive(true);
        SetUiValues(DataSet);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SetUiValues(so_DataSet _dataSet)
    {
        if (_dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Stone) != null)
            TextStone.text = _dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Stone).Value.ToString();
        else
            TextStone.text = "0";
        if (_dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Coal) != null)
            TextCoal.text = _dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Coal).Value.ToString();
        else
            TextCoal.text = "0";
        if (_dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Iron) != null)
            TextIron.text = _dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Iron).Value.ToString();
        else
            TextIron.text = "0";
        if (_dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Dida) != null)
            TextDida.text = _dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Dida).Value.ToString();
        else
            TextDida.text = "0";
        if (_dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Gale) != null)
            TextGale.text = _dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Gale).Value.ToString();
        else
            TextGale.text = "0";
        if (_dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy) != null)
            TextEnergy.text = _dataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy).Value.ToString();
        else
            TextEnergy.text = "0";

        Icon.sprite = DataSet.MainIcon;
    }
}
