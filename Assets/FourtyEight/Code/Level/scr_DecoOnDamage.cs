using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DecoOnDamage : MonoBehaviour {

    public GameObject decoObject;

    public void MarkDamaged()
    {
        if (decoObject != null)
        {
            Instantiate(decoObject, transform.position, Quaternion.identity).transform.parent = transform;
        }
        Destroy(this);
    }
}
