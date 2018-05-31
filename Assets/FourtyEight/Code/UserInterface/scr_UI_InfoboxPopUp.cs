using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class scr_UI_InfoboxPopUp : MonoBehaviour {

    public GameObject InfoBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
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
            else
            {
            }
        }
    }
}
