using System.Collections;
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
    private float timeLeft = 0;

    private void Start()
    {
        healthMax = _Stats.Attributes.Find(x => x.Name == "Maximum Health");
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == "Health");

        health.Value = healthMax.Value;
        timeLeft = 1;
    }

    void Update()
    {
        
    }
}
