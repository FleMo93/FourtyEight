using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Extractor : MonoBehaviour, I_IClickable, I_IDamagable, I_IExtractor
{
    //public enum Resources { None, Iron, Stone, Coal, Crystal_Dida, Crystal_Gale }

    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;
    [SerializeField]
    private scr_Attributes.Attribute resourceToTake = scr_Attributes.Attribute.Coal;


    private so_DataSet.Attribute ressourcePerSecond;
    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute energyCost;
    private Animator animator;

    private float timeLeft = 0;

    void Awake ()
    {
        scr_Attributes.Attribute ressourcePerScondEnum = scr_Attributes.Attribute.Coal_per_second;

        switch(resourceToTake)
        {
            case scr_Attributes.Attribute.Coal:
                ressourcePerScondEnum = scr_Attributes.Attribute.Coal_per_second;
                break;
            case scr_Attributes.Attribute.Dida:
                ressourcePerScondEnum = scr_Attributes.Attribute.Dida_per_second;
                break;

            case scr_Attributes.Attribute.Gale:
                ressourcePerScondEnum = scr_Attributes.Attribute.Gale_per_second;
                break;

            case scr_Attributes.Attribute.Iron:
                ressourcePerScondEnum = scr_Attributes.Attribute.Iron_per_second;
                break;

            case scr_Attributes.Attribute.Stone:
                ressourcePerScondEnum = scr_Attributes.Attribute.Stone_per_second;
                break;
        }

        ressourcePerSecond = _Stats.Attributes.Find(x => x.Name == ressourcePerScondEnum);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        energyCost = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy);

        health.Value = healthMax.Value;
        timeLeft = 1;

        animator = GetComponent<Animator>();
        _StatsGlobal.Energy -= (int)energyCost.Value;
        
    }

    bool lastEnergyState = false;
	void Update ()
    {
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
        }

        if(_StatsGlobal.Energy < 0 || lastEnergyState)
        {
            animator.Play("Idle");
        }

        if(_StatsGlobal.Energy > 0 || !lastEnergyState)
        {
            animator.Play("Drill");
        }

        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            switch(resourceToTake)
            {
                case scr_Attributes.Attribute.Coal:
                    _StatsGlobal.Coal += (int)ressourcePerSecond.Value;
                    break;
                case scr_Attributes.Attribute.Dida:
                    _StatsGlobal.CrystalDida += (int)ressourcePerSecond.Value;
                    break;

                case scr_Attributes.Attribute.Gale:
                    _StatsGlobal.CrystalGale += (int)ressourcePerSecond.Value;
                    break;

                case scr_Attributes.Attribute.Iron:
                    _StatsGlobal.Iron += (int)ressourcePerSecond.Value;
                    break;

                case scr_Attributes.Attribute.Stone:
                    _StatsGlobal.Stone += (int)ressourcePerSecond.Value;
                    break;
            }

            timeLeft = 1;
        }
    }

    public so_DataSet GetSoDataSet()
    {
        return _Stats;
    }

    public scr_DataSet GetScrDataSet()
    {
        return GetComponent<scr_DataSet>();
    }

    private void OnDestroy()
    {
        _StatsGlobal.Energy += (int)energyCost.Value;
    }

    public void SetRessourceToTake(scr_Attributes.Attribute ressourceToTake)
    {
        resourceToTake = ressourceToTake;
    }
}
