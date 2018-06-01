using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class scr_UI_BuildingButton : MonoBehaviour, IPointerDownHandler {

    public so_DataSet DataSet;
    public so_DataSetGlobal GlobalDataSet;

    List<so_DataSet.Attribute> attToShow;


    // Use this for initialization
    void Start () {
      //  attToShow = DataSet.Attributes.ToArray().Where(x => x.Name == scr_Attributes.Attribute.cos
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnPointerDown(PointerEventData data)
    {
        if (this.GetComponent<Button>().interactable)
        {

        }
    }
}
