using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerAttack : MonoBehaviour
{

    public Transform bullet_Spawn;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (bullet_Spawn != null)
            {
                
            }
        }
    }
}
