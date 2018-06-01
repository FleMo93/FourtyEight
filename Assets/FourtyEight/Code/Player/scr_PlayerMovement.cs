using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerMovement : MonoBehaviour
{

    public Rigidbody Rigid;
    public float power = 2;
    public float rotarePower = 2;

    void OnEnable()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }
    
    void FixedUpdate()
    {

        bool input = false;


        if (Input.GetAxis("Horizontal") > 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.forward * power);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 90, Time.deltaTime * rotarePower), 0);
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.back * power);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 270, Time.deltaTime * rotarePower), 0);
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.left * power);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 0, Time.deltaTime * rotarePower), 0);
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.right * power);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 180, Time.deltaTime * rotarePower), 0);
            }
        }

        if (!input)
        {
            Rigid.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
