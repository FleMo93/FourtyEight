using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tower : MonoBehaviour, I_IClickable, I_IDamagable
{

    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;
    [SerializeField]
    private GameObject _Head;
    [SerializeField]
    private GameObject _Cannon;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute energyCost;
    private so_DataSet.Attribute minimumRange;
    private so_DataSet.Attribute damage;
    private so_DataSet.Attribute range;
    private so_DataSet.Attribute fireRate;
    private so_DataSet.Attribute rotationSpeed;
    private SphereCollider rangeSphereCollider;
    private List<GameObject> enemysInRange;
    private Quaternion headDefault;
    private Quaternion cannonDefault;

    float timeLeftToFire;
    void Start ()
    {
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        energyCost = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy);
        minimumRange = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Minimum_range);
        range = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Range);
        damage = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Damage);
        fireRate = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Fire_rate);
        rotationSpeed = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Rotation_speed);

        health.Value = healthMax.Value;
        timeLeftToFire += fireRate.Value;

        rangeSphereCollider = gameObject.AddComponent<SphereCollider>();
        rangeSphereCollider.isTrigger = true;
        rangeSphereCollider.radius = range.Value / 2;

        enemysInRange = new List<GameObject>();

        _StatsGlobal.Energy-= (int)energyCost.Value;

        headDefault = _Head.transform.rotation.Copy();
        cannonDefault = _Cannon.transform.rotation.Copy();
    }

    void Update()
    {
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if(_StatsGlobal.Energy < 0)
        {
            return;
        }

        if (timeLeftToFire > 0)
        {
            timeLeftToFire -= Time.deltaTime;
        }

        if(timeLeftToFire <= 0)
        {
            GameObject enemy = GetEnemy();

            if(enemy != null)
            {
                RotateTowards(enemy.transform.position);
            }
            else
            {
                RotateToDefault();
            }
        }
    }

    private GameObject GetEnemy()
    {
        if(enemysInRange.Count > 0)
        {
            return enemysInRange[0];
        }
        else
        {
            return null;
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Quaternion actualRotation = _Head.transform.rotation.Copy();
        _Head.transform.LookAt(target);
        _Head.transform.rotation = Quaternion.Euler(0, _Head.transform.rotation.eulerAngles.y + 180, 0);
        Quaternion targetRotation = _Head.transform.rotation.Copy();
        _Head.transform.rotation = actualRotation;
        _Head.transform.rotation = Quaternion.RotateTowards(actualRotation, targetRotation, rotationSpeed.Value);
    }

    private void RotateToDefault()
    {
        _Head.transform.rotation = Quaternion.RotateTowards(_Head.transform.rotation, headDefault, rotationSpeed.Value);
        _Cannon.transform.rotation = Quaternion.RotateTowards(_Cannon.transform.rotation, cannonDefault, rotationSpeed.Value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == scr_Tags.Enemy && !enemysInRange.Contains(other.gameObject))
        {
            enemysInRange.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == scr_Tags.Enemy && enemysInRange.Contains(other.gameObject))
        {
            enemysInRange.Remove(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        _StatsGlobal.Energy += (int)energyCost.Value;
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
