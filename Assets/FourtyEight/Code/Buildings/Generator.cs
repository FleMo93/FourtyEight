﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute createsEnergy;
    private float timeLeft = 0;

    private void Start()
    {
        healthMax = _Stats.Attributes.Find(x => x.Name == "Maximum Health");
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == "Health");
        createsEnergy = _Stats.Attributes.Find(x => x.Name == "Creates energy");
        health.Value = healthMax.Value;
        timeLeft = 1; 
    }

    bool lastEnergyState = false;
    void Update()
    {
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            if (_StatsGlobal.Coal > 0)
            {
                _StatsGlobal.Coal -= 1;

                if (!lastEnergyState)
                {
                    _StatsGlobal.Energy += (int)createsEnergy.Value;
                    lastEnergyState = true;
                }
            }
            else
            {
                if (lastEnergyState)
                {
                    _StatsGlobal.Energy -= (int)createsEnergy.Value;
                    lastEnergyState = false;
                }
            }

            timeLeft += 1;
        }
    }
}