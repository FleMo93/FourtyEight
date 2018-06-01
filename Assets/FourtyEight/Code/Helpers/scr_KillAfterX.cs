using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_KillAfterX : MonoBehaviour {

    public float lifeTime = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.fixedDeltaTime;
	}
}
