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
	void LateUpdate () {
        if (target != null)
        {
            transform.position = target.position + Offset;
        }
        else
        {
            GameObject tempG = GameObject.FindGameObjectWithTag(scr_Tags.Player);
            if (tempG !=null)
            {
                target = tempG.transform;
            }
        }
	}
}
