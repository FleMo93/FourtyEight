using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class scr_UI_BuildingButton : MonoBehaviour, IPointerDownHandler {

    public so_DataSet DataSet;
    public so_DataSetGlobal GlobalDataSet;

    public Color NormalColor;
    public Color InactiveColor;

    float CostStone;
    float CostCoal;
    float CostIron;
    float CostDida;
    float CostGale;
    float CostEnergy;


    // Use this for initialization
    void Start () {
        try
        {
            CostStone = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Stone).Value;
            CostCoal = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Coal).Value;
            CostIron = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Iron).Value;
            CostDida = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Dida).Value;
            CostGale = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Gale).Value;
            CostEnergy = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy).Value;
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    // Update is called once per frame
    void Update () {
        bool buildable = GlobalDataSet.Coal >= CostCoal && GlobalDataSet.Stone >= CostStone && GlobalDataSet.Iron >= CostIron && GlobalDataSet.CrystalDida >= CostDida &&
            GlobalDataSet.CrystalGale >= CostGale && GlobalDataSet.Energy >= CostEnergy;

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (this.GetComponent<Button>().interactable)
        {

        }
    }
}
