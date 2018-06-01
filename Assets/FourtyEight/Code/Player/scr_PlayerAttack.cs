using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerAttack : MonoBehaviour
{

    public Transform bullet_Spawn;
    public Transform bullet_Projectile;
    public float coolDown_Load = 0;
    public float coolDown = 1;

    public float range = 1;

    // Update is called once per frame
    void Update()
    {
        coolDown_Load += Time.deltaTime;
        if (coolDown_Load > coolDown && Input.GetAxis("Fire1") > 0)
        {
            if (bullet_Spawn != null)
            {
                Transform tempB = Instantiate(bullet_Projectile, bullet_Spawn.position, Quaternion.identity);
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
        if (Physics.Raycast(ray, out hit, range))
        {
            Transform objectHit = hit.transform;
            if (objectHit)
            {
                I_IDamagable targetDamagable = objectHit.GetComponent<I_IDamagable>();
                if (targetDamagable != null)
                {
                    scr_DataSet.Attribute targetHealth = targetDamagable.GetScrDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);

                    if (targetHealth != null)
                    {
                        targetHealth.Value -= 5;
                    }
                }

                //scr_MountainStoneBlock targetDamagable = objectHit.GetComponent<scr_MountainStoneBlock>();
                //if (targetDamagable != null)
                //{
                //    scr_DataSet.Attribute targetHealth = targetDamagable.GetScrDataSet().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);

                //    if (targetHealth != null)
                //    {
                //        targetHealth.Value -= 5;
                //    }
                //}
                
            }
        }
    }
}
