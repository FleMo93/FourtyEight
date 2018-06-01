using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dbg_TerrainRayCaster : MonoBehaviour
{
    public Camera camera;

    bool wannaBuildingQuad = false;


    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray);

        if (Input.GetMouseButtonDown(1))
        {
            wannaBuildingQuad = !wannaBuildingQuad;
        }

        if (Input.GetMouseButton(0))
        {
            if (wannaBuildingQuad)
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.blue);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Transform objectHit = hit.transform;
                    //Debug.Log(objectHit.name);

                    //Debug.Log(scr_LevelManager.inst);
                    if (scr_LevelManager_proc.inst != null)
                    {
                        Debug.Log("2x2 is free: " + scr_LevelManager_proc.inst.currentLevel.isSpaceFree((int)objectHit.position.x, (int)objectHit.position.z,2,2));
                    }
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.blue);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Transform objectHit = hit.transform;
                    //Debug.Log(objectHit.name);

                    //Debug.Log(scr_LevelManager.inst);
                    if (scr_LevelManager_proc.inst != null)
                    {
                        scr_LevelManager_proc.inst.currentLevel.SetTileByte(0, (int)objectHit.position.x, (int)objectHit.position.z);
                    }
                }
            }
   
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction , Color.red);

        }
    }
}