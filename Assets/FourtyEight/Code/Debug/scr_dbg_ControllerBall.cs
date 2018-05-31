using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dbg_ControllerBall : MonoBehaviour
{

    public Rigidbody myRigid;
    float powor = 10;
    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (myRigid)
            {
                myRigid.AddForce(Vector3.forward * powor);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (myRigid)
            {
                myRigid.AddForce(Vector3.back * powor);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (myRigid)
            {
                myRigid.AddForce(Vector3.left * powor);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (myRigid)
            {
                myRigid.AddForce(Vector3.right* powor);
            }
        }
    }
}
