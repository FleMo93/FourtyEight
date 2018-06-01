using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class scr_PlayerAttack : MonoBehaviour
{

    public Transform bullet_Spawn;
    public Transform bullet_Projectile;


    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    private float coolDown_Load;
    private float coolDown; //private so_DataSet.Attribute coolDown;
    private so_DataSet.Attribute dmg;
    private so_DataSet.Attribute range;

    void Start()
    {
        coolDown_Load = 0;
        coolDown = 1;// _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.coolDown);
        dmg = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Damage);
        range = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Range);
    }

    // Update is called once per frame
    void Update()
    {
        coolDown_Load += Time.deltaTime;
        if (coolDown_Load > coolDown && Input.GetAxis("Jump") > 0)
        {
            if (bullet_Spawn != null)
            {
                Transform tempParticle = Instantiate(bullet_Projectile, bullet_Spawn.position, Quaternion.identity);
                coolDown_Load = 0;
                ShootWeapon();
            }
        }
    }
    
    void ShootWeapon()
    {
        RaycastHit hit;
        Ray ray = new Ray(bullet_Spawn.position - (bullet_Spawn.right/2), bullet_Spawn.right);

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        if (Physics.Raycast(ray, out hit, range.Value))
        {
            Transform objectHit = hit.transform;
            bool damageDeal = false;
            if (objectHit)
            {
                I_IDamagable targetDamagable = objectHit.GetComponent<I_IDamagable>();
                if (targetDamagable != null)
                {
                    so_DataSet.Attribute sotargetHealth = targetDamagable.GetSoDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
                    if (sotargetHealth.TakeFromLocalDataSet)
                    {
                        scr_DataSet.Attribute targetHealth = targetDamagable.GetScrDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
                        

                        if (targetHealth != null)
                        {
                            targetHealth.Value -= dmg.Value;
                            damageDeal = true;
                        }
                    }

                    if (damageDeal)
                    {
                        so_DataSet.Attribute typeOfOreBonus = targetDamagable.GetSoDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Gale);
                        if (typeOfOreBonus != null)
                        {
                            _StatsGlobal.CrystalGale += (int)typeOfOreBonus.Value;
                        }if (typeOfOreBonus == null)
                        {
                            typeOfOreBonus = targetDamagable.GetSoDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Dida);
                            _StatsGlobal.CrystalDida += (int)typeOfOreBonus.Value;
                        }
                        if (typeOfOreBonus == null)
                        {
                            typeOfOreBonus = targetDamagable.GetSoDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Iron);
                            _StatsGlobal.Iron += (int)typeOfOreBonus.Value;
                        }
                        if (typeOfOreBonus == null)
                        {
                            typeOfOreBonus = targetDamagable.GetSoDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Stone);
                            _StatsGlobal.Stone += (int)typeOfOreBonus.Value;
                        }
                        if (typeOfOreBonus == null)
                        {
                            typeOfOreBonus = targetDamagable.GetSoDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Coal);
                            _StatsGlobal.Coal += (int)typeOfOreBonus.Value;
                        }


                    }
                    //else //nothing!
                    //{
                    //    sotargetHealth.Value -= dmg;
                    //}
                }
            }
        }
    }
}
