using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Laser : MonoBehaviour, I_ILaser
{
    //[SerializeField]
    [SerializeField]
    ParticleSystem particleSystem;
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    private float _LifeTime = 0.1f;

    public void SetLength(float length)
    {
        lineRenderer.gameObject.transform.localScale = new Vector3(
            lineRenderer.transform.localScale.x,
            lineRenderer.transform.localScale.y,
            length
            );

        var emission = particleSystem.emission;
        var rateOverTime = emission.rateOverTime;
        rateOverTime.constant = emission.rateOverTime.constant * length;

        var shape = particleSystem.shape;
        shape.position = new Vector3(0, 0, length / 2);
        shape.scale = new Vector3(0.5f, 0.5f, length);

    }

    private void Update()
    {
        _LifeTime -= Time.deltaTime;

        if(_LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
