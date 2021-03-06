﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MountainStoneBlock : MonoBehaviour, I_IDamagable
{
    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;

    private void Start()
    {
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        health.Value = healthMax.Value;
    }
    

    void Update()
    {
        if (health.Value <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    public so_DataSet GetSoDataSet()
    {
        return _Stats;
    }

    public scr_DataSet GetScrDataSet()
    {
        return GetComponent<scr_DataSet>();
    }
}
