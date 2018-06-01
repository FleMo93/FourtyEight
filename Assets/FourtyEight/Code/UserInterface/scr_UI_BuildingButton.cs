﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class scr_UI_BuildingButton : MonoBehaviour, IPointerDownHandler {

    public so_DataSet DataSet;
    public so_DataSetGlobal GlobalDataSet;

    public string SearchString;
    public string SearchString2;
    public GameObject PreBuildObject;
    public GameObject BuildObject;
    GameObject previewObject;

    float CostStone;
    float CostCoal;
    float CostIron;
    float CostDida;
    float CostGale;
    float CostEnergy;

    bool buildable;
    bool buildModeActive;
    bool buildPosOK;

    // Use this for initialization
    void Start () {
        GetCost();
    }

    // Update is called once per frame
    void Update () {
        buildable = GlobalDataSet.Coal >= CostCoal && GlobalDataSet.Stone >= CostStone && GlobalDataSet.Iron >= CostIron && GlobalDataSet.CrystalDida >= CostDida &&
            GlobalDataSet.CrystalGale >= CostGale && GlobalDataSet.Energy >= CostEnergy;
        this.GetComponent<Button>().interactable = buildable;


        if (buildModeActive && !EventSystem.current.IsPointerOverGameObject() && GlobalDataSet.BuildModeActive == this.gameObject)
        {
            Debug.Log("FUCK");
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 9999))
            {
                Debug.Log(DataSet.Name.ToString());
                if (hit.collider.gameObject.name.ToLower().Contains(SearchString) && hit.collider.gameObject.name.ToLower().Contains(SearchString2))
                {
                    previewObject.transform.position = hit.collider.transform.Find("BuildPos").position;
                    buildPosOK = previewObject.GetComponent< scr_UI_BuildingPreObjects>().Buildable;
                }
            }

            if (buildPosOK && Input.GetMouseButtonDown(0))
            {
                GameObject buildObject = Instantiate(BuildObject);
                buildObject.transform.position = previewObject.transform.position;
                Destroy(previewObject);
                buildModeActive = false;
                buildPosOK = false;
                GlobalDataSet.BuildModeActive = null;
            }

            if (!buildPosOK && Input.GetMouseButtonDown(0))
            {
                Destroy(previewObject);
                buildModeActive = false;
                buildPosOK = false;
                RefundCosts();
                GlobalDataSet.BuildModeActive = null;
            }
        }




    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (this.GetComponent<Button>().interactable)
        {
            if (buildModeActive)
            {
                Destroy(previewObject);
                buildModeActive = false;
                buildPosOK = false;
                RefundCosts();
                GlobalDataSet.BuildModeActive = null;
            }

            if (!buildModeActive)
            {
                PayCosts();
                buildModeActive = true;
                previewObject = Instantiate(PreBuildObject);
                GlobalDataSet.BuildModeActive = this.gameObject;
            }

        }
    }

    void PayCosts()
    {
        GlobalDataSet.Stone -= (int)CostStone;
        GlobalDataSet.Coal -= (int)CostCoal;
        GlobalDataSet.Iron -= (int)CostIron;
        GlobalDataSet.CrystalDida -= (int)CostDida;
        GlobalDataSet.CrystalGale -= (int)CostGale;
        GlobalDataSet.Energy -= (int)CostEnergy;
    }

    void RefundCosts()
    {
        GlobalDataSet.Stone += (int)CostStone;
        GlobalDataSet.Coal += (int)CostCoal;
        GlobalDataSet.Iron += (int)CostIron;
        GlobalDataSet.CrystalDida += (int)CostDida;
        GlobalDataSet.CrystalGale += (int)CostGale;
        GlobalDataSet.Energy += (int)CostEnergy;
    }

    void GetCost()
    {
        if (DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Stone) != null)
            CostStone = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Stone).Value;
        if (DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Coal) != null)
            CostCoal = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Coal).Value;
        if (DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Iron) != null)
            CostIron = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Iron).Value;
        if (DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Dida) != null)
            CostDida = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Dida).Value;
        if (DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Gale) != null)
            CostGale = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Gale).Value;
        if (DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy) != null)
            CostEnergy = DataSet.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy).Value;
    }
}
