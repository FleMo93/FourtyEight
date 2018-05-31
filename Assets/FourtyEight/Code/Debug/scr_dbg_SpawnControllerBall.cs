using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dbg_SpawnControllerBall : MonoBehaviour {

    public Transform ball;

	// Use this for initialization
	void Start () {
        if (ball)
        {
            Instantiate(ball);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
