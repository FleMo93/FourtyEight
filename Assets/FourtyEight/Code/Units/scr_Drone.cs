using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Drone : MonoBehaviour, I_IDamagable, I_IClickable
{
    [SerializeField]
    private so_DataSet _Stats;

    private scr_DataSet scrStats;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;

    private void Start()
    {
        scrStats = GetComponent<scr_DataSet>();
        health = scrStats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);

        health.Value = healthMax.Value;
    }

    private void Update()
    {
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
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
