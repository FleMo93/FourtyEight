using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tower : MonoBehaviour
{

    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute energyCost;
    private so_DataSet.Attribute minimumRange;
    private so_DataSet.Attribute damage;
    private so_DataSet.Attribute range;
    private SphereCollider minRangeSphereCollider;
    private SphereCollider rangeSphereCollider;

    void Start ()
    {
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        energyCost = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Energy_costs);
        minimumRange = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Minimum_range);
        range = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Range);
        damage = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Damage);

        health.Value = healthMax.Value;

        minRangeSphereCollider = gameObject.AddComponent<SphereCollider>();
        minRangeSphereCollider.isTrigger = true;
        minRangeSphereCollider.radius = minimumRange.Value / 2;

        rangeSphereCollider = gameObject.AddComponent<SphereCollider>();
        rangeSphereCollider.isTrigger = true;
        rangeSphereCollider.radius = range.Value / 2;
    }

    void Update()
    {

    }
}
