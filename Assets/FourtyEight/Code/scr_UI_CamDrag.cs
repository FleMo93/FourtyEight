using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_UI_CamDrag : MonoBehaviour {

    private Vector3 dragOrigin;
    public float dragSpeed = 3;
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);

        transform.Translate(move, Space.World);


    }
}
