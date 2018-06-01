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
    private NavMeshPath path;

    private GameObject actualTarget;

    private void Start()
    {
        scrStats = GetComponent<scr_DataSet>();
        health = scrStats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);

        health.Value = healthMax.Value;

        path = new NavMeshPath();
        
    }

    private void Update()
    { 
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        CalculatePath();
        Walk();
    }

    void Walk()
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
