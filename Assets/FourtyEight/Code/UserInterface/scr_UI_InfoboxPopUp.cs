using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class scr_UI_InfoboxPopUp : MonoBehaviour {

    public so_DataSetGlobal GlobalData;
    public float DisableTime = 1f;
    public GameObject InfoBox;
    float disabletimer;
    bool enable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GlobalData.BuildModeActive != null)
            disabletimer = DisableTime;

        if (disabletimer > 0)
            disabletimer -= Time.deltaTime;

        enable = disabletimer <= 0;

        
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && enable)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 9999))
            {
                if (hit.collider.gameObject.GetComponent<I_IClickable>() != null)
                {
                    InfoBox.GetComponent<scr_UI_Infobox>().DataSet = hit.collider.gameObject.GetComponent<I_IClickable>().GetSoDataSet();
                    InfoBox.GetComponent<scr_UI_Infobox>().LocalDataSet = hit.collider.gameObject.GetComponent<I_IClickable>().GetScrDataSet();
                    InfoBox.SetActive(true);
                }
            }
        }
    }
}
