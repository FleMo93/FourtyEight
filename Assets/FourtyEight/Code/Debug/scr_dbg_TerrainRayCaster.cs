using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dbg_TerrainRayCaster : MonoBehaviour
{
    public Camera camera;

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray);

        if (Input.GetMouseButton(0))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            if (Physics.Raycast(ray, out hit,100))
            {
                Transform objectHit = hit.transform;
                //Debug.Log(objectHit.name);

                //Debug.Log(scr_LevelManager.inst);
                if (scr_LevelManager.inst != null)
                {
                    scr_LevelManager.inst.currentLevel.SetTileByte(0, (int)objectHit.position.x, (int)objectHit.position.z);
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction , Color.red);

        }
    }
}