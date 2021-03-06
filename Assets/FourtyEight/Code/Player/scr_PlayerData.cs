﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Currently not rly used
/// </summary>
public class scr_PlayerData : MonoBehaviour, I_IDamagable {


    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute moveSpeed;
    private so_DataSet.Attribute rotationSpeed;

    private scr_DataSet statsScr;

    void Start()
    {
        statsScr = GetComponent<scr_DataSet>();
        health = statsScr.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        moveSpeed = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Movement_Speed);
        rotationSpeed = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Rotation_speed);

        health.Value = healthMax.Value;
    }

    void Update()
    {
        if (health.Value <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public so_DataSet GetSoDataSet()
    {
        return _Stats;
    }

    public scr_DataSet GetScrDataSet()
    {
        return statsScr;
    }
}
