using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_Drone : MonoBehaviour, I_IDamagable, I_IClickable
{
    [SerializeField]
    private so_DataSet _Stats;

    private scr_DataSet scrStats;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute visibilityRange;
    private so_DataSet.Attribute rotationSpeed;
    private NavMeshPath path;

    private GameObject actualTarget;
    private GameObject player;

    private enum States { None, Attack, Move }
    private States state = States.None;

    private void Start()
    {
        scrStats = GetComponent<scr_DataSet>();
        health = scrStats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        visibilityRange = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Visibility_Range);
        rotationSpeed = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Rotation_speed);

        health.Value = healthMax.Value;

        path = new NavMeshPath();
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = visibilityRange.Value;

        player = GameObject.FindGameObjectWithTag(scr_Tags.Player);

        if(player == null)
        {
            throw new Exception("No player found!");
        }
    }

    private void Update()
    {
        DrawPath();

        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        CalculatePath();

        if(state == States.None)
        {

        }

        Walk();
    }

    void Walk()
    {
        Vector3? nextPath = GetNexPath();

        if(!nextPath.HasValue)
        {
            return;
        }


        RotateTowards(nextPath.Value);
    }

    void RotateTowards(Vector3 v3)
    {
        Quaternion actualRot = this.transform.rotation.Copy();
        this.transform.LookAt(v3);
        this.transform.eulerAngles = new Vector3(
            0,
            this.transform.eulerAngles.y,
            0);

        Quaternion targetRot = this.transform.rotation.Copy();
        this.transform.rotation = Quaternion.RotateTowards(actualRot, targetRot, rotationSpeed.Value * Time.deltaTime);
    }

    Vector3? GetNexPath()
    {
        if (path.corners.Length == 0)
        {
            return null;
        }
        else if (path.corners.Length == 1 &&
            Mathf.Approximately(path.corners[0].x, this.transform.position.x) &&
            Mathf.Approximately(path.corners[0].z, this.transform.position.z))
        {
            return null;
        }
        else if (path.corners.Length == 2 &&
            Mathf.Approximately(path.corners[1].x, this.transform.position.x) &&
            Mathf.Approximately(path.corners[1].z, this.transform.position.z))
        {
            return null;
        }
        else
        {
            return path.corners[1];
        }
    }

    void DrawPath()
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    float timeToPathCalculation = 1;
    void CalculatePath()
    {
        timeToPathCalculation -= Time.deltaTime;

        if (timeToPathCalculation <= 0)
        {
            NavMesh.CalculatePath(this.transform.position, new Vector3(0, 0, 0), NavMesh.AllAreas, path);
            timeToPathCalculation = 1;
        }
    }


    public so_DataSet GetSoDataSet()
    {
        return _Stats;
    }

    public scr_DataSet GetScrDataSet()
    {
        return scrStats;
    }
}
