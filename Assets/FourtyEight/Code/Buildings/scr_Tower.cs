using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private GameObject _FirePosition;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private AudioSource shotSource;

    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute energyCost;
    private so_DataSet.Attribute minimumRange;
    private so_DataSet.Attribute damage;
    private so_DataSet.Attribute range;
    private so_DataSet.Attribute fireRate;
    private so_DataSet.Attribute rotationSpeed;
    private Quaternion headDefault;
    private Quaternion cannonDefault;
    private GameObject cannonRotateTowards;

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
        _StatsGlobal.Energy-= (int)energyCost.Value;

        headDefault = _Head.transform.rotation.Copy();
        cannonDefault = _Cannon.transform.rotation.Copy();
        cannonRotateTowards = new GameObject("_CannonRotateTowards");
        cannonRotateTowards.transform.SetParent(_Head.transform);
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

        GameObject enemy = GetEnemy();

        if(enemy != null)
        {
            RotateHeadTowards(enemy.transform.position);
            RotateCannoneTowards(enemy.transform.position);

            if (timeLeftToFire <= 0 && IsLookingAtEnemy(enemy))
            {
                I_IDamagable damagable = enemy.GetComponent<I_IDamagable>();

                if(damagable == null)
                {
                    throw new Exception("GameObject with tag " + scr_Tags.Enemy + " need I_IDamagable script!");
                }

                so_DataSet.Attribute soHealth = damagable.GetSoDataSet().Attributes.Where(x => x.Name == scr_Attributes.Attribute.Health).FirstOrDefault();

                if(soHealth == null)
                {
                    throw new Exception("I_IDamagable needs health attribute!");
                }

                if (soHealth.TakeFromLocalDataSet)
                {
                    scr_DataSet.Attribute scrHealth = damagable.GetScrDataSet().Attributes.Where(x => x.Name == scr_Attributes.Attribute.Health).FirstOrDefault();
                    
                    if(scrHealth == null)
                    {
                        throw new Exception("I_IDamagable needs health attribute!");
                    }

                    scrHealth.Value -= damage.Value;
                }
                else
                {
                    soHealth.Value -= damage.Value;
                }

                GameObject go = Instantiate(_LaserPrefab);
                go.transform.position = _FirePosition.transform.position;
                go.transform.LookAt(enemy.transform.position);

                float distance = Vector3.Distance(_FirePosition.transform.position, enemy.transform.position);
                go.GetComponent<I_ILaser>().SetLength(distance);

                shotSource.Play();

                timeLeftToFire = fireRate.Value;
            }
        }
        else
        {
            RotateToDefault();
        }
    }

    private GameObject GetEnemy()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach(GameObject enemy in EnemiesInRange())
        {
            if(Vector3.Distance(this.transform.position, enemy.transform.position) > minimumRange.Value)
            {
                return enemy;
            }
        }

        return null;
    }

    private GameObject[] EnemiesInRange()
    {
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, range.Value, Vector3.up);

        List<GameObject> enemies = new List<GameObject>();

        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.gameObject.tag == scr_Tags.Enemy)
            {
                enemies.Add(hit.collider.gameObject);
            }
        }
        return enemies.ToArray();
    }

    private void RotateHeadTowards(Vector3 target)
    {
        Quaternion actualRotation = _Head.transform.rotation.Copy();
        _Head.transform.LookAt(target);
        _Head.transform.rotation = Quaternion.Euler(0, _Head.transform.rotation.eulerAngles.y, 0);
        Quaternion targetRotation = _Head.transform.rotation.Copy();
        _Head.transform.rotation = actualRotation;
        _Head.transform.rotation = Quaternion.RotateTowards(actualRotation, targetRotation, rotationSpeed.Value);
    }

    private void RotateCannoneTowards(Vector3 target)
    {
        
        Vector3 ankTarget = target;
        ankTarget.y = _Cannon.transform.position.y;
        float ank = Vector3.Distance(_Cannon.transform.position, ankTarget);
        float gegKa = target.y - _Cannon.transform.position.y;

        cannonRotateTowards.transform.position = _Cannon.transform.position;
        cannonRotateTowards.transform.Translate(new Vector3(0, gegKa, ank));

        Quaternion actualRotation = _Cannon.transform.rotation.Copy();
        _Cannon.transform.LookAt(cannonRotateTowards.transform);
        Quaternion targetRotation = _Cannon.transform.rotation.Copy();
        _Cannon.transform.rotation = actualRotation;
        _Cannon.transform.rotation = Quaternion.RotateTowards(actualRotation, targetRotation, 1f);
    }

    private void RotateToDefault()
    {
        _Head.transform.rotation = Quaternion.RotateTowards(_Head.transform.rotation, headDefault, rotationSpeed.Value);
        Vector3 v3 = _Cannon.transform.position;
        v3 += _Cannon.transform.forward;
        v3.y = _Cannon.transform.position.y;
        RotateCannoneTowards(v3);
    }

    private bool IsLookingAtEnemy(GameObject enemy)
    {
        Collider[] colliders = enemy.GetComponents<Collider>();

        Ray ray = new Ray(_FirePosition.transform.position, _FirePosition.transform.forward);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit, range.Value))
        {
            return false;
        }

        if(colliders.Contains(hit.collider))
        {
            return true;
        }

        return false;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == scr_Tags.Enemy && !enemysInRange.Contains(other.gameObject))
    //    {
    //        enemysInRange.Add(other.gameObject);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == scr_Tags.Enemy && enemysInRange.Contains(other.gameObject))
    //    {
    //        enemysInRange.Remove(other.gameObject);
    //    }
    //}

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
