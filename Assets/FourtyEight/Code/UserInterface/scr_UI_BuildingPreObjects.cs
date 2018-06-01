using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_UI_BuildingPreObjects : MonoBehaviour {

    public bool Buildable = true;
    public Color ColorGood = Color.green;
    public Color ColorBad = Color.red;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Buildable)
            foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>())
            {
                rend.material.color = ColorGood;
            }
        if (!Buildable)
            foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>())
            {
                rend.material.color = ColorBad;
            }

    }
    private void OnTriggerStay(Collider other)
    {
        Buildable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Buildable = true;
    }
    
}
