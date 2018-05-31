using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, I_IClickable
{

    [SerializeField]
    private so_DataSet _Stats;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;

    void Start()
    {
        healthMax = _Stats.Attributes.Find(x => x.Name == "Maximum Health");
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == "Health");
    }

    private void Update()
    {
        
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
