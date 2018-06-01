using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerMovement : MonoBehaviour
{

    public Rigidbody Rigid;
    private so_DataSet.Attribute movePower;
    private so_DataSet.Attribute rotarePower;

    public Transform bullet_Spawn;
    public Transform bullet_Projectile;


    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    void Start()
    {
        movePower = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Movement_Speed);
        rotarePower = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Rotation_speed);
        Rigid = GetComponent<Rigidbody>();
    }
        
    void FixedUpdate()
    {
        bool input = false;
        if (Input.GetAxis("Vertical") > 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.forward * movePower.Value);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 90, Time.deltaTime * rotarePower.Value), 0);
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.back * movePower.Value);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 270, Time.deltaTime * rotarePower.Value), 0);
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.left * movePower.Value);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 0, Time.deltaTime * rotarePower.Value), 0);
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            input = true;
            if (Rigid)
            {
                Rigid.AddForce(Vector3.right * movePower.Value);
                transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, 180, Time.deltaTime * rotarePower.Value), 0);
            }
        }

        if (!input)
        {
            Rigid.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
