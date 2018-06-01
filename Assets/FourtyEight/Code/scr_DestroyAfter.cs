using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DestroyAfter : MonoBehaviour {
    [SerializeField]
    private float _DestroyAfter = 1f;


	void Update ()
    {
        _DestroyAfter -= Time.deltaTime;

        if(_DestroyAfter <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
