using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_UI_CamFollowTarget : MonoBehaviour {

    public Transform target;
    public Vector3 Offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            transform.position = target.position + Offset;
        }
	}
}
